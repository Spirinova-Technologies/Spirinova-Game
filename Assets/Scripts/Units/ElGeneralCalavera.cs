using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Assets.HeroEditor.Common.CharacterScripts;

public class ElGeneralCalavera : MonoBehaviourPunCallbacks
{
    GameMaster gm;

    public Character character;

    public GameObject magicAuraBRunic; // AP Gain Animation
    public GameObject brokenHeart; // Charm Animation
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
        if (Input.GetKeyDown(KeyCode.Alpha1) && gm.selectedUnit != null && gm.selectedUnit.name == "El General Calavera(Clone)" && gm.selectedUnit.actionPoints >= 2 && gm.selectedUnit.exploracionCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            Exploracion(gm.selectedUnit);   
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2) && gm.selectedUnit != null && gm.selectedUnit.name == "El General Calavera(Clone)" && gm.selectedUnit.actionPoints >= 4 && gm.selectedUnit.ametralladoraCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            Ametralladora(gm.selectedUnit);   
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha3) && gm.selectedUnit != null && gm.selectedUnit.name == "El General Calavera(Clone)" && gm.selectedUnit.actionPoints >= 2 && gm.selectedUnit.crearMunicionesCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            CrearMuniciones(gm.selectedUnit);
            gm.UpdateStatsPanel();      
        }
    }

    public void Exploracion(Unit unit)
    {
        if (unit.exploracionCast == false)
        {
            photonView.RPC("ExploracionAnimation", RpcTarget.All);
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.exploracionCast = true;
            unit.actionPoints -= 2;
            unit.actionPoints = unit.actionPoints * 2;
            unit.cantAttack = true;
            StartCoroutine(ExploracionUncast(gm.selectedUnit, 10f));
        }
    }

    [PunRPC]
    public void ExploracionAnimation()
    {
        Instantiate(magicAuraBRunic, this.transform.position, Quaternion.identity);
        Instantiate(brokenHeart, this.transform.position, Quaternion.identity);
    }

    IEnumerator ExploracionUncast(Unit unit, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        unit.cantAttack = false;
        unit.exploracionCast = false;
    }

    public void Ametralladora(Unit unit)
    {
        if (unit.ametralladoraCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.ametralladoraCast = true;
            unit.actionPoints -= 4;
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

    IEnumerator CrearMunicionesUncast(Unit unit, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        unit.maxAttackRange -= 1;
        unit.attackDamage = Mathf.Max(0, unit.attackDamage - 1);
    }

    [PunRPC]
    public void VictoryAnim()
    {
        character.Animator.SetTrigger("Victory Trigger");
    }
}