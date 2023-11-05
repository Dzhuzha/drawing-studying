using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private LevelConfig _levelConfig;
    [SerializeField] private Button _mainMenuButton;

    private SpellChecker _spellChecker;

    private void Start()
    {
        PrepareNextLevelConfig();
    }

    private void OnEnable()
    {
        if (SceneManager.GetActiveScene().buildIndex == _levelConfig.HomeSceneIndex) return;
        _mainMenuButton.onClick.AddListener(() => LoadScene(_levelConfig.HomeSceneIndex));
    }

    private void OnDisable()
    {
        if (SceneManager.GetActiveScene().buildIndex == _levelConfig.HomeSceneIndex) return;
        _mainMenuButton.onClick.RemoveListener(() => LoadScene(_levelConfig.HomeSceneIndex));
    }

    public void SubscribeToLevelComplete(SpellChecker spellChecker)
    {
        spellChecker.SymbolCompleted += ReloadScene;
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    private void ReloadScene(SpellChecker spellChecker)
    {
        spellChecker.SymbolCompleted -= ReloadScene;
        StartCoroutine(ChangeScene());
    }

    private IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void PrepareNextLevelConfig()
    {
        int levelIndex = _levelConfig.Colors.IndexOf(_levelConfig.ChosenColor);
        int nextLevelIndex = (levelIndex + 1) % _levelConfig.Colors.Count;
        Color nextColor = _levelConfig.Colors[nextLevelIndex];

        _levelConfig.InitLevel(_levelConfig, nextColor);
    }
}