using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MinigameSettings", menuName = "Minigame/Create new Minigame Settings", order = 51)]
public class MiniGameContent : ScriptableObject
{
    [SerializeField] private string _title;
    [SerializeField] private List<Color> _colors = new List<Color>();
    [SerializeField] private List<Sprite> _levelSprites = new List<Sprite>();
    [SerializeField] private GameType _gameType;
    [SerializeField] private SpellChecker _tracePrefab;
    [SerializeField] private AudioClip _startPhrase;
    [SerializeField] private List<AudioClip> _motivationPhrases = new List<AudioClip>();
    [SerializeField] private int _gameSceneIndex;

    public string Title => _title;
    public int GameSceneIndex => _gameSceneIndex;
    public List<Color> Colors => _colors;
    public List<Sprite> LevelSprites => _levelSprites;
    public AudioClip StartPhrase => _startPhrase;
    public List<AudioClip> MotivationPhrases => _motivationPhrases;
    public SpellChecker TracePrefab => _tracePrefab;
    public GameType GameType => _gameType;
}

public enum GameType
{
    Letters,
    Numbers,
    Shapes
}