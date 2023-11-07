using UnityEngine;
using Zenject;

public class LineGenerator : MonoBehaviour
{
    [SerializeField] private Line _line;

    private const int BAKED_LINE_INDEX = 0;
    private const int DRAWING_LINE_INDEX = 3;

    private SpellChecker _spellChecker;
    private PointerVisualizer _pointer;
    private LevelConfig _levelConfig;
    private Camera _camera;
    private Line _currentLine;
    private bool _isUponDrawSymbol;
    private DrawState _currentState;

    public void Init(SpellChecker spellChecker)
    {
        _spellChecker = spellChecker;
        _spellChecker.LineCompleted += FinishDrawing;
    }

    [Inject]
    public void Construct(LevelConfig levelConfig, PointerVisualizer pointer)
    {
        _levelConfig = levelConfig;
        _pointer = pointer;
    }

    private void Start()
    {
        _camera = Camera.main;
        _line.SetColor(_levelConfig.ChosenColor);
        _line.SetLineLayerIndex(DRAWING_LINE_INDEX);
    }

    private void OnDestroy()
    {
        _spellChecker.LineCompleted -= FinishDrawing;
    }

    private void Update()
    {
        _isUponDrawSymbol = TryGetDrawSymbolUnderPointer();
        ManageDrawingState();
    }

    private void ManageDrawingState()
    {
        switch (_currentState)
        {
            case DrawState.CouldDraw:
                if (!_isUponDrawSymbol) _currentState = DrawState.DrawUnavailable;
                if (Input.GetMouseButton(0))
                    StartDrawing();
                break;
            case DrawState.Drawing:
                Draw();
                if (!_isUponDrawSymbol || Input.GetMouseButtonUp(0)) _currentState = DrawState.Interrupted;
                break;
            case DrawState.Interrupted:
                InterruptDrawing();
                break;
            case DrawState.DrawUnavailable:
                if (_isUponDrawSymbol) _currentState = DrawState.CouldDraw;
                break;
        }
    }

    private void FinishDrawing()
    {
        _currentLine.SetLineLayerIndex(BAKED_LINE_INDEX);
        _currentLine = null;
        _currentState = DrawState.DrawUnavailable;
        _pointer.gameObject.SetActive(false);
    }

    private void InterruptDrawing()
    {
        if (_currentLine != null) Destroy(_currentLine.gameObject);
        _currentLine = null;
        _currentState = DrawState.DrawUnavailable;
        _pointer.gameObject.SetActive(false);
    }

    private void Draw()
    {
        Vector2 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        _currentLine.UpdateLine(mousePosition);
        _pointer.transform.position = mousePosition;
    }

    private void StartDrawing()
    {
        if (_currentLine == null) _currentLine = Instantiate(_line, transform, false);
        _pointer.gameObject.SetActive(true);
        _currentState = DrawState.Drawing;
    }

    private bool TryGetDrawSymbolUnderPointer()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

        return _isUponDrawSymbol = hit.collider != null && hit.collider.TryGetComponent(out SpellChecker spellChecker);
    }

    private enum DrawState
    {
        CouldDraw,
        Drawing,
        Interrupted,
        DrawUnavailable
    }
}