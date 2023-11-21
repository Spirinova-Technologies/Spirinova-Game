using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Assets.HeroEditor.Common.CharacterScripts;

public class ElGuardiaDeAcero : MonoBehaviourPunCallbacks
{
    GameMaster gm;

    public Character character;

    public GameObject pickupHeart;
    public GameObject fireShield; // Attack Buff Animation

    void Start()
    {
        gm = FindObjectOfType<GameMaster>();
        character = GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && gm.selectedUnit != null && gm.selectedUnit.name == "El Guardia de Acero(Clone)" && gm.selectedUnit.actionPoints >= 4 && gm.selectedUnit.resistenciaCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            Resistencia(gm.selectedUnit);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && gm.selectedUnit != null && gm.selectedUnit.name == "El Guardia de Acero(Clone)" && gm.selectedUnit.actionPoints >= 2 && gm.selectedUnit.motivacionCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            Motivacion(gm.selectedUnit);
            gm.UpdateStatsPanel(); 
        }
    }

    public void Resistencia(Unit unit)
    {
        if (unit.resistenciaCast == false)
        {
            photonView.RPC("ResistenciaAnimation", RpcTarget.All);
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.resistenciaCast = true;
            unit.actionPoints -= 4;
            unit.physicalArmor += 1;
            unit.resistenciaCast = false;
        }
    }

    [PunRPC]
    public void ResistenciaAnimation()
    {
        Instantiate(pickupHeart, this.transform.position, Quaternion.identity);
    }

    public void Motivacion(Unit unit)
    {
        if (unit.motivacionCast == false)
        {
            photonView.RPC("MotivacionAnimation", RpcTarget.All);
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.motivacionCast = true;
            unit.actionPoints -= 2;
            unit.UpdateActionPointsText();
            unit.attackDamage += 1;
            unit.motivacionCast = false;
        }
    }

    [PunRPC]
    public void MotivacionAnimation()
    {
        Instantiate(fireShield, this.transform.position, Quaternion.identity);
    }

    [PunRPC]
    public void VictoryAnim()
    {
        character.Animator.SetTrigger("Victory Trigger");
    }
}