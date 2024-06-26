using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rail : MonoBehaviour
{
   void Start()
   {
        
        if (RailManager.Instance != null)
        {
            RailManager.Instance.AddRail(this.transform);
        }
    }

}
