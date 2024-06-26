using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // ���� ��� (ĳ����)
    public Vector3 offset; // ī�޶�� ��� ������ �Ÿ�
    public float smoothSpeed = 0.125f; 

    private void FixedUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        transform.LookAt(target); // ��� �ٶ󺸱�
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
