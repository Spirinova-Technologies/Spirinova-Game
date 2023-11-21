using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    private BarracksB barracksb;
    public int i;
    //private UnselectFromBarracks UnselectFromBarracks;

    private void Start()
    {
        barracksb = GameObject.FindGameObjectWithTag("BarracksB").GetComponent<BarracksB>();
    }

    private void Update()
    {
        if (transform.childCount <= 0)
        {
            barracksb.isFull[i] = false;
        }
    }
}
