using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SelectableButton<T> : SelectableBase<T> where T : MonoBehaviour
{
    protected override void Awake()
    {
        base.Awake();

        var button = GetComponent<Button>();
        button.onClick.AddListener(Select);
    }
}