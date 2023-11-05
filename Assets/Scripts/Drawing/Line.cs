using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Line : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private EdgeCollider2D _collider;

    private const float DISTANCE_PRECISION = 0.1f;
    
    private List<Vector2> _linePoints = new List<Vector2>();

    public void SetLineLayerIndex(int index)
    {
        _lineRenderer.sortingOrder = index;
    }

    public void SetColor(Color drawColor)
    {
        _lineRenderer.startColor = drawColor;
        _lineRenderer.endColor = drawColor;
    }

    public void UpdateLine(Vector2 position)
    {
        if (_linePoints.Count < 1)
        {
            SetPoint(position);
            return;
        }

        if (Vector2.Distance(_linePoints.Last(), position) > DISTANCE_PRECISION)
        {
            SetPoint(position);
        }
    }

    private void SetPoint(Vector2 point)
    {
        _linePoints.Add(point);
        if (!_lineRenderer.enabled) _lineRenderer.enabled = true;

        _lineRenderer.positionCount = _linePoints.Count;
        _lineRenderer.SetPosition(_linePoints.Count - 1, point);
        _collider.points = _linePoints.ToArray();
    }
}