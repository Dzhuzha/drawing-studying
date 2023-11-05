using UnityEngine;

public class TraceSymbolCreator : MonoBehaviour
{
    [SerializeField] private LineGenerator _lineGenerator;
    [SerializeField] private HintPresenter _hintPresenter;
    [SerializeField] private VoiceoverPlayer _voiceoverPlayer;
    [SerializeField] private LevelConfig _levelConfig;
    [SerializeField] private LevelLoader _levelLoader;

    private readonly Vector2 _spawnPosition = Vector2.zero;
    private SpellChecker _symbolToDraw;

    private void Start()
    {
        CreateTraceSymbol(_levelConfig.TracePrefab);
        InitDependencies();
        ResolveSubscriptions();
    }

    private void InitDependencies()
    {
        _hintPresenter.Init(_symbolToDraw);
        _lineGenerator.Init(_symbolToDraw);
    }

    private void ResolveSubscriptions()
    {
        _symbolToDraw.SubscribeToFirstRulesAnnouncement(_voiceoverPlayer);
        _voiceoverPlayer.SubscribeToLevelComplete(_symbolToDraw);
        _levelLoader.SubscribeToLevelComplete(_symbolToDraw);
    }

    private void CreateTraceSymbol(SpellChecker symbolPrefab)
    {
        _symbolToDraw = Instantiate(symbolPrefab, _spawnPosition, Quaternion.identity);
    }
}