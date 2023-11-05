using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelIcon : MonoBehaviour
{
    public event Action<Color> LevelChosen;

    [SerializeField] private Image _image;
    [SerializeField] private Button _button;

    private LevelLoader _levelLoader;

    private void Start()
    {
        _button.onClick.AddListener(SendClickedLevelInfo);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(SendClickedLevelInfo);
    }

    private void SendClickedLevelInfo()
    {
        LevelChosen?.Invoke(_image.color);
    }

    public void Init(Sprite levelIcon, Color iconColor)
    {
        _image.sprite = levelIcon;
        _image.color = iconColor;
    }
}