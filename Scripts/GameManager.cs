using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    [TextArea(1, 5)]
    [SerializeField] private string[] _clues;
    [SerializeField] private TextMeshPro _tvCluesText;
    [SerializeField] private bool[,] _puzzleItems = new bool[3,3];

    public static Action<bool> OnCompletion;
    public static Action OnAllPuzzlesComplete;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        UiManager.OnPauseGame += PauseGame;
        UiManager.OnRestartGame += RestartGame;
        UiManager.OnQuitGame += QuitGame;

        ChangeClue(0);
    }

    private void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

    private void PauseGame(bool state)
    {
        Time.timeScale = state ? Time.timeScale = 0 : Time.timeScale = 1;
    }

    private void RestartGame()
    {
        SceneManager.LoadScene("EscapeRoom");
    }
    
    private void OpenedExit()
    {
        OnCompletion?.Invoke(false);
    }

    private void ChangeClue(int clueNo)
    {
        _tvCluesText.text = _clues[clueNo];
    }

    internal void UpdatePuzzle(int puzzle, int clue, bool state = false)
    {
        Debug.Log($"Puzzle no: {puzzle}. Item no: {clue}. Placed: {state}");
        _puzzleItems[puzzle, clue] = state;
        var length = _puzzleItems.GetLength(puzzle);
        bool cluesComplete = true;
        bool allPuzzlesComplete = true;

        for (int i = 0; i < length; i++)
        {
            if (!_puzzleItems[puzzle, i])
            {
                cluesComplete = false;
            }
        }

        if (cluesComplete && allPuzzlesComplete)
        {
            OpenedExit();
        }
    }
}
