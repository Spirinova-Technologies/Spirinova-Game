using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Assets.HeroEditor.Common.CharacterScripts;

public class JurgensitoElAmoDelDesvergue : MonoBehaviourPunCallbacks
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
        if (Input.GetKeyDown(KeyCode.Alpha1) && gm.selectedUnit != null && gm.selectedUnit.name == "Jurgensito, el Amo del Desvergue(Clone)" && gm.selectedUnit.actionPoints >= 6 && gm.selectedUnit.calzonChinoCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            CalzonChino(gm.selectedUnit);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && gm.selectedUnit != null && gm.selectedUnit.name == "Jurgensito, el Amo del Desvergue(Clone)" && gm.selectedUnit.actionPoints >= 6 && gm.selectedUnit.teVoyAMearWeyCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            TeVoyAMearWey(gm.selectedUnit);
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha3) && gm.selectedUnit != null && gm.selectedUnit.name == "Jurgensito, el Amo del Desvergue(Clone)" && gm.selectedUnit.actionPoints >= 6 && gm.selectedUnit.aventarHuevosCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            AventarHuevos(gm.selectedUnit);
        }
    }

    public void CalzonChino(Unit unit)
    {
        if (unit.calzonChinoCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.calzonChinoCast = true;
            unit.actionPoints -= 6;
        }
    }

    public void TeVoyAMearWey(Unit unit)
    {
        if (unit.teVoyAMearWeyCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.teVoyAMearWeyCast = true;
            unit.actionPoints -= 6;
        }
    }
    
    public void AventarHuevos(Unit unit)
    {
        if (unit.aventarHuevosCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.aventarHuevosCast = true;
            unit.actionPoints -= 6;
        }
    }

    [PunRPC]
    public void VictoryAnim()
    {
        character.Animator.SetTrigger("Victory Trigger");
    }
}