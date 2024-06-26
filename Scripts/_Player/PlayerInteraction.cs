using UnityEngine;
using Photon.Pun;

public class PlayerInteraction : MonoBehaviourPun
{
    public float interactionRange = 1.5f;
    public float closeRange = 0.75f;
    public Transform holdPoint;
    public LayerMask interactableLayer;
    public Material highlightMaterial;

    private IInteractable heldObject = null;
    private IHighlightable highlightedObject = null;
    private Rigidbody heldObjectRigidbody = null;
    private PhotonView photonView;

    void Start()
    {
        photonView = GetComponent<PhotonView>();

        // "Rail" ���̾ interactableLayer�� �߰��մϴ�.
        int railLayer = LayerMask.NameToLayer("Resource");
        interactableLayer |= (1 << railLayer);
    }

    void Update()
    {
        if (!photonView.IsMine) return;

        HighlightInteractable();
        HandleInput();
    }

    void FixedUpdate()
    {
        UpdateHeldObjectPosition();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (heldObject == null)
            {
                TryPickup();
            }
            else
            {
                TryPlaceOrDropOrStack();
            }
        }
    }

    private void TryPickup()
    {
        if (highlightedObject != null)
        {
            IInteractable interactable = (highlightedObject as Component).GetComponent<IInteractable>();
            if (interactable != null)
            {
                heldObject = interactable;
                heldObject.Pickup(holdPoint);

                PhotonView heldObjectPhotonView = (heldObject as Component).GetComponent<PhotonView>();
                if (heldObjectPhotonView != null)
                {
                    photonView.RPC("RPC_PickupObject", RpcTarget.AllBuffered, heldObjectPhotonView.ViewID);
                }

                highlightedObject.ResetHighlight();
                highlightedObject = null;

                // ���� ��ȣ�ۿ��� �����ϱ� ���� ������ٵ� ���� ����
                heldObjectRigidbody = (heldObject as Component).GetComponent<Rigidbody>();
                if (heldObjectRigidbody != null)
                {
                    heldObjectRigidbody.isKinematic = true;
                    heldObjectRigidbody.useGravity = false;
                    heldObjectRigidbody.detectCollisions = false;
                }
                return;
            }
        }
        Debug.Log("No interactable object in range.");
    }

    [PunRPC]
    private void RPC_PickupObject(int viewID)
    {
        PhotonView view = PhotonView.Find(viewID);
        if (view != null)
        {
            IInteractable interactable = view.GetComponent<IInteractable>();
            if (interactable != null)
            {
                heldObject = interactable;
                heldObject.Pickup(holdPoint);
            }
        }
    }

    private bool IsWithinView(GameObject target)
    {
        Vector3 directionToTarget = (target.transform.position - transform.position).normalized;
        Vector3 forward = transform.forward;

        forward.y = 0;
        directionToTarget.y = 0;

        forward.Normalize();
        directionToTarget.Normalize();

        float angle = Vector3.Angle(forward, directionToTarget);
        return angle <= 30f;
    }

    private void HighlightInteractable()
    {
        if (heldObject != null) return;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, interactionRange, interactableLayer);
        Collider closestCollider = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider hitCollider in hitColliders)
        {
            float distance = Vector3.Distance(transform.position, hitCollider.transform.position);
            if (distance < closestDistance && IsWithinView(hitCollider.gameObject))
            {
                closestDistance = distance;
                closestCollider = hitCollider;
            }
        }

        if (closestCollider != null)
        {
            IHighlightable highlightable = closestCollider.GetComponent<IHighlightable>();
            if (highlightable != null)
            {
                if (highlightedObject != null && highlightedObject != highlightable)
                {
                    highlightedObject.ResetHighlight();
                }
                highlightedObject = highlightable;
                highlightedObject.Highlight();
                return;
            }
        }

        if (highlightedObject != null)
        {
            highlightedObject.ResetHighlight();
            highlightedObject = null;
        }
    }

    private void UpdateHeldObjectPosition()
    {
        if (heldObject != null)
        {
            // ��� �ִ� ��ü�� ��ġ�� ȸ���� holdPoint�� ���� ������Ʈ
            Transform heldObjectTransform = (heldObject as Component).transform;
            (heldObject as Component).transform.position = holdPoint.position;
            (heldObject as Component).transform.rotation = holdPoint.rotation;

            PhotonView heldObjectPhotonView = (heldObject as Component).GetComponent<PhotonView>();
            if (heldObjectPhotonView != null && heldObjectPhotonView.IsMine)
            {
                heldObjectPhotonView.transform.position = holdPoint.position;
                heldObjectPhotonView.transform.rotation = holdPoint.rotation;
            }
        }
    }

    private bool IsFacing(GameObject target)
    {
        Vector3 directionToTarget = (target.transform.position - transform.position).normalized;
        return Vector3.Dot(transform.forward, directionToTarget) > 0.5f;
    }

    private void TryPlaceOrDropOrStack()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, closeRange, interactableLayer);

        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.GetComponent<IInteractable>() == heldObject) continue;

            // �߰��� ���� ����
        }

        if (heldObject.TryPlace())
        {
            // ������ٵ� ������ ������� �ǵ���
            if (heldObjectRigidbody != null)
            {
                heldObjectRigidbody.isKinematic = false;
                heldObjectRigidbody.useGravity = true;
                heldObjectRigidbody.detectCollisions = true;
                heldObjectRigidbody = null;
            }

            PhotonView heldObjectPhotonView = (heldObject as Component).GetComponent<PhotonView>();
            if (heldObjectPhotonView != null)
            {
                Vector3 position = (heldObject as Component).transform.position;
                Quaternion rotation = (heldObject as Component).transform.rotation;
                photonView.RPC("RPC_DropObject", RpcTarget.AllBuffered, heldObjectPhotonView.ViewID, position, rotation);
            }

            heldObject = null;
        }
        else
        {
            heldObject.Drop();
            // ������ٵ� ������ ������� �ǵ���
            if (heldObjectRigidbody != null)
            {
                heldObjectRigidbody.isKinematic = false;
                heldObjectRigidbody.useGravity = true;
                heldObjectRigidbody.detectCollisions = true;
                heldObjectRigidbody = null;
            }
            heldObject = null;
        }
    }

    [PunRPC]
    private void RPC_DropObject(int viewID, Vector3 position, Quaternion rotation)
    {
        PhotonView view = PhotonView.Find(viewID);
        if (view != null)
        {
            IInteractable interactable = view.GetComponent<IInteractable>();
            if (interactable != null)
            {
                (interactable as Component).transform.position = position;
                (interactable as Component).transform.rotation = rotation;
                interactable.Drop();
            }
        }
    }
}
