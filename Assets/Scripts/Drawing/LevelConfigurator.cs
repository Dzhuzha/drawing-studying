using UnityEngine;

public class LevelConfigurator : MonoBehaviour
{
    [SerializeField] private TraceSymbolPresenter _traceSymbolPresenter;
    [SerializeField] private LineGenerator _lineGenerator;
    [SerializeField] private LevelLoader _levelLoader;
    [SerializeField] private LevelConfig _levelConfig;

    private void Start()
    {
        SpellChecker spellChecker = _traceSymbolPresenter.PlaceSymbolOnGamefield(_levelConfig.TracePrefab);
        _lineGenerator.Init(spellChecker, _levelConfig.ChosenColor);
        SelectNextLevel();
        spellChecker.SymbolCompleted += ChangeScene;
        StartCoroutine(_traceSymbolPresenter.ShowSymbol());
    }

    private void SelectNextLevel()
    {
        int levelIndex = _levelConfig.Colors.IndexOf(_levelConfig.ChosenColor);
        int nextLevelIndex = (levelIndex + 1) % _levelConfig.Colors.Count;
        Color nextColor = _levelConfig.Colors[nextLevelIndex];
        
        _levelConfig.InitLevel(_levelConfig, nextColor);
    }

    private void ChangeScene(SpellChecker spellChecker)
    {
        spellChecker.SymbolCompleted -= ChangeScene; //todo Винести звідси
        _levelLoader.ReloadScene();
    }
}