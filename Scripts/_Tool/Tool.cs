using UnityEngine;

public abstract class Tool : MonoBehaviour, IInteractable
{
    private Transform originalParent;
    public float detectionRange = 2.0f;
    public LayerMask interactableLayer;
    private Rigidbody rb;
    public float hitCooldown = 0.5f; // Ÿ�� ������ ���� �ð�
    private float lastHitTime;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        rb.isKinematic = false;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        lastHitTime = -hitCooldown; // ���� ���� �� �ٷ� Ÿ���� �� �ֵ��� ����
    }

    protected virtual void Update()
    {
        CheckForCollision();
    }

    private void CheckForCollision()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRange, interactableLayer);
        foreach (Collider hitCollider in hitColliders)
        {
            if (Time.time >= lastHitTime + hitCooldown) // Ÿ�� ���� Ȯ��
            {
                InteractableObject interactableObject = hitCollider.GetComponent<InteractableObject>();
                if (interactableObject != null && IsValidTarget(interactableObject))
                {
                    interactableObject.Hit();
                    lastHitTime = Time.time; // ������ Ÿ�� �ð� ������Ʈ
                    Debug.Log($"Hit the {hitCollider.tag} with a {GetType().Name.ToLower()}.");
                }
            }
        }
    }

    protected abstract bool IsValidTarget(InteractableObject interactableObject);

    public void Pickup(Transform holdPoint)
    {
        originalParent = transform.parent;
        transform.parent = holdPoint;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        rb.isKinematic = true;
    }

    public void Drop()
    {
        transform.parent = originalParent;
        rb.isKinematic = false;
    }

    public virtual bool TryPlace()
    {
        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Time.time >= lastHitTime + hitCooldown) // Ÿ�� ���� Ȯ��
        {
            InteractableObject interactableObject = other.GetComponent<InteractableObject>();
            if (interactableObject != null && IsValidTarget(interactableObject))
            {
                interactableObject.Hit();
                lastHitTime = Time.time; // ������ Ÿ�� �ð� ������Ʈ
                Debug.Log($"Hit the {other.tag} with a {GetType().Name.ToLower()}.");
            }
        }
    }
}
