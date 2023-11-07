using UnityEngine;

public class CameraAdjuster : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _projectionSizeForIPad4 = 5f;
    [SerializeField] private float _projectionSize = 8f;

    private const float PHONE_MAX_ASPECT = 1.5f;

    private void Start()
    {
        bool isPhone = (float) Screen.width / Screen.height < PHONE_MAX_ASPECT;
        _camera.orthographicSize = isPhone ? _projectionSize : _projectionSizeForIPad4;
    }
}