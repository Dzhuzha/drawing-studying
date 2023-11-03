using UnityEngine;

public class PersistentData : MonoBehaviour
{
    public MiniGameContent MiniGameContent { get; private set; }
    public Color ChosenColor { get; private set; }

    public void InitData(MiniGameContent miniGameContent, Color chosenColor)
    {
        MiniGameContent = miniGameContent;
        ChosenColor = chosenColor;
    }
}