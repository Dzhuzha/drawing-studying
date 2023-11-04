using System;
using System.Collections;
using UnityEngine;

public class GamingSequenceController : MonoBehaviour
{
    public event Action<LevelState> LevelInitialized;

    [SerializeField] private TraceSymbolPresenter _traceSymbolPresenter;
    [SerializeField] private LineGenerator _lineGenerator;
    [SerializeField] private LevelLoader _levelLoader;
    [SerializeField] private LevelConfig _levelConfig;
    [SerializeField] private VoiceoverPlayer _voiceoverPlayer;

    private float _timeToRepeatRules = 7f;
    private float _timer;
    
    public LevelState CurrentState { get; private set; } = LevelState.Initialization;

    private void Awake()
    {
        InitializeState();
        _timer = _timeToRepeatRules;
    }

    private void InitializeState()
    {
        SpellChecker spellChecker = _traceSymbolPresenter.PlaceSymbolOnGamefield(_levelConfig.TracePrefab);
        _lineGenerator.Init(spellChecker, _levelConfig.ChosenColor);
        SelectNextLevel();
        spellChecker.SymbolCompleted += OnLevelComplete;
        _voiceoverPlayer.Init(_levelConfig.MotivationPhrases, _levelConfig.StartPhrase);
        LevelInitialized?.Invoke(CurrentState);
        //    StartCoroutine(_traceSymbolPresenter.ShowSymbol());
        _traceSymbolPresenter.ShowSymbol();
        ChangeLevelState(LevelState.LevelPresentation);
    }

    private void Update()
    {
        switch (CurrentState)
        {
            case LevelState.LevelPresentation:      
                _traceSymbolPresenter.ActivateFirstGuideLine();
                _voiceoverPlayer.PlayLevelRulesPhrase();
                ChangeLevelState(LevelState.Game);
                break;
            case LevelState.Game:
                CheckPlayerActivity();
                break;
            case LevelState.Congratulation:
                _voiceoverPlayer.PlayRandomMotivationPhrase();
                StartCoroutine(ChangeScene());
                ChangeLevelState(LevelState.WaitForSceneReload);
                break;
        }
    }

    private void CheckPlayerActivity()
    {
        if (Input.anyKeyDown)
        {
            _timer = _timeToRepeatRules;
        }
        else
        {
            _timer -= Time.deltaTime;
        }
        
        if (_timer < 0)
        {
            _timer = _timeToRepeatRules;
            _voiceoverPlayer.PlayLevelRulesPhrase();
        }
    }

    private void OnLevelComplete(SpellChecker spellChecker)
    {
        spellChecker.SymbolCompleted -= OnLevelComplete;
        ChangeLevelState(LevelState.Congratulation);
    }

    private void ChangeLevelState(LevelState newState)
    {
        CurrentState = newState;
    }

    private void SelectNextLevel()
    {
        int levelIndex = _levelConfig.Colors.IndexOf(_levelConfig.ChosenColor);
        int nextLevelIndex = (levelIndex + 1) % _levelConfig.Colors.Count;
        Color nextColor = _levelConfig.Colors[nextLevelIndex];

        _levelConfig.InitLevel(_levelConfig, nextColor);
    }

    private IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(1f);
     //   spellChecker.SymbolCompleted -= ChangeScene; //todo Винести звідси
        _levelLoader.ReloadScene();
    }

    public enum LevelState
    {
        Initialization,
        LevelPresentation,
        Game,
        Congratulation,
        WaitForSceneReload
    }
}