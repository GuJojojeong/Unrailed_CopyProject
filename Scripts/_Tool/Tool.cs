using UnityEngine;

public abstract class Tool : MonoBehaviour, IInteractable
{
    private Transform originalParent;
    public float detectionRange = 2.0f;
    public LayerMask interactableLayer;
    private Rigidbody rb;
    public float hitCooldown = 0.5f; // 타격 간격을 위한 시간
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
        lastHitTime = -hitCooldown; // 게임 시작 시 바로 타격할 수 있도록 설정
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
            if (Time.time >= lastHitTime + hitCooldown) // 타격 간격 확인
            {
                InteractableObject interactableObject = hitCollider.GetComponent<InteractableObject>();
                if (interactableObject != null && IsValidTarget(interactableObject))
                {
                    interactableObject.Hit();
                    lastHitTime = Time.time; // 마지막 타격 시간 업데이트
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
        if (Time.time >= lastHitTime + hitCooldown) // 타격 간격 확인
        {
            InteractableObject interactableObject = other.GetComponent<InteractableObject>();
            if (interactableObject != null && IsValidTarget(interactableObject))
            {
                interactableObject.Hit();
                lastHitTime = Time.time; // 마지막 타격 시간 업데이트
                Debug.Log($"Hit the {other.tag} with a {GetType().Name.ToLower()}.");
            }
        }
    }
}
