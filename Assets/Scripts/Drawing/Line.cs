using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Line : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;

    private List<Vector2> _linePoints = new List<Vector2>();

    private void SetPoint(Vector2 point)
    {
        _linePoints.Add(point);

        _lineRenderer.positionCount = _linePoints.Count;
        _lineRenderer.SetPosition(_linePoints.Count - 1, point);
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
}