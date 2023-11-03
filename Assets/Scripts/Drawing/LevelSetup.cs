using UnityEngine;

public class LevelSetup : MonoBehaviour
{
    [SerializeField] private TraceSymbolPresenter _traceSymbolPresenter;
    [SerializeField] private LineGenerator _lineGenerator;
    [SerializeField] private LevelLoader _levelLoader;

    private PersistentData _persistentData;

    private void Start()
    {
        _persistentData = FindObjectOfType<PersistentData>();
        SpellChecker spellChecker = _traceSymbolPresenter.PlaceSymbolOnGamefield(_persistentData.MiniGameContent.TracePrefab);
        _lineGenerator.Init(spellChecker, _persistentData.ChosenColor);
        SelectNextLevel();
        spellChecker.SymbolCompleted += ChangeScene;
        StartCoroutine(_traceSymbolPresenter.ShowSymbol());
    }

    private void SelectNextLevel()
    {
        int levelIndex = _persistentData.MiniGameContent.Colors.IndexOf(_persistentData.ChosenColor);
        int nextLevelIndex = (levelIndex + 1) % _persistentData.MiniGameContent.Colors.Count;
        Color nextColor = _persistentData.MiniGameContent.Colors[nextLevelIndex];
        
        _persistentData.InitData(_persistentData.MiniGameContent, nextColor);
    }

    private void ChangeScene(SpellChecker spellChecker)
    {
        spellChecker.SymbolCompleted -= ChangeScene; //todo Винести звідси
        _levelLoader.ReloadScene();
    }
}