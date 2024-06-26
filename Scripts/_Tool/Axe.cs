using UnityEngine;

public class Axe : Tool
{
    protected override bool IsValidTarget(InteractableObject interactableObject)
    {
        return interactableObject is Tree;
    }
}
