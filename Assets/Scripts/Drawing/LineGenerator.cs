using UnityEngine;

public class LineGenerator : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Line _line;
    [SerializeField] private SpellChecker _spellChecker;

    private Line _currentLine;
    private bool _isUponDrawSymbol;
    private DrawState _currentState;

    public void Init(SpellChecker spellChecker, Color colorToDraw)
    {
        _line.SetColor(colorToDraw);
        _spellChecker = spellChecker;
        _spellChecker.LineCompleted += FinishDrawing;
    }
    
    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        _spellChecker.LineCompleted -= FinishDrawing;
    }

    private void Update()
    {
        if (_spellChecker == null) return;

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
        _currentLine = null;
        _currentState = DrawState.DrawUnavailable;
    }

    private void InterruptDrawing()
    {
        if (_currentLine != null) Destroy(_currentLine.gameObject);
        _currentLine = null;
        _currentState = DrawState.DrawUnavailable;
    }

    private void Draw()
    {
        Vector2 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        _currentLine.UpdateLine(mousePosition);  
    }

    private void StartDrawing()
    {
        if (_currentLine == null) _currentLine = Instantiate(_line, transform, false);
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