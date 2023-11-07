using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MenuButton : MonoBehaviour
{
    [SerializeField] private Button _button;

    private LevelConfig _levelConfig;
    private LevelLoader _levelLoader;

    [Inject]
    public void Construct(LevelConfig levelConfig, LevelLoader levelLoader)
    {
        _levelConfig = levelConfig;
        _levelLoader = levelLoader;
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(() => _levelLoader.LoadScene(_levelConfig.HomeSceneIndex));
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(() => _levelLoader.LoadScene(_levelConfig.HomeSceneIndex));
    }
}