using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Transform mainCameraTransform;

    void OnEnable()
    {
        FindMainCamera();
    }

    void Start()
    {
        FindMainCamera();
    }

    void Update()
    {
        if (mainCameraTransform != null)
        {
            transform.rotation = Quaternion.LookRotation(transform.position - mainCameraTransform.position);
        }
        else
        {
            FindMainCamera();
        }
    }

    private void FindMainCamera()
    {
        if (Camera.main != null)
        {
            mainCameraTransform = Camera.main.transform;
        }        
    }
}
