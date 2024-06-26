using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    public int hitPoints;
    private int currentHitPoints;
    public Material hitMaterial;
    private Renderer renderer;
    private Material originalMaterial;
    public GameObject dropItemPrefab; // 파괴될 때 생성할 아이템의 프리팹

    protected virtual void Start()
    {
        currentHitPoints = hitPoints;
        renderer = GetComponent<Renderer>();
        originalMaterial = renderer.material;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
            rb.isKinematic = true;
        }
    }

    public void Hit()
    {
        currentHitPoints--;
        renderer.material = hitMaterial;
        if (currentHitPoints <= 0)
        {
            DropItem();
            Destroy(gameObject);
        }
    }

    private void DropItem()
    {
        if(dropItemPrefab == null)
        {
            Debug.Log("널값");
        }
        if (dropItemPrefab != null)
        {
            Debug.Log("들어옴");
            Instantiate(dropItemPrefab, transform.position, Quaternion.identity);
        }
    }
}
