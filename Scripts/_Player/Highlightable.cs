using UnityEngine;

public class Highlightable : MonoBehaviour, IHighlightable
{
    private Renderer RenDerer;
    private Material originalMaterial;
    public Material highlightMaterial;

    void Start()
    {
        RenDerer = GetComponent<Renderer>();
        originalMaterial = RenDerer.material;
    }

    public void Highlight()
    {
        RenDerer.material = highlightMaterial;
    }

    public void ResetHighlight()
    {
        RenDerer.material = originalMaterial;
    }
}
