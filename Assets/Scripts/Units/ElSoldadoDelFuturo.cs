using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Assets.HeroEditor.Common.CharacterScripts;

public class ElSoldadoDelFuturo : MonoBehaviourPunCallbacks
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
        if (Input.GetKeyDown(KeyCode.Alpha1) && gm.selectedUnit != null && gm.selectedUnit.name == "El Soldado del Futuro(Clone)" && gm.selectedUnit.actionPoints >= 4 && gm.selectedUnit.lanzagranadasCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            Lanzagranadas(gm.selectedUnit);   
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2) && gm.selectedUnit != null && gm.selectedUnit.name == "El Soldado del Futuro(Clone)" && gm.selectedUnit.actionPoints >= 1 && gm.selectedUnit.disparoRapidoCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            DisparoRapido(gm.selectedUnit);   
        }
    }

    public void Lanzagranadas(Unit unit)
    {
        if (unit.lanzagranadasCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.lanzagranadasCast = true;
            unit.actionPoints -= 4;
            unit.UpdateActionPointsText();
        }
    }

    public void DisparoRapido(Unit unit)
    {
        if (unit.disparoRapidoCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.disparoRapidoCast = true;
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