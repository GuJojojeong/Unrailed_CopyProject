using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RailTurn : MonoBehaviour
{
    [Header("레일 오브젝트")]
    [SerializeField] GameObject railObjectStraight;
    [SerializeField] GameObject railObjectRight;

    RaycastHit hit;
    Ray rayFront;
    Ray rayRight;
    Ray rayLeft;

    private float distance = 0.5f;
    private int layerMask;

    public LayerMask layerMask2;

    private void Awake()
    {
        railObjectStraight = transform.GetChild(0).gameObject;
        railObjectRight = transform.GetChild(1).gameObject;
        layerMask = 1 << LayerMask.NameToLayer("Rail");
    }

    void Start()
    {
        if (LastRailSelect.Instance != null && LastRailSelect.Instance.rail != this.gameObject) //가장 마지막에 설치한 레일이 아니라면 실행
        {
            IfMakeNewRail();
        }
    }

    Ray MakeFrontRay()
    {
        return new Ray(transform.position, transform.forward);
    }

    Ray MakeLeftRay()
    {
        return new Ray(transform.position, -transform.right);
    }

    Ray MakeRightRay()
    {
        return new Ray(transform.position, transform.right);
    }

    public void IfMakeNewRail()
    {
        if (LastRailSelect.Instance != null && LastRailSelect.Instance.rail != null)
        {
            LastRailSelect.Instance.rail.GetComponent<RailTurn>().CheckCloseRail();
            CheckRotate();
            LastRailSelect.Instance.rail = this.gameObject;
        }
    }

    public void CheckRotate() //가장 마지막에 설치한 레일만 실행
    {
        rayFront = MakeFrontRay();
        rayLeft = MakeLeftRay();
        rayRight = MakeRightRay();

        if (Physics.Raycast(rayFront.origin, rayFront.direction, out hit, distance, layerMask))
        {
            if (hit.collider.gameObject == LastRailSelect.Instance.ReturnToLastRailChildObject(0)
                || hit.collider.gameObject == LastRailSelect.Instance.ReturnToLastRailChildObject(1))
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            }
        }

        if (Physics.Raycast(rayRight.origin, rayRight.direction, out hit, distance, layerMask))
        {
            if (hit.collider.gameObject == LastRailSelect.Instance.ReturnToLastRailChildObject(0)
                || hit.collider.gameObject == LastRailSelect.Instance.ReturnToLastRailChildObject(1))
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, -90, 0));
            }
        }

        if (Physics.Raycast(rayLeft.origin, rayLeft.direction, out hit, distance, layerMask))
        {
            if (hit.collider.gameObject == LastRailSelect.Instance.ReturnToLastRailChildObject(0)
                || hit.collider.gameObject == LastRailSelect.Instance.ReturnToLastRailChildObject(1))
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
            }
        }
    }

    public void CheckCloseRail()
    {
        rayFront = MakeFrontRay();
        rayLeft = MakeLeftRay();
        rayRight = MakeRightRay();

        if (Physics.Raycast(rayFront.origin, rayFront.direction, out hit, distance, layerMask))
        {
            //정면에 있다면 : 변경 X
        }
        else if (Physics.Raycast(rayRight.origin, rayRight.direction, out hit, distance, layerMask))
        {
            //오른쪽에 있다면 : 오른쪽으로 Turn 리소스 변경
            railObjectStraight.SetActive(false);
            railObjectRight.SetActive(true);
        }
        else if (Physics.Raycast(rayLeft.origin, rayLeft.direction, out hit, distance, layerMask))
        {
            //왼쪽에 있다면 : 왼쪽으로 Turn 리소스 변경
            railObjectStraight.SetActive(false);
            railObjectRight.SetActive(true);
            railObjectRight.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
        }
    }

//    private void Update()
//    {
//        RaycastHit hit2;
        
//        Ray rayTop = new Ray(new Vector3(transform.position.x, (transform.position.y - 0.5f), transform.position.z), transform.up);
//        if(Physics.Raycast(rayTop.origin, rayTop.direction, out hit2, 0.25f, layerMask2))
//        {
//            Debug.Log(hit2.transform.gameObject);
//        }
//        Debug.DrawRay(rayTop.origin, rayTop.direction, Color.red);
//    }
}
