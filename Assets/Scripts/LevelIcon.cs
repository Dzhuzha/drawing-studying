using UnityEngine;
using UnityEngine.UI;

public class LevelIcon : MonoBehaviour
{
    [SerializeField] private Image _image;

    private GameType _gameType;

    public void Init(GameType gameType, Sprite levelIcon, Color iconColor)
    {
        _image.sprite = levelIcon;
        _image.color = iconColor;
        _gameType = gameType;
    }
}