using UnityEngine;
using Zenject;

public class GameSceneInstaller : MonoInstaller
{
    [SerializeField] private HintPresenter _hintPresenter;
    [SerializeField] private PointerVisualizer _pointerVisualizer;
    [SerializeField] private VoiceoverPlayer _voiceOverPlayer;
    [SerializeField] private LineGenerator _lineGenerator;
    [SerializeField] private TraceSymbolCreator _traceSymbolCreator;
    [SerializeField] private LevelConfig _levelConfig;
    [SerializeField] private LevelLoader _levelLoader;

    public override void InstallBindings()
    {
        BindLevelConfig();
        CreateAndBindLevelLoader();
        BindPointerVisualizer();
        BindHintPresenter();
        BindLineGenerator();
        BindVoiceOverPlayer();
        BindTraceSymbolToDraw();
    }
    
    private void CreateAndBindLevelLoader()
    {
        LevelLoader levelLoader = Container.InstantiatePrefabForComponent<LevelLoader>(_levelLoader);
        Container.Bind<LevelLoader>().FromInstance(levelLoader).AsSingle();
    }

    private void BindLevelConfig()
    {
        Container.Bind<LevelConfig>().FromScriptableObject(_levelConfig).AsSingle();
    }

    private void BindTraceSymbolToDraw()
    {
        TraceSymbolCreator traceSymbolCreator = Container.InstantiatePrefabForComponent<TraceSymbolCreator>(_traceSymbolCreator);
        Container.Bind<TraceSymbolCreator>().FromInstance(traceSymbolCreator).AsSingle();
    }

    private void BindPointerVisualizer()
    {
        PointerVisualizer pointerVisualizer = Container.InstantiatePrefabForComponent<PointerVisualizer>(_pointerVisualizer);
        Container.Bind<PointerVisualizer>().FromInstance(pointerVisualizer).AsSingle();
    }

    private void BindLineGenerator()
    {
        LineGenerator lineGenerator = Container.InstantiatePrefabForComponent<LineGenerator>(_lineGenerator);
        Container.Bind<LineGenerator>().FromInstance(lineGenerator).AsSingle();
    }

    private void BindHintPresenter()
    {
        HintPresenter hintPresenter = Container.InstantiatePrefabForComponent<HintPresenter>(_hintPresenter);
        Container.Bind<HintPresenter>().FromInstance(hintPresenter).AsSingle();
    }

    private void BindVoiceOverPlayer()
    {
        VoiceoverPlayer voiceOverPlayer = Container.InstantiatePrefabForComponent<VoiceoverPlayer>(_voiceOverPlayer);
        Container.Bind<VoiceoverPlayer>().FromInstance(voiceOverPlayer).AsSingle();
    }
}