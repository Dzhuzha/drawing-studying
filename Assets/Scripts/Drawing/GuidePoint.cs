using System;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class GuidePoint : MonoBehaviour
{
    public event Action<GuidePoint> PointReached;
    
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private Sprite _defaultPointSprite;
    [SerializeField] private Sprite _finalPointSprite;

    [SerializeField] private float _defaultPointScale;
    [SerializeField] private float _finalPointScale;
    [SerializeField] private float _defaultPoint;
    [SerializeField] private float _finalPointRadius;

    private const string APPEARANCE_ANIMATION = "GuidePointAnimation";

    public void Init(bool finishPoint)
    {
        if (finishPoint)
        {
            _renderer.sprite = _finalPointSprite;
            transform.localScale = new Vector2(_finalPointScale, _finalPointScale);
            GetComponent<CircleCollider2D>().radius = _finalPointRadius;
        }
        else
        {
            _renderer.sprite = _defaultPointSprite;
            transform.localScale = new Vector2(_defaultPointScale, _defaultPointScale);
            GetComponent<CircleCollider2D>().radius = _defaultPoint;
        }
    }

    public void PlayAnimation()
    {
        GetComponent<Animator>().Play(APPEARANCE_ANIMATION);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out Line line))
        {
            PointReached?.Invoke(this);
        }
    }
}