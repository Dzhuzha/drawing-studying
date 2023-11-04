using System.Collections.Generic;
using UnityEngine;

public class LevelDataInitializer : MonoBehaviour
{
    [SerializeField] private List<GameContentInitializer> _gameContent = new List<GameContentInitializer>();
    [SerializeField] private LevelLoader _levelLoader;
    [SerializeField] private int _gameSceneIndex = 1;
    [SerializeField] private LevelConfig _levelConfig;

    private void OnEnable()
    {
        foreach (GameContentInitializer content in _gameContent)
        {
          content.LevelTriggered += LoadTraceLevel;
        }
    }

    private void OnDisable()
    {
        foreach (GameContentInitializer content in _gameContent)
        {
            content.LevelTriggered -= LoadTraceLevel;
        }
    }

    private void LoadTraceLevel(MiniGameContent miniGameContent, Color chosenColor)
    {
        _levelConfig.InitLevel(miniGameContent, chosenColor);
        _levelLoader.LoadScene(_gameSceneIndex);
    }
}