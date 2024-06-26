using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneScale : MonoBehaviour
{
    void Start()
    {
        float randomScale = Random.Range(1f, 1.5f);
        transform.localScale = new Vector3(randomScale, Random.Range(1f, 2f), randomScale);
    }

}
