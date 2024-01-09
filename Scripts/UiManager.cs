using System;
using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public static Action OnLeftButton;
    public static Action OnRightButton;
    public static Action<bool> OnPauseGame;
    public static Action OnRestartGame;
    public static Action OnQuitGame;

    [SerializeField] private KeyCode _pauseKey = KeyCode.P;
    [SerializeField] private Canvas _pauseMenu;
    [SerializeField] private Canvas _fadeInCanvas;
    [SerializeField] private GameObject _quitButton;

    private void Start()
    {
        ToggleFadeInCanvas(true);

        GameManager.OnCompletion += ToggleFadeInCanvas;
    }

    private void Update()
    {
        if (Input.GetKeyDown(_pauseKey))
        {
            OnPauseGame?.Invoke(Time.timeScale != 0);
        }
    }

    private void ToggleFadeInCanvas(bool state)
    {
        _fadeInCanvas.transform.gameObject.SetActive(true);

        _fadeInCanvas.GetComponent<Animator>().SetBool("ScreenFade", state);
        
        if (!state)
        {
            _fadeInCanvas.GetComponentInChildren<TextMeshProUGUI>().text = "You escaped this time...";
            //_quitButton.SetActive(true);
        }
    }

    public void TogglePauseMenuCanvas()
    {
        _pauseMenu.gameObject.SetActive(!_pauseMenu.gameObject.activeSelf);
    }

    public void RestartGameButton()
    {
        OnRestartGame?.Invoke();
    }

    public void LeftDirectionButton()
    {
        OnLeftButton?.Invoke();
    }

    public void RightDirectionButton()
    {
        OnRightButton?.Invoke();
    }

    public void QuitGameButton()
    {
        OnQuitGame?.Invoke();
    }
}
