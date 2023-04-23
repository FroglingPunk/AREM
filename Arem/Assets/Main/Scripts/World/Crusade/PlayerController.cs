using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Vector2 _limitPositionX;
    [SerializeField] private float _movementSpeed = 3f;


    private void Update()
    {
        var axis = Input.GetAxis("Horizontal");

        var position = transform.position;
        position.x += axis * _movementSpeed * Time.deltaTime;
        position.x = Mathf.Clamp(position.x, _limitPositionX.x, _limitPositionX.y);
        transform.position = position;
    }
}