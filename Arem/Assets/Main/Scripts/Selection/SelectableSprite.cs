using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public abstract class SelectableSprite<T> : SelectableBase<T> where T : MonoBehaviour
{
    private void OnMouseUpAsButton()
    {
        Select();
    }
}