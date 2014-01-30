using UnityEngine;
using System.Linq;
using System.Collections;
using XInputDotNetPure;

public class LevelState : MenuStateBase
{
    private GamePad _controller;
    private Timer _timer;

	public LevelState()
	{
        center = GameObject.Find("LevelScreen").transform.position;

        _timer = new Timer(1f);
        _timer.AddCallback(delegate()
        {
            GameManager.Instance.Start();
        });
        _controller = ControllerInput.GetController(PlayerIndex.One);
        _controller.deadZone = GamePadDeadZone.IndependentAxes;
	}

	public override void Update(MenuManager manager)
	{
        _timer.Update();
        if (LevelSelectionManager.currentState != null)
        {
            _controller = GameManager.Instance.playerRefs.OrderBy(x => x.indexInt).First().controller;
            LevelSelectionManager.currentState.Update(_controller);
        }

        if (_controller.JustPressed(Button.B))
	    {
            if(LevelSelectionManager.currentState == LevelSelectionManager.selectionBlocks[LevelSelectionState.NotSelecting])
            {
                manager.ChangeState(MenuStates.ArmoryState);
            }
	    }
        if(_controller.JustPressed(Button.A))
        {
            if(LevelSelectionManager.currentState == LevelSelectionManager.selectionBlocks[LevelSelectionState.Done])
            {              
                GameObject.Find("CameraFade").GetComponent<CameraFade>().StartFade(new Color(0, 0, 0, 1), 2f);                
                _timer.Start();               
            }
        }
	}

	public override void OnActive()
	{
		LevelSelectionManager.currentState = LevelSelectionManager.selectionBlocks [LevelSelectionState.LevelSelection];
	}
}
