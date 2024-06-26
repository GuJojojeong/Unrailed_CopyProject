using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastRailSelect : MonoBehaviour
{
    public static LastRailSelect Instance;
    public GameObject rail;
    public GameObject EndRail;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this);
        }
        Instance = this;

    }
  
    public GameObject ReturnToLastRailChildObject(int Num)
    {
        return rail.transform.GetChild(Num).gameObject;
    }
}
