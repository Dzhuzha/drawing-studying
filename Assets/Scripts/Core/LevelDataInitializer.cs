using System.Collections.Generic;
using UnityEngine;

public class LevelDataInitializer : MonoBehaviour
{
    [SerializeField] private List<GameContentInitializer> _gameContent = new List<GameContentInitializer>();
    [SerializeField] private PersistentData _persistentData;
    [SerializeField] private LevelLoader _levelLoader;
    [SerializeField] private int _gameSceneIndex = 1;
    
    private static bool _dataContainerSpawned = false;
    
    private void Awake()
    {
        if (!_dataContainerSpawned)
        {
            _persistentData = Instantiate(_persistentData);
            DontDestroyOnLoad(_persistentData);
            _dataContainerSpawned = true;
        }
        else
        {
            _persistentData = FindObjectOfType<PersistentData>();
        }
    }

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
        _persistentData.InitData(miniGameContent, chosenColor);
        _levelLoader.LoadScene(_gameSceneIndex);
    }
}