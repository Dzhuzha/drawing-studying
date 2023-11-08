using UnityEngine;
using Zenject;

public class MenuSceneInstaller : MonoInstaller
{
    [SerializeField] private LevelConfig _levelConfig;
    [SerializeField] private LevelLoader _levelLoader;

    public override void InstallBindings()
    {
        BindLevelConfig();
        CreateAdBindLevelLoader();
    }

    private void CreateAdBindLevelLoader()
    {
        LevelLoader levelLoader = Container.InstantiatePrefabForComponent<LevelLoader>(_levelLoader);
        Container.Bind<LevelLoader>().FromInstance(levelLoader).AsSingle();
    }

    private void BindLevelConfig()
    {
        Container.Bind<LevelConfig>().FromScriptableObject(_levelConfig).AsSingle();
    }
}