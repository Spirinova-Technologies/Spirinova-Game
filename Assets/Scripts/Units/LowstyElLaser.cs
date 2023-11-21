using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Assets.HeroEditor.Common.CharacterScripts;

public class LowstyElLaser : MonoBehaviourPunCallbacks
{
    GameMaster gm;

    public Character character;

    public GameObject fireShield; // Attack Buff Animation
    public GameObject magicCircleN; // Range Buff Animation

    void Start()
    {
        gm = FindObjectOfType<GameMaster>();
        character = GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && gm.selectedUnit != null && gm.selectedUnit.name == "Lowsty, el Laser(Clone)" && gm.selectedUnit.actionPoints >= 1 && gm.selectedUnit.disparoRapidoCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            DisparoRapido(gm.selectedUnit);   
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2) && gm.selectedUnit != null && gm.selectedUnit.name == "Lowsty, el Laser(Clone)" && gm.selectedUnit.actionPoints >= 2 && gm.selectedUnit.crearMunicionesCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            CrearMuniciones(gm.selectedUnit);
            gm.UpdateStatsPanel();      
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && gm.selectedUnit != null && gm.selectedUnit.name == "Lowsty, el Laser(Clone)" && gm.selectedUnit.actionPoints >= 4 && gm.selectedUnit.disparoExplosivoCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            DisparoExplosivo(gm.selectedUnit);   
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

    public void CrearMuniciones(Unit unit)
    {
        if (unit.crearMunicionesCast == false)
        {
            photonView.RPC("CrearMunicionesAnimation", RpcTarget.All);
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.crearMunicionesCast = true;
            unit.actionPoints -= 2;
            unit.UpdateActionPointsText();
            unit.maxAttackRange += 1;
            unit.attackDamage += 1;
            unit.crearMunicionesCast = false;
            gm.UpdateStatsPanel();
            StartCoroutine(CrearMunicionesUncast(gm.selectedUnit, 10f));
            gm.UpdateStatsPanel();
        }
    }

    [PunRPC]
    public void CrearMunicionesAnimation()
    {
        Instantiate(fireShield, this.transform.position, Quaternion.identity);
        Instantiate(magicCircleN, this.transform.position, Quaternion.identity);
    }

    public void DisparoExplosivo(Unit unit)
    {
        if (unit.disparoExplosivoCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.disparoExplosivoCast = true;
            unit.actionPoints -= 4;
            unit.UpdateActionPointsText();
        }
    }

    [PunRPC]
    public void VictoryAnim()
    {
        character.Animator.SetTrigger("Victory Trigger");
    }

    IEnumerator CrearMunicionesUncast(Unit unit, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        unit.maxAttackRange -= 1;
        unit.attackDamage = Mathf.Max(0, unit.attackDamage - 1);
    }
}