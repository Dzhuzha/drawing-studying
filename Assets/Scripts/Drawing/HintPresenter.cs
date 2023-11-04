using System.Collections;
using UnityEngine;

public class HintPresenter : MonoBehaviour
{
    [SerializeField] private SpellChecker _spellChecker;
    [SerializeField] private GameObject _hint;
    [SerializeField] private float _hintSpeed;

    private Coroutine _pathHint;
    private int _currentPointIndex = 0;

    public void Init(SpellChecker spellChecker)
    {
        _spellChecker = spellChecker;
        _hint = Instantiate(_hint);
        _hint.SetActive(false);
        _hint.transform.position = _spellChecker.ActiveGuideLine.GetPosition(_currentPointIndex);
    }

    public void ShowHint()
    {
        _pathHint = StartCoroutine(GoThroughThePass());
    }

    public void HideHint()
    {
        if (_pathHint == null) return;
        
        StopCoroutine(_pathHint);
        _hint.SetActive(false);
        _pathHint = null;
    }

    private IEnumerator GoThroughThePass()
    {
        _currentPointIndex = 0;
        _hint.transform.position = _spellChecker.ActiveGuideLine.GetPosition(_currentPointIndex);
        _hint.SetActive(true);

        while (_currentPointIndex < _spellChecker.ActiveGuideLine.PointsCount)
        {
            Vector2 targetPosition = _spellChecker.ActiveGuideLine.GetPosition(_currentPointIndex);
        
            while (Vector2.Distance(_hint.transform.position, targetPosition) > 0.01f)
            {
                _hint.transform.position = Vector2.MoveTowards(_hint.transform.position, targetPosition, _hintSpeed * Time.deltaTime);
                yield return null;
            }
        
            _currentPointIndex++;
        }

        _hint.SetActive(false);
    }
}