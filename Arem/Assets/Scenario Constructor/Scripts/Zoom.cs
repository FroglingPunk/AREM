using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour
{
    [SerializeField] private float _zoomChangingSpeed = 1f;

    private RectTransform _rectTransform;


    private void Awake()
    {
        _rectTransform = transform as RectTransform;
    }


    void Update()
    {
        var value = Input.mouseScrollDelta.y;
        value *= _zoomChangingSpeed;

        var scale = _rectTransform.localScale + Vector3.one * value;

        scale.x = Mathf.Clamp(scale.x, 0.3f, 2f);
        scale.y = Mathf.Clamp(scale.y, 0.3f, 2f);
        scale.z = Mathf.Clamp(scale.z, 0.3f, 2f);

        _rectTransform.localScale = scale;
    }
}