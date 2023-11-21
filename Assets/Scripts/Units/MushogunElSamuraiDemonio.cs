using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Assets.HeroEditor.Common.CharacterScripts;

public class MushogunElSamuraiDemonio : MonoBehaviourPunCallbacks
{
    GameMaster gm;

    public Character character;

    void Start()
    {
        gm = FindObjectOfType<GameMaster>();
        character = GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && gm.selectedUnit != null && gm.selectedUnit.name == "Mushogun, el Samurai Demonio(Clone)" && gm.selectedUnit.actionPoints >= 2 && gm.selectedUnit.espadaDelDemonioCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            EspadaDelDemonio(gm.selectedUnit);
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2) && gm.selectedUnit != null && gm.selectedUnit.name == "Mushogun, el Samurai Demonio(Clone)" && gm.selectedUnit.actionPoints >= 4 && gm.selectedUnit.espadaDeLaMuerteCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            EspadaDeLaMuerte(gm.selectedUnit);
        }
    }

    public void EspadaDelDemonio(Unit unit)
    {
        if (unit.espadaDelDemonioCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.espadaDelDemonioCast = true;
            unit.actionPoints -= 2;
        }
    }

    public void EspadaDeLaMuerte(Unit unit)
    {
        if (unit.espadaDeLaMuerteCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.espadaDeLaMuerteCast = true;
            unit.actionPoints -= 4;
        }
    }

    [PunRPC]
    public void VictoryAnim()
    {
        character.Animator.SetTrigger("Victory Trigger");
    }
}