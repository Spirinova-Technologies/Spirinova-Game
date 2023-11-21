using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Assets.HeroEditor.Common.CharacterScripts;

public class Fulanito : MonoBehaviourPunCallbacks
{
    GameMaster gm;

    public Character character;
    Unit unit;

    void Start()
    {
        gm = FindObjectOfType<GameMaster>();
        character = GetComponent<Character>();
        unit = GetComponent<Unit>();
        unit.gloriaEterna = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && gm.selectedUnit != null && gm.selectedUnit.name == "Fulanito(Clone)" && gm.selectedUnit.actionPoints >= 1 && gm.selectedUnit.hachaDelDemonioCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            HachaDelDemonio(gm.selectedUnit);   
        }
    }

    public void HachaDelDemonio(Unit unit)
    {
        if (unit.hachaDelDemonioCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.hachaDelDemonioCast = true;
            unit.actionPoints -= 1;
            unit.UpdateActionPointsText();
        }
    }

    [PunRPC]
    public void VictoryAnim()
    {
        character.Animator.SetTrigger("Victory Trigger");
    }
}