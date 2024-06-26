using UnityEngine;

public class Pickupable : MonoBehaviour, IInteractable
{
    private Transform originalParent;

    public void Pickup(Transform holdPoint)
    {
        originalParent = transform.parent;
        transform.parent = holdPoint;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    public void Drop()
    {
        transform.parent = originalParent;
    }

    public bool TryPlace()
    {
        // Implement track placing logic here
        return false;
    }
}
