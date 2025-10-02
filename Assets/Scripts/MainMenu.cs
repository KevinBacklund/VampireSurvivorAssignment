using TMPro;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] SaveManager saveManager; 
    [SerializeField] TextMeshProUGUI highScoreText; 
    private void Start()
    {
        saveManager.LoadData();
        highScoreText.text = "Highscore  - " + saveManager.GetHighscore.ToString();
        SoundManager.PlayBgm(Bgm.mainMenu);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}