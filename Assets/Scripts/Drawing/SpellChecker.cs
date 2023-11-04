using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpellChecker : MonoBehaviour
{
    public event Action<SpellChecker> SymbolCompleted;
    public event Action LineCompleted;

    [SerializeField] private List<GuideLine> _guideLines;

    private GuideLine _activeGuideLine = null;

    public GuideLine ActiveGuideLine => _activeGuideLine;

    private void Awake()
    {
        _guideLines = GetComponentsInChildren<GuideLine>().ToList();
        _activeGuideLine = _guideLines[0];
    }

    public void ActivateFirstGuideLine()
    {
        ActivateGuideline(0);
    }

    private void ActivateGuideline(int index)
    {
        _activeGuideLine = _guideLines[index];
        _guideLines[index].gameObject.SetActive(true);
        _activeGuideLine.LineFinished += SetupNextGuideline;
    }

    private void SetupNextGuideline()
    {
        _activeGuideLine.LineFinished -= SetupNextGuideline;
        _activeGuideLine.gameObject.SetActive(false);
        LineCompleted?.Invoke();

        int nextGuidelineIndex = _guideLines.IndexOf(_activeGuideLine) + 1;

        if (nextGuidelineIndex < _guideLines.Count)
        {
            ActivateGuideline(nextGuidelineIndex);
        }
        else
        {
            SymbolCompleted?.Invoke(this);
        }
    }
}