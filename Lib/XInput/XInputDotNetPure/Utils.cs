using System;

namespace XInputDotNetPure
{
    internal struct ThumbStickValue
    {
        public float x, y;

        public ThumbStickValue(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
    }

    static class Utils
    {
        public const uint Success = 0x000;
        public const uint NotConnected = 0x000;

        public const int LeftStickDeadZone = 7849;
        public const int RightStickDeadZone = 8689;
        public const int TriggerDeadZone = 30;

        public static float ApplyTriggerDeadZone(byte value, GamePadDeadZone deadZoneMode)
        {
            if (deadZoneMode == GamePadDeadZone.None)
            {
                return ApplyDeadZone(value, byte.MaxValue, 0.0f);
            }
            else
            {
                return ApplyDeadZone(value, byte.MaxValue, TriggerDeadZone);
            }
        }

        public static ThumbStickValue ApplyStickDeadZone(short valueX, short valueY, GamePadDeadZone deadZoneMode, int deadZoneSize)
        {
            if (deadZoneMode == GamePadDeadZone.Circular)
            {
                // Cast to long to avoid int overflow if valueX and valueY are both 32768, which would result in a negative number and Sqrt returns NaN
                float distanceFromCenter = (float)Math.Sqrt((long)valueX * (long)valueX + (long)valueY * (long)valueY);
                float coefficient = ApplyDeadZone(distanceFromCenter, short.MaxValue, deadZoneSize);
                coefficient = coefficient > 0.0f ? coefficient / distanceFromCenter : 0.0f;
                return new ThumbStickValue(
                    Clamp(valueX * coefficient),
                    Clamp(valueY * coefficient)
                );
            }
            else if (deadZoneMode == GamePadDeadZone.IndependentAxes)
            {
                return new ThumbStickValue(
                    ApplyDeadZone(valueX, short.MaxValue, deadZoneSize),
                    ApplyDeadZone(valueY, short.MaxValue, deadZoneSize)
                );
            }
            else
            {
                return new ThumbStickValue(
                    ApplyDeadZone(valueX, short.MaxValue, 0.0f),
                    ApplyDeadZone(valueY, short.MaxValue, 0.0f)
                );
            }
        }

        private static float Clamp(float value)
        {
            return value < -1.0f ? -1.0f : (value > 1.0f ? 1.0f : value);
        }

        private static float ApplyDeadZone(float value, float maxValue, float deadZoneSize)
        {
            if (value < -deadZoneSize)
            {
                value += deadZoneSize;
            }
            else if (value > deadZoneSize)
            {
                value -= deadZoneSize;
            }
            else
            {
                return 0.0f;
            }

            value /= maxValue - deadZoneSize;

            return Clamp(value);
        }
    }
}
