using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Assets.HeroEditor.Common.CharacterScripts;

public class RebecaLaHeroina : MonoBehaviourPunCallbacks
{
    GameMaster gm;

    public Character character;
    List<Unit> alliesInAOERange = new List<Unit>();

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
        if (Input.GetKeyDown(KeyCode.Alpha1) && gm.selectedUnit != null && gm.selectedUnit.name == "Rebeca, la Heroina(Clone)" && gm.selectedUnit.actionPoints >= 2 && gm.selectedUnit.crearMunicionesCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            CrearMuniciones(gm.selectedUnit);
            gm.UpdateStatsPanel();      
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2) && gm.selectedUnit != null && gm.selectedUnit.name == "Rebeca, la Heroina(Clone)" && gm.selectedUnit.actionPoints >= 2 && gm.selectedUnit.curacionCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            Curacion(gm.selectedUnit);     
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

    public void Curacion(Unit unit)
    {
        if (unit.curacionCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.curacionCast = true;
            unit.actionPoints -= 2;
            alliesInAOERange.Clear();
            
            foreach (Unit unitInRange in FindObjectsOfType<Unit>())
            {
            if ((Mathf.Abs(transform.position.x - unitInRange.transform.position.x) + Mathf.Abs(transform.position.y - unitInRange.transform.position.y) <= 1))
            {
                if (unit.playerNumber == unitInRange.playerNumber)
                {
                    this.alliesInAOERange.Add(unitInRange);
                    if (this.alliesInAOERange.Contains(unitInRange))
                    {
                        gm.selectedUnit.HealUE(unitInRange, "curacion");
                    }
                }
            }
            }
            unit.curacionCast = false;
        }
    }

    [PunRPC]
    public void VictoryAnim()
    {
        character.Animator.SetTrigger("Victory Trigger");
    }
}