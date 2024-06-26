using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeMap : MonoBehaviourPun
{
    GameObject firstRail;

    int[,] map = new int[40, 15]  // 5행 5열의 배열
    {
    {0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0},
    {0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0},
    {0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0},
    {0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0},
    {0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0},
    {0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 0, 0},
    {0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 0, 0},
    {0, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 0},
    {1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2},
    {1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2},

    {1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2},
    {3, 3, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2},
    {3, 3, 3, 3, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2},
    {3, 3, 3, 3, 3, 1, 1, 1, 1, 1, 2, 2, 2, 3, 3},
    {3, 3, 3, 3, 3, 4, 4, 4, 4, 1, 2, 2, 2, 3, 3},
    {3, 3, 3, 4, 4, 4, 4, 4, 4, 4, 4, 2, 2, 3, 3},
    {3, 3, 3, 4, 4, 4, 4, 4, 4, 4, 4, 2, 3, 3, 3},
    {3, 3, 4, 4, 4, 4, 4, 4, 4, 4, 4, 3, 3, 3, 3},
    {3, 3, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 3, 3, 3},
    {3, 3, 4, 4, 4, 4, 4, 4, 2, 4, 4, 4, 4, 3, 3},

    {2, 4, 4, 4, 4, 4, 4, 2, 2, 4, 4, 4, 4, 4, 3},
    {2, 4, 4, 4, 4, 4, 4, 2, 2, 4, 4, 4, 4, 4, 3},
    {2, 2, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 3, 3},
    {2, 2, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 3, 3},
    {2, 2, 2, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 3, 3},
    {2, 2, 2, 2, 4, 4, 4, 4, 4, 4, 4, 4, 2, 2, 2},
    {2, 2, 2, 2, 1, 1, 4, 4, 4, 4, 4, 1, 2, 2, 2},
    {2, 2, 2, 2, 1, 1, 3, 3, 3, 1, 1, 2, 2, 2, 2},
    {2, 2, 2, 2, 1, 3, 3, 3, 3, 3, 1, 2, 2, 2, 2},
    {2, 2, 2, 2, 1, 3, 3, 3, 3, 3, 1, 1, 2, 2, 2},

    {2, 2, 2, 1, 1, 3, 3, 3, 3, 3, 1, 1, 2, 2, 2},
    {2, 2, 2, 1, 1, 1, 3, 3, 3, 3, 1, 1, 2, 2, 2},
    {2, 2, 1, 1, 1, 1, 1, 3, 3, 1, 1, 1, 1, 2, 2},
    {1, 1, 4, 4, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    {1, 4, 4, 4, 4, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    {1, 4, 4, 4, 1, 1, 1, 1, 1, 1, 1, 1, 3, 3, 1},
    {2, 4, 4, 4, 2, 1, 1, 1, 1, 1, 1, 3, 3, 3, 3},
    {2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 3, 3, 3, 3},
    {2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 3, 3, 3, 3, 3},
    {2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 3, 3, 3, 3, 3}
    };

    [SerializeField] GameObject grassBlock; //아무것도 없음
    [SerializeField] GameObject groundBlock; //나무
    [SerializeField] GameObject stoneBlock; //돌
    [SerializeField] GameObject waterBlock; //물

    [SerializeField] GameObject tree;
    [SerializeField] GameObject stone;

    [SerializeField] GameObject rail;
    [SerializeField] GameObject straightRail;

    [SerializeField] GameObject trainHead;
    [SerializeField] GameObject woodContainer;
    [SerializeField] GameObject endStation;

    [SerializeField] Transform floor_1;
    [SerializeField] Transform floor_2;
    [SerializeField] Transform floor_3; //rail, train등


    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            MakeNormalMap();
            MakeDefaultRail();
            MakeDefaultTrain();
            MakeEndRail();
        }
    }

    void MakeNormalMap()
    {
        for (int i = 0; i < 40; i++)
        {
            for (int j = 0; j < 15; j++)
            {
                if (map[i, j] == 1)
                {
                    GameObject obj = PhotonNetwork.Instantiate("MapBlock/grass_block", new Vector3(i, 0, j), Quaternion.identity);
                    obj.transform.SetParent(floor_1);
                }
                else if (map[i, j] == 2)
                {
                    GameObject treeObj = PhotonNetwork.Instantiate("object/tree", new Vector3(i, 1, j), Quaternion.Euler(new Vector3(0, Random.Range(1, 180), 0)));
                    treeObj.transform.SetParent(floor_2);
                    GameObject groundObj = PhotonNetwork.Instantiate("MapBlock/ground_block", new Vector3(i, 0, j), Quaternion.identity);
                    groundObj.transform.SetParent(floor_1);
                }
                else if (map[i, j] == 3)
                {
                    GameObject stoneObj = PhotonNetwork.Instantiate("object/stone", new Vector3(i, 1, j), Quaternion.identity);
                    stoneObj.transform.SetParent(floor_2);
                    GameObject stoneBlockObj = PhotonNetwork.Instantiate("MapBlock/stone_block", new Vector3(i, 0, j), Quaternion.identity);
                    stoneBlockObj.transform.SetParent(floor_1);
                }
                else if (map[i, j] == 4)
                {
                    GameObject waterObj = PhotonNetwork.Instantiate("MapBlock/water_block", new Vector3(i, 0, j), Quaternion.identity);
                    waterObj.transform.SetParent(floor_1);
                }
            }
        }
    }

    void MakeDefaultRail()
    {
        for (int i = 3; i < 9; i++)
        {
            GameObject straightRailObj = PhotonNetwork.Instantiate("rail_gameobject/straight_rail", new Vector3(i, 1, 7), Quaternion.Euler(new Vector3(0, 0, 0)));
            straightRailObj.transform.SetParent(floor_3);
            RailManager.Instance.AddRail(straightRailObj.transform);
        }
        firstRail = PhotonNetwork.Instantiate("object/rail", new Vector3(9, 1, 7), Quaternion.Euler(new Vector3(0, 90, 0)));
        firstRail.transform.SetParent(floor_3);
        LastRailSelect.Instance.rail = firstRail;
        RailManager.Instance.AddRail(firstRail.transform);
        LastRailSelect.Instance.rail = firstRail; 
    }

    void MakeEndRail()
    {
        GameObject endRailObj = PhotonNetwork.Instantiate("object/rail", new Vector3(38, 1, 7), Quaternion.Euler(new Vector3(0, 90, 0))); 
        endRailObj.transform.SetParent(floor_3);         
        LastRailSelect.Instance.EndRail = endRailObj;
        GameObject endStationObj = PhotonNetwork.Instantiate("object/station", new Vector3(38, 1, 8), Quaternion.identity);
        endStationObj.transform.SetParent(floor_3);
    } 


        void MakeDefaultTrain()
    {
        GameObject trainHeadObj = PhotonNetwork.Instantiate("train/Tain", new Vector3(3, 1, 7), Quaternion.Euler(new Vector3(0, 90, 0)));
        trainHeadObj.transform.SetParent(floor_3);
        GameObject woodContainerObj = PhotonNetwork.Instantiate("train/Tain (1)", new Vector3(1, 1, 7), Quaternion.Euler(new Vector3(0, 90, 0)));
        woodContainerObj.transform.SetParent(floor_3);
        GameObject train1Obj = PhotonNetwork.Instantiate("train/Tain (2)", new Vector3(-1, 1, 7), Quaternion.Euler(new Vector3(0, 90, 0)));
        train1Obj.transform.SetParent(floor_3);
        GameObject railbuldObj = PhotonNetwork.Instantiate("train/RailBuildeCar", new Vector3(-3, 1, 7), Quaternion.Euler(new Vector3(270, 0, 0)));
        railbuldObj.transform.SetParent(floor_3);
    }
}
