using UnityEngine;

public class SettingsState : State
{
    public override void EnterState()
    {
        base.EnterState();
        GameManager.Instance.settingsScreen.SetActive(true);
    }
    public override void UpdateState()
    {
        base.UpdateState();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Instance.SwitchState<PausedState>();
        }
    }
    public override void ExitState()
    {
        base.ExitState();
        GameManager.Instance.settingsScreen.SetActive(false);
    }
}

