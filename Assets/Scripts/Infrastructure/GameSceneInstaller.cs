using UnityEngine;
using Zenject;

public class GameSceneInstaller : MonoInstaller
{
    [SerializeField] private HintPresenter _hintPresenter;
    [SerializeField] private PointerVisualizer _pointerVisualizer;
    [SerializeField] private VoiceoverPlayer _voiceOverPlayer;
    [SerializeField] private LineGenerator _lineGenerator;
    [SerializeField] private TraceSymbolCreator _traceSymbolCreator;

    public override void InstallBindings()
    {
        BindPointerVisualizer();
        BindHintPresenter();
        BindLineGenerator();
        BindVoiceOverPlayer();
        BindTraceSymbolToDraw();
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