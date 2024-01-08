using System;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public static Action OnLeftButton;
    public static Action OnRightButton;
    public static Action<bool> OnPauseGame;
    public static Action OnRestartGame;

    [SerializeField] private KeyCode _pauseKey = KeyCode.P;
    [SerializeField] private Canvas _pauseMenu;

    private void Update()
    {
        if (Input.GetKeyDown(_pauseKey))
        {
            OnPauseGame?.Invoke(Time.timeScale != 0);
        }
    }

    public void TogglePauseMenuButton()
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

}
