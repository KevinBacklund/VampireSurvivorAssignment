using UnityEngine;

public class GameplayState : State
{
    public override void EnterState()
    {
        base.EnterState();

        GameManager.gamePaused = false;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Instance.SwitchState<PausedState>();
        }
    }

}
