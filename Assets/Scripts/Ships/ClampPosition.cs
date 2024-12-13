
using UnityEngine;

public class ClampPosition : MonoBehaviour
{
    [SerializeField] private bool clampPositionX;
    [SerializeField] private bool clampPositionY;
    private Transform _transform;
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
        _transform = transform;
    }
    private void LateUpdate()
    {
        ClampLimitsPosition();
    }
    private void ClampLimitsPosition()
    {
        var viewportPoint = _camera.WorldToViewportPoint(_transform.position);
        if (clampPositionX)
        {
            viewportPoint.x = Mathf.Clamp(viewportPoint.x, 0.07f, 0.93f); 
            
        }
        if (clampPositionY)
        {
            viewportPoint.y = Mathf.Clamp(viewportPoint.y, 0.07f, 0.93f); 
            
        }
        _transform.position =_camera.ViewportToWorldPoint(viewportPoint);
    }
}