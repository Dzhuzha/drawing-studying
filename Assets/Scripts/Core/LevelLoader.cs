using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class LevelLoader : MonoBehaviour
{
    private SpellChecker _spellChecker;
    private LevelConfig _levelConfig;

    public void SubscribeToLevelComplete(SpellChecker spellChecker)
    {
        spellChecker.SymbolCompleted += ReloadScene;
    }
    
    [Inject]
    public void Construct(LevelConfig levelConfig)
    {
        _levelConfig = levelConfig;
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    private void ReloadScene(SpellChecker spellChecker)
    {
        spellChecker.SymbolCompleted -= ReloadScene;
        PrepareNextLevelConfig();
        StartCoroutine(ChangeScene());
    }

    private IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(_levelConfig.DelayBetweenSceneReload);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void PrepareNextLevelConfig()
    {
        if (_levelConfig.Colors.Count < 1) return;
        int levelIndex = _levelConfig.Colors.IndexOf(_levelConfig.ChosenColor);
        int nextLevelIndex = (levelIndex + 1) % _levelConfig.Colors.Count;
        Color nextColor = _levelConfig.Colors[nextLevelIndex];

        _levelConfig.InitLevel(_levelConfig, nextColor);
    }
}