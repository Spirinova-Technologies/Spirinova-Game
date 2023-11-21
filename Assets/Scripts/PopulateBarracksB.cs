using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulateBarracksB : MonoBehaviour
{
    private BarracksA barracksa;
    private BarracksB barracksb;
    public GameObject unitButton;

    private void Start()
    {
        barracksb = GameObject.FindGameObjectWithTag("BarracksB").GetComponent<BarracksB>();
    }

    public void OnPopulate()
    {
        for (int i = 0; i < barracksb.slots.Length; i++)
        {
            if (barracksb.isFull[i] == false)
            {
                // Unit can be added to barracks.
                barracksb.isFull[i] = true;
                Instantiate(unitButton, barracksb.slots[i].transform, false);
                gameObject.GetComponent<Button>().interactable = false;
                //Destroy(gameObject); PROBABLY NOT NEEDED.
                break;
            }
        }
    }
}
