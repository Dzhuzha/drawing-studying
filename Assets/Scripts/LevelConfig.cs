using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "Minigame/Create new Level config", order = 52)]
public class LevelConfig : ScriptableObject
{
    public SpellChecker TracePrefab { get; private set; }
    public List<Color> Colors { get; private set; }
    public Color ChosenColor { get; private set; }
    public GameType GameType { get; private set; }
    public AudioClip StartPhrase { get; private set; }
    public List<AudioClip> MotivationPhrases { get; private set; }

    [SerializeField] private int _homeSceneIndex;
    [SerializeField] private float _timeToShowHint;
    [SerializeField] private float _timeToRepeatRules;

    public int HomeSceneIndex => _homeSceneIndex;
    public float TimeToShowHint => _timeToShowHint;
    public float TimeToRepeatRules => _timeToRepeatRules;

    public void InitLevel(MiniGameContent miniGameContent, Color chosenColor)
    {
        TracePrefab = miniGameContent.TracePrefab;
        Colors = miniGameContent.Colors;
        ChosenColor = chosenColor;
        GameType = miniGameContent.GameType;
        StartPhrase = miniGameContent.StartPhrase;
        MotivationPhrases = miniGameContent.MotivationPhrases;
    }

    public void InitLevel(LevelConfig levelConfig, Color chosenColor)
    {
        TracePrefab = levelConfig.TracePrefab;
        Colors = levelConfig.Colors;
        ChosenColor = chosenColor;
        GameType = levelConfig.GameType;
        StartPhrase = levelConfig.StartPhrase;
        MotivationPhrases = levelConfig.MotivationPhrases;
    }
}