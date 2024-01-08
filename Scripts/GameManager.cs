using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        UiManager.OnPauseGame += PauseGame;
        UiManager.OnRestartGame += RestartGame;
    }

    private void PauseGame(bool state)
    {
        Time.timeScale = state ? Time.timeScale = 0 : Time.timeScale = 1;
    }

    private void RestartGame()
    {
        SceneManager.LoadScene("EscapeRoom");
    }
    //
}
