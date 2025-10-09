using UnityEngine;

public class SettingsState : State
{
    public override void EnterState()
    {
        base.EnterState();
        GameManager.Instance.settingsScreen.SetActive(true);
    }
    public override void ExitState() 
    { 
        base.ExitState();
        GameManager.Instance.settingsScreen.SetActive(false);
    }
}
