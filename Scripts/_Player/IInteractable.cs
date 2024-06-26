using UnityEngine;

public interface IInteractable
{
    void Pickup(Transform holdPoint);
    void Drop();
    bool TryPlace();
}
