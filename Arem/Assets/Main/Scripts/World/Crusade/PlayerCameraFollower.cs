using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraFollower : MonoBehaviour
{
    [SerializeField] Vector2 _limitPositionX;

    private TestPlayer _player;
    private Vector3 _lastPlayerPosition;


    private void Start()
    {
        _player = FindObjectOfType<TestPlayer>();
        _lastPlayerPosition = _player.transform.position;
    }

    private void LateUpdate()
    {
        var position = transform.position + _player.transform.position - _lastPlayerPosition;
        position.x = Mathf.Clamp(position.x, _limitPositionX.x, _limitPositionX.y);
        position.y = transform.position.y;
        position.z = transform.position.z;
        transform.position = position;

        _lastPlayerPosition = _player.transform.position;
    }
}