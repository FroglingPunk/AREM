using UnityEngine;

namespace Utils
{
    [RequireComponent(typeof(Camera))]
    public class DynamicOrtographicCamera : MonoBehaviour
    {
        private const float DEFAULT_ASPECT = 16f / 9f;

        private Camera cam;
        private float defaultOrtographicSize;


        private void Start()
        {
            cam = GetComponent<Camera>();

            defaultOrtographicSize = cam.orthographicSize;
        }

        private void Update()
        {
            cam.orthographicSize = DEFAULT_ASPECT / cam.aspect * defaultOrtographicSize;
        }
    }
}