using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // 따라갈 대상 (캐릭터)
    public Vector3 offset; // 카메라와 대상 사이의 거리
    public float smoothSpeed = 0.125f; 

    private void FixedUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        transform.LookAt(target); // 대상 바라보기
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
