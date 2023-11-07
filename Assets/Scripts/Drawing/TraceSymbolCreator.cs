using UnityEngine;
using Zenject;

public class TraceSymbolCreator : MonoBehaviour
{
    private VoiceoverPlayer _voiceoverPlayer;
    private HintPresenter _hintPresenter;
    private LineGenerator _lineGenerator;
    private SpellChecker _symbolToDraw;
    private LevelConfig _levelConfig;
    private LevelLoader _levelLoader;

    [Inject]
    public void Construct(LevelConfig levelConfig, LevelLoader levelLoader, VoiceoverPlayer voiceoverPlayer, HintPresenter hintPresenter, LineGenerator lineGenerator)
    {
        _levelConfig = levelConfig;
        _levelLoader = levelLoader;
        _voiceoverPlayer = voiceoverPlayer;
        _hintPresenter = hintPresenter;
        _lineGenerator = lineGenerator;
    }

    private void Start()
    {
        _symbolToDraw = Instantiate(_levelConfig.TracePrefab);
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
}