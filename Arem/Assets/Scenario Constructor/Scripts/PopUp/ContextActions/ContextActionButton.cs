using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ContextActionButton : MonoBehaviour
{
    [SerializeField] private Text _textComponent;

    private Button _button;


    private void Awake()
    {
        _button = GetComponent<Button>();
    }


    public void Init(string label, Action callback)
    {
        _textComponent.text = label;
        _button.onClick.AddListener(callback.Invoke);

        gameObject.SetActive(true);
    }

    public void Deinit()
    {
        _button.onClick.RemoveAllListeners();

        gameObject.SetActive(false);
    }
}