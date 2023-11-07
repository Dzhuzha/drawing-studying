using UnityEngine;

public class CameraAdjuster : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _projectionSizeForIPad4 = 8f;
    [SerializeField] private float _projectionSize = 5f;

    private const float PHONE_MAX_ASPECT = 1.5f;

    private void Start()
    {
        bool isTab = (float) Screen.width / Screen.height < PHONE_MAX_ASPECT;
        _camera.orthographicSize = isTab ? _projectionSizeForIPad4 : _projectionSize;
    }
}