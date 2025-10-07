using UnityEngine;

public class PausedState : State
{

    public override void EnterState()
    {
        base.EnterState();

        GameManager.gamePaused = true;
        GameManager.Instance.pauseScreen.SetActive(true);
    }
    public override void UpdateState()
    {
        base.UpdateState();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Instance.SwitchState<GameplayState>();
        }
    }

    public override void ExitState()
    {
        base.ExitState();
        GameManager.Instance.pauseScreen.SetActive(false);
        GameManager.gamePaused = false;
    }
}
