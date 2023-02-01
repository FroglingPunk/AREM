using UnityEngine;

public class InputMouse : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            var messageBus = this.GetController<MessageBus>();
            messageBus.Callback(new SelectMessage<FieldCell>(null));
        }
    }
}