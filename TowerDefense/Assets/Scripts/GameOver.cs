using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

    public Text roundsText;
    public SceneFader sceneFader;
    public string mainmenuScene = "MainMenu";

    private void OnEnable()
    {
        roundsText.text = PlayerStats.rounds.ToString();
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        WaveSpawner.EnemiesAlive = -1;
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        Time.timeScale = 1f;
        sceneFader.FadeTo(mainmenuScene); 
    }
}
