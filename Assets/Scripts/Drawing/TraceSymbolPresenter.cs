using UnityEngine;

public class TraceSymbolPresenter : MonoBehaviour
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
        _hintPresenter.Init(_symbolToDraw);
        _lineGenerator.Init(_symbolToDraw);
        _voiceoverPlayer.SubscribeToLevelComplete(_symbolToDraw);
        _levelLoader.SubscribeToLevelComplete(_symbolToDraw);
        ShowSymbol();
    }

    private void CreateTraceSymbol(SpellChecker symbolPrefab)
    {
        _symbolToDraw = Instantiate(symbolPrefab, _spawnPosition, Quaternion.identity);
        _symbolToDraw.Init();
        _symbolToDraw.gameObject.SetActive(false);
    }

    private void ShowSymbol()
    {
        _symbolToDraw.gameObject.SetActive(true);
        ActivateFirstGuideLine();
    }

    private void ActivateFirstGuideLine()
    {
        _symbolToDraw.ActivateFirstGuideLine();
    }
}