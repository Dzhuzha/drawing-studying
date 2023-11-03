using UnityEngine;

public class PersistentData : MonoBehaviour
{
    private MiniGameContent _miniGameContent;
    private Color _chosenColor;
    
    public void InitData(MiniGameContent miniGameContent, Color chosenColor)
    {
        _miniGameContent = miniGameContent;
        _chosenColor = chosenColor;
    }
}