using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MinigameSettings", menuName = "Minigame/Create new Minigame Settings", order = 51)]
public class MiniGameContent : ScriptableObject
{
    [SerializeField] private string _title;
    [SerializeField] private List<Color> _colors = new List<Color>();
    [SerializeField] private List<Sprite> _levelSprites = new List<Sprite>();
    [SerializeField] private GameType _gameType;

    public string Title => _title;
    public List<Color> Colors => _colors;
    public List<Sprite> LevelSprites => _levelSprites;
    public GameType GameType => _gameType;
}

public enum GameType
{
    Letters,
    Numbers,
    Shapes
}