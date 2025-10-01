using UnityEngine;

public class LevelUpState : State
{

    public GameObject player;

    public override void EnterState()
    {
        base.EnterState();
        GameManager.choosingUpgrade = true;
        GameManager.gamePaused = true;
        GameManager.Instance.levelUpScreen.SetActive(true);
        player.SendMessage("RemoveAndApplyUpgrades");
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (!GameManager.choosingUpgrade)
        {
            GameManager.Instance.SwitchState<GameplayState>();
        }
    }

    public override void ExitState()
    {
        base.ExitState();
        GameManager.gamePaused = false;
        GameManager.Instance.levelUpScreen.SetActive(false);
    }
}
