using UnityEngine;

public abstract class PlayerTriggerBase : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<TestPlayer>(out var player))
            OnPlayerEnter(player);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<TestPlayer>(out var player))
            OnPlayerExit(player);
    }


    protected abstract void OnPlayerEnter(TestPlayer player);
    protected abstract void OnPlayerExit(TestPlayer player);
}