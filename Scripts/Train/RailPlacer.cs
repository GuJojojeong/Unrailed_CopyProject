using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailPlacer : MonoBehaviour
{
    public Transform railPrefab;  // ���� ������
    public Transform railPrefab2;
    private Transform currentRail; // ���� ��ġ ���� ����

 
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ���콺 ���� ��ư Ŭ��
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

            // ù ���� ��ġ
            if (currentRail == null)
            {
                currentRail = Instantiate(railPrefab2, position, Quaternion.identity);
                RailManager.Instance.AddRail(currentRail);
            }
            else
            {
                // ���� ������ ������ �� ������ ��ġ
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

            // ù ���� ��ġ
            if (currentRail == null)
            {
                currentRail = Instantiate(railPrefab2, position, Quaternion.identity);
                RailManager.Instance.AddRail(currentRail);
            }
            else
            {
                // ���� ������ ������ �� ������ ��ġ
                Vector3 endPoint = currentRail.transform.Find("EndPoint").position;
                
                currentRail = Instantiate(railPrefab2, endPoint, Quaternion.Euler(0,90f,0));

                RailManager.Instance.AddRail(currentRail);
            }
        }
    }

    void PlaceRail()
    {
        // ���콺 ��ġ ��������
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 position = hit.point;

            // ù ���� ��ġ
            if (currentRail == null)
            {
                currentRail = Instantiate(railPrefab, position, Quaternion.identity);
                RailManager.Instance.AddRail(currentRail);
            }
            else
            {
                // ���� ������ ������ �� ������ ��ġ
                Vector3 endPoint = currentRail.transform.Find("EndPoint").position;
                currentRail = Instantiate(railPrefab, endPoint, Quaternion.identity);
                RailManager.Instance.AddRail(currentRail);
            }
        }
    }
}
