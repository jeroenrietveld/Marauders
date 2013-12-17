using System;
using System.Runtime.InteropServices;

namespace XInputDotNetPure
{

    class Imports
    {
        internal const string DLLName = "XInputInterface";

        [DllImport(DLLName)]
        public static extern uint XInputGamePadGetState(uint playerIndex, IntPtr state);
        [DllImport(DLLName)]
        public static extern void XInputGamePadSetState(uint playerIndex, float leftMotor, float rightMotor);
    }

    public enum Button : ushort
    {
        DPadUp          = (1 << 0),
        DPadDown        = (1 << 1),
        DPadLeft        = (1 << 2),
        DPadRight       = (1 << 3),
        Start           = (1 << 4),
        Back            = (1 << 5),
        LeftThumb       = (1 << 6),
        RightThumb      = (1 << 7),
        LeftShoulder    = (1 << 8),
        RightShoulder   = (1 << 9),
        A               = (1 << 12),
        B               = (1 << 13),
        X               = (1 << 14),
        Y               = (1 << 15)
    }

    public enum Axis
    {
        LeftHorizontal,
        LeftVertical,

        RightHorizontal,
        RightVertical,

        LeftTrigger,
        RightTrigger
    }

    public enum PlayerIndex
    {
        One,
        Two,
        Three,
        Four
    }

    public enum GamePadDeadZone
    {
        Circular,
        IndependentAxes,
        None
    }

    internal struct RawState
    {
        public uint dwPacketNumber;
        public GamePad Gamepad;

        public struct GamePad
        {
            public ushort dwButtons;
            public byte bLeftTrigger;
            public byte bRightTrigger;
            public short sThumbLX;
            public short sThumbLY;
            public short sThumbRX;
            public short sThumbRY;
        }
    }

    public class GamePad
    {
        private PlayerIndex _index;

        public GamePadDeadZone deadZone = GamePadDeadZone.Circular;

        private RawState _current, _previous;
        private float[] _axis = new float[System.Enum.GetValues(typeof(Axis)).Length];
        private float _vibrationTime;

        public bool connected { get; private set; }

        public GamePad(PlayerIndex index)
        {
            this._index = index;
        }

        public float Axis(Axis axis)
        {
            return _axis[(int)axis];
        }

        public bool Pressed(Button button)
        {
            return Pressed(button, _current);
        }

        public bool JustPressed(Button button)
        {
            return Pressed(button) && !Pressed(button, _previous);
        }

        public bool JustReleased(Button button)
        {
            return !Pressed(button) && Pressed(button, _previous);
        }

        private bool Pressed(Button button, RawState state)
        {
            return (state.Gamepad.dwButtons & (ushort)button) != 0;
        }

        public void SetVibration(float leftMotor, float rightMotor, float time)
        {
            Imports.XInputGamePadSetState((uint)_index, leftMotor, rightMotor);
            _vibrationTime = time;
        }

		public void Update(float tpf)
		{
            _previous = _current;

			unsafe
			{
				byte* rawData = stackalloc byte [Marshal.SizeOf(typeof(RawState))];
				IntPtr rawPtr = new IntPtr(rawData);

				connected = Imports.XInputGamePadGetState((uint)_index, rawPtr) == Utils.Success;
                
				_current = (RawState)Marshal.PtrToStructure(rawPtr, typeof(RawState));
			}

            var gamePad = _current.Gamepad;

            var leftThumbstick = Utils.ApplyStickDeadZone(gamePad.sThumbLX, gamePad.sThumbLY, deadZone, Utils.LeftStickDeadZone);
            _axis[(int)XInputDotNetPure.Axis.LeftHorizontal] = leftThumbstick.x;
            _axis[(int)XInputDotNetPure.Axis.LeftVertical] = leftThumbstick.y;

            var rightThumbstick = Utils.ApplyStickDeadZone(gamePad.sThumbRX, gamePad.sThumbRY, deadZone, Utils.RightStickDeadZone);
            _axis[(int)XInputDotNetPure.Axis.RightHorizontal] = rightThumbstick.x;
            _axis[(int)XInputDotNetPure.Axis.RightVertical] = rightThumbstick.y;

            _axis[(int)XInputDotNetPure.Axis.LeftTrigger] = Utils.ApplyTriggerDeadZone(gamePad.bLeftTrigger, deadZone);
            _axis[(int)XInputDotNetPure.Axis.RightTrigger] = Utils.ApplyTriggerDeadZone(gamePad.bRightTrigger, deadZone);

            // Only if vibration hasn't been switched off yet. Avoid unneccesary calls to XInput.
            if (_vibrationTime >= 0)
            {
                _vibrationTime -= tpf;
                if (_vibrationTime <= 0)
                {
                    Imports.XInputGamePadSetState((uint)_index, 0, 0);
                }
            }
		}
    }
}
