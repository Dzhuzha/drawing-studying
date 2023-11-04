using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GuideLine : MonoBehaviour
{
    public event Action LineFinished;
    
    [SerializeField] private List<GuidePoint> _guidePoints = new List<GuidePoint>();
    [SerializeField] private bool _isClosedCircle = false;

    private GuidePoint _nextGoalPoint = null;
    private Line _currentLine = null;
    private const float GUIDE_POINT_GIZMO_RADIUS = 0.3f;
    
    public int PointsCount => _guidePoints.Count;

    private void Start() // todo StateMachine аби рейс кондішн попередити? Awake => Start було
    {
        _guidePoints = GetComponentsInChildren<GuidePoint>().ToList();

        foreach (GuidePoint guide in _guidePoints)
        {
            guide.Init(ReferenceEquals(guide, _guidePoints[^1]));
        }

        _nextGoalPoint = _guidePoints[0];
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        foreach (GuidePoint guidePoint in _guidePoints)
        {
            guidePoint.PlayAnimation();
            guidePoint.PointReached += CheckReachedPoint;
        }
    }

    private void CheckReachedPoint(GuidePoint reachedPoint)
    {
        if (ReferenceEquals(_nextGoalPoint, reachedPoint))
        {
            int nextPointIndex = _guidePoints.IndexOf(reachedPoint) + 1;

            if (nextPointIndex < _guidePoints.Count)
            {
                _nextGoalPoint = _guidePoints[nextPointIndex];
            }
            else
            {
                FinishLine();
            }
        }
        else
        {
            ResetProgress();
        }
    }

    private void ResetProgress()
    {
        _nextGoalPoint = _guidePoints[0];
    }

    private void FinishLine()
    {
        foreach (GuidePoint guidePoint in _guidePoints)
        {
            guidePoint.PointReached -= CheckReachedPoint;
        }
        
        LineFinished?.Invoke();
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            Vector2 startPosition = GetPosition(i);
            Gizmos.DrawSphere(startPosition, GUIDE_POINT_GIZMO_RADIUS);

            if (i == transform.childCount - 1 && !_isClosedCircle) continue;

            Vector2 finishPosition = GetPosition(GetNextIndex(i));
            Gizmos.DrawLine(startPosition, finishPosition);
        }
    }

    public Vector2 GetPosition(int index)
    {
        return transform.GetChild(index).position;
    }

    public int GetNextIndex(int index)
    {
        int newIndex = index + 1;
        return newIndex < transform.childCount ? newIndex : 0;
    }
}