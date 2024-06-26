using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainResourceContainer : MonoBehaviour
{
    [SerializeField] protected GameObject[] woodcontainer;
    [SerializeField] protected GameObject[] stonecontainer;

    int woodCount;
    int stoneCount;

    private void Awake()
    {
        
    }
}

interface IResourceManagement
{
    void GetResource();
    void UseResource();
}