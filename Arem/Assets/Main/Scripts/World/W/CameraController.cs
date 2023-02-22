using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    [SerializeField] private float _maxOrtographicSize;
    [SerializeField] private float _minOrtographicSize;

    [SerializeField, Range(1f, 5f)] private float _zoomChangingSpeed = 1f;
    [SerializeField, Range(1f, 100f)] private float _posChangingSpeed = 1f;

    private Camera _camera;


    private void Start()
    {
        _camera = GetComponent<Camera>();
    }

    private void Update()
    {
        Zoom();
        Move();
        ClampPosition();
    }


    private void Zoom()
    {
        var value = Input.mouseScrollDelta.y;
        value *= _zoomChangingSpeed;

        _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize - value, _minOrtographicSize, _maxOrtographicSize);
    }

    private void Move()
    {
        var input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (input.x == 0 && input.y == 0)
            return;

        var position = transform.position;
        position.x += input.x * _posChangingSpeed * Time.deltaTime;
        position.z += input.y * _posChangingSpeed * Time.deltaTime;
        transform.position = position;
    }

    private void ClampPosition()
    {
        var delta = _maxOrtographicSize - _camera.orthographicSize;

        var maxPositionZ = delta;
        var minPositionZ = -delta;

        var maxPositionX = delta * 16f / 9f;
        var minPositionX = -maxPositionX;

        var position = transform.position;
        position.x = Mathf.Clamp(position.x, minPositionX, maxPositionX);
        position.z = Mathf.Clamp(position.z, minPositionZ, maxPositionZ);
        transform.position = position;
    }
}