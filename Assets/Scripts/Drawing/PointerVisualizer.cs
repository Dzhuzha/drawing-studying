using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PointerVisualizer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _didiIcon;

    private void OnDisable()
    {
        transform.position = new Vector2(100, 100);
    }
}