using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailPlacer : MonoBehaviour
{
    public Transform railPrefab;  // 레일 프리팹
    public Transform railPrefab2;
    private Transform currentRail; // 현재 배치 중인 레일

 
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼 클릭
        {
            PlaceRail();
        }
        if (Input.GetMouseButtonDown(1)) 
        {
            PlaceRail2();
        }
        if (Input.GetMouseButtonDown(2))
        {
            PlaceRail3();
        }
    }

    private void PlaceRail3()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 position = hit.point;

            // 첫 레일 배치
            if (currentRail == null)
            {
                currentRail = Instantiate(railPrefab2, position, Quaternion.identity);
                RailManager.Instance.AddRail(currentRail);
            }
            else
            {
                // 현재 레일의 끝점에 새 레일을 배치
                Vector3 endPoint = currentRail.transform.Find("EndPoint").position;
              

                currentRail = Instantiate(railPrefab2, endPoint, Quaternion.Euler(0, -90f, 0));

                RailManager.Instance.AddRail(currentRail);
            }
        }
    }

    private void PlaceRail2()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 position = hit.point;

            // 첫 레일 배치
            if (currentRail == null)
            {
                currentRail = Instantiate(railPrefab2, position, Quaternion.identity);
                RailManager.Instance.AddRail(currentRail);
            }
            else
            {
                // 현재 레일의 끝점에 새 레일을 배치
                Vector3 endPoint = currentRail.transform.Find("EndPoint").position;
                
                currentRail = Instantiate(railPrefab2, endPoint, Quaternion.Euler(0,90f,0));

                RailManager.Instance.AddRail(currentRail);
            }
        }
    }

    void PlaceRail()
    {
        // 마우스 위치 가져오기
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 position = hit.point;

            // 첫 레일 배치
            if (currentRail == null)
            {
                currentRail = Instantiate(railPrefab, position, Quaternion.identity);
                RailManager.Instance.AddRail(currentRail);
            }
            else
            {
                // 현재 레일의 끝점에 새 레일을 배치
                Vector3 endPoint = currentRail.transform.Find("EndPoint").position;
                currentRail = Instantiate(railPrefab, endPoint, Quaternion.identity);
                RailManager.Instance.AddRail(currentRail);
            }
        }
    }
}
