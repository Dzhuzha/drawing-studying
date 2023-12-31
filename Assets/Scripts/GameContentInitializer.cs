using UnityEngine;
using TMPro;

public class GameContentInitializer : MonoBehaviour
{
    [SerializeField] private MiniGameContent _miniGameContent;
    [SerializeField] private LevelIcon _levelIconPrefab;
    [SerializeField] private Transform _iconContainer;
    [SerializeField] private TMP_Text _titleLabel;
    [SerializeField] private LevelConfig _levelConfig;
    [SerializeField] private LevelLoader _levelLoader;

    private void Start()
    {
        CreateGameIcons();
    }

    private void CreateGameIcons()
    {
        _titleLabel.text = _miniGameContent.Title;
        _titleLabel.enabled = true;

        for (int i = 0; i < _miniGameContent.LevelSprites.Count; i++)
        {
            LevelIcon newIcon = Instantiate(_levelIconPrefab, _iconContainer);
            newIcon.Init(_miniGameContent.LevelSprites[i], _miniGameContent.Colors[i]);
            SubscribeToButton(newIcon);
        }
    }

    private void OnLevelChosen(Color chosenColor)
    {
        UnsubscribeFromAllButtons();
        _levelConfig.InitLevel(_miniGameContent, chosenColor);
        _levelLoader.LoadScene(_miniGameContent.GameSceneIndex);
    }

    private void SubscribeToButton(LevelIcon levelIcon)
    {
        levelIcon.LevelChosen += OnLevelChosen;
    }

    private void UnsubscribeFromAllButtons()
    {
        for (int i = 0; i < _iconContainer.childCount; i++)
        {
            if (_iconContainer.TryGetComponent(out LevelIcon levelIcon))
            {
                levelIcon.LevelChosen -= OnLevelChosen;
            }
        }
    }
}