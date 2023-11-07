using System.Collections;
using UnityEngine;
using Zenject;

public class HintPresenter : MonoBehaviour
{
    [SerializeField] private GameObject _hint;

    private const float DISTANCE_PRECISION = 0.01f;
    private const int FIRST_POINT_INDEX = 0;

    private SpellChecker _spellChecker;
    private LevelConfig _levelConfig;
    private Coroutine _pathHint;
    private int _currentPointIndex = FIRST_POINT_INDEX;
    private float _hintSpeed;
    private float _hintTimer;

    public void Init(SpellChecker spellChecker)
    {
        _spellChecker = spellChecker;
    }

    [Inject]
    public void Construct(LevelConfig levelConfig)
    {
        _levelConfig = levelConfig;
    }

    private void Start()
    {
        _hintTimer = _levelConfig.TimeToShowHint;
        _hintSpeed = _levelConfig.TutorialMovementSpeed;
        _hint = Instantiate(_hint, transform);
        _hint.SetActive(false);
    }

    private void ShowHint()
    {
        _pathHint = StartCoroutine(GoThroughThePass());
    }

    private void HideHint()
    {
        if (_pathHint == null) return;

        StopCoroutine(_pathHint);
        _hint.SetActive(false);
        _pathHint = null;
    }

    private void Update()
    {
        CheckPlayerActivity();
    }

    private void RunTimer()
    {
        if (Input.anyKey)
        {
            _hintTimer = _levelConfig.TimeToShowHint;
            HideHint();
        }
        else
        {
            _hintTimer -= Time.deltaTime;
        }
    }

    private void CheckPlayerActivity()
    {
        RunTimer();

        if (_hintTimer > 0) return;

        _hintTimer = _levelConfig.TimeToShowHint;
        ShowHint();
    }

    private IEnumerator GoThroughThePass()
    {
        _currentPointIndex = FIRST_POINT_INDEX;
        _hint.transform.position = _spellChecker.ActiveGuideLine.GetPosition(_currentPointIndex);
        _hint.SetActive(true);

        while (_currentPointIndex < _spellChecker.ActiveGuideLine.PointsCount)
        {
            Vector2 targetPosition = _spellChecker.ActiveGuideLine.GetPosition(_currentPointIndex);

            while (Vector2.Distance(_hint.transform.position, targetPosition) > DISTANCE_PRECISION)
            {
                _hint.transform.position = Vector2.MoveTowards(_hint.transform.position, targetPosition, _hintSpeed * Time.deltaTime);
                yield return null;
            }

            _currentPointIndex++;
        }

        _hint.SetActive(false);
    }
}