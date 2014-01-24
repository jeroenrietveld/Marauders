using UnityEngine;
using System.Linq;
using System.Collections;
using XInputDotNetPure;

public class LevelState : MenuStateBase
{
    private GamePad _controller;

	public LevelState()
	{
        center = GameObject.Find("LevelScreen").transform.position;

        _controller = ControllerInput.GetController(PlayerIndex.One);
        _controller.deadZone = GamePadDeadZone.IndependentAxes;
	}

	public override void Update(MenuManager manager)
	{
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
	}

	public override void OnActive()
	{
		LevelSelectionManager.currentState = LevelSelectionManager.selectionBlocks [LevelSelectionState.LevelSelection];
	}
}
