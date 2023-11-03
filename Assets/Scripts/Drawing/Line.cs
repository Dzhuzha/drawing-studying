using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Line : MonoBehaviour
{
    public event Action<GuidePoint> PointReached;

    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private EdgeCollider2D _collider;

    private List<Vector2> _linePoints = new List<Vector2>();

    private void SetPoint(Vector2 point)
    {
        _linePoints.Add(point);
        if (!_lineRenderer.enabled) _lineRenderer.enabled = true;

        _lineRenderer.positionCount = _linePoints.Count;
        _lineRenderer.SetPosition(_linePoints.Count - 1, point);
        _collider.points = _linePoints.ToArray();
    }

    public void UpdateLine(Vector2 position)
    {
        if (_linePoints.Count < 1)
        {
            SetPoint(position);
            return;
        }

        if (Vector2.Distance(_linePoints.Last(), position) > 0.1f)
        {
            SetPoint(position);
        }
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out SpellChecker spellChecker))
        {
            spellChecker.RegisterLine(this);
        }

        if (other.gameObject.TryGetComponent(out GuidePoint guidePoint))
        {
            PointReached?.Invoke(guidePoint);
        }
    }
}