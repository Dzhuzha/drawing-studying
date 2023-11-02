using UnityEngine;

public class LineGenerator : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Line _line;

    private Line _currentLine;
    
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (_currentLine == null) _currentLine = Instantiate(_line, transform);

            Vector2 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            _currentLine.UpdateLine(mousePosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            _currentLine = null;
        }
    }
}