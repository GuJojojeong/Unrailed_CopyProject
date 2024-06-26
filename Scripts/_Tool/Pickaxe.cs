using UnityEngine;

public class Pickaxe : Tool
{
    protected override bool IsValidTarget(InteractableObject interactableObject)
    {
        return interactableObject is Stone;
    }
}
