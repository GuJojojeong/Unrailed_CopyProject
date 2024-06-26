using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstallRail : MonoBehaviour, IItemSetposition, IItemCanStack
{
    int count = 0;
    GameObject topObject; //가장 맨 위에 있는 오브젝트
    public void CanStack()
    {
        if(this == topObject)
        {
            Debug.Log("실행");
        }
    }

    public void CheckTopObject()
    {
        Ray rayTop = new Ray(new Vector3(transform.position.x, (transform.position.y - 0.5f), transform.position.z), transform.up);
        Debug.Log(Physics.Raycast(rayTop.origin, rayTop.direction, 10));
    }

    public void SetPosition()
    {
        transform.position = new Vector3((int)transform.position.x, transform.position.y, (int)transform.position.z);
        Debug.Log("실행");
        Debug.Log(transform.position.x + "/" + transform.position.y);
    }

    private void Awake()
    {

    }
    void Start()
    {
        SetPosition();
    }

}
