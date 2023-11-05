using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpellChecker : MonoBehaviour
{
    public event Action<SpellChecker> SymbolCompleted;
    public event Action LineCompleted;

    [SerializeField] private List<GuideLine> _guideLines = new List<GuideLine>();

    public GuideLine ActiveGuideLine { get; private set; }

    public void Init()
    {
        _guideLines = GetComponentsInChildren<GuideLine>().ToList();

        foreach (var guideLine in _guideLines)
        {
            guideLine.Init();
        }
    }

    public void ActivateFirstGuideLine()
    {
        ActivateGuideline(0);
    }

    private void ActivateGuideline(int index)
    {
        ActiveGuideLine = _guideLines[index];
        _guideLines[index].gameObject.SetActive(true);
        ActiveGuideLine.LineFinished += SetupNextGuideline;
    }

    private void SetupNextGuideline()
    {
        ActiveGuideLine.LineFinished -= SetupNextGuideline;
        ActiveGuideLine.gameObject.SetActive(false);
        LineCompleted?.Invoke();

        int nextGuidelineIndex = _guideLines.IndexOf(ActiveGuideLine) + 1;

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