using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Assets.HeroEditor.Common.CharacterScripts;

public class NekrilElAmigoDelConocido : MonoBehaviourPunCallbacks
{
    GameMaster gm;

    public Character character;

    public GameObject magicAuraBRunic;
    public GameObject magicCircleN; // Range Buff Animation

    void Start()
    {
        gm = FindObjectOfType<GameMaster>();
        character = GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && gm.selectedUnit != null && gm.selectedUnit.name == "Nekril, el Amigo del Conocido(Clone)" && gm.selectedUnit.actionPoints >= 4 && gm.selectedUnit.creacionDeBrujulaCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            CreacionDeBrujula(gm.selectedUnit);   
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2) && gm.selectedUnit != null && gm.selectedUnit.name == "Nekril, el Amigo del Conocido(Clone)" && gm.selectedUnit.actionPoints >= 2 && gm.selectedUnit.creacionDeBalasLaserCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            CreacionDeBalasLaser(gm.selectedUnit);   
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && gm.selectedUnit != null && gm.selectedUnit.name == "Nekril, el Amigo del Conocido(Clone)" && gm.selectedUnit.actionPoints >= 1 && gm.selectedUnit.disparoRapidoCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            DisparoRapido(gm.selectedUnit);   
        }
    }

    public void CreacionDeBrujula(Unit unit)
    {
        if (unit.creacionDeBrujulaCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.creacionDeBrujulaCast = true;
            unit.actionPoints -= 4;
            foreach (Unit units in FindObjectsOfType<Unit>())
            {
                units.photonView.RPC("CreacionDeBrujulaAnimation", RpcTarget.All);
                units.actionPoints += 1;
            }
            unit.creacionDeBrujulaCast = false;
        }
    }

    [PunRPC]
    public void CreacionDeBrujulaAnimation()
    {
        Instantiate(magicAuraBRunic, this.transform.position, Quaternion.identity);
    }

    public void CreacionDeBalasLaser(Unit unit)
    {
        if (unit.creacionDeBalasLaserCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.creacionDeBalasLaserCast = true;
            unit.actionPoints -= 2;
            foreach (Unit units in FindObjectsOfType<Unit>())
            {
                units.photonView.RPC("CreacionDeBalasLaserAnimation", RpcTarget.All);
                units.maxAttackRange += 1;
            }
            unit.creacionDeBalasLaserCast = false;
        }
    }

    [PunRPC]
    public void CreacionDeBalasLaserAnimation()
    {
        Instantiate(magicCircleN, this.transform.position, Quaternion.identity);
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