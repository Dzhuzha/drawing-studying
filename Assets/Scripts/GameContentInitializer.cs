using UnityEngine;
using TMPro;

public class GameContentInitializer : MonoBehaviour
{
    [SerializeField] private MiniGameContent _miniGameContent;
    [SerializeField] private LevelIcon _levelIconPrefab;
    [SerializeField] private Transform _iconContainer;
    [SerializeField] private TMP_Text _titleLabel;


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
            newIcon.Init(_miniGameContent.GameType, _miniGameContent.LevelSprites[i], _miniGameContent.Colors[i]);
        }
    }
}