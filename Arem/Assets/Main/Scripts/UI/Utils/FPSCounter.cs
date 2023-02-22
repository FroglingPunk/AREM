using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    [SerializeField] private Text _textFpsCount;


    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
            _textFpsCount.enabled = !_textFpsCount.enabled;

        if (!_textFpsCount.enabled)
            return;

        var fps = (int)(1f / Time.unscaledDeltaTime);
        _textFpsCount.text = fps.ToString();
    }
}