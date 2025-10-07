using UnityEngine;

public class GameOverState : State
{
    public override void EnterState()
    {
        base.EnterState();

        GameManager.gamePaused = true;
        GameManager.Instance.saveManager.SetHighscore(GameManager.currentScore);
        GameManager.currentScore = 0;
        GameManager.Instance.gameOverScreen.SetActive(true);
    }

    public override void ExitState()
    {
        base.ExitState();

        GameManager.Instance.gameOverScreen.SetActive(false);
    }
}
