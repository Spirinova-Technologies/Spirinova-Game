using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Assets.HeroEditor.Common.CharacterScripts;

public class LuisElAngel666 : MonoBehaviourPunCallbacks
{
    GameMaster gm;

    public Character character;
    Unit unit;
    List<Unit> alliesInAOERange = new List<Unit>();
    List<Unit> enemiesInAOERange = new List<Unit>();

    public GameObject pickupStar; // Inmortalidad & Renacimiento Animation 1/2
    public GameObject resurrectionLightCircle; // Grito del Dragon, Inmortalidad & Renacimiento 2/2 Animation

    void Start()
    {
        gm = FindObjectOfType<GameMaster>();
        character = GetComponent<Character>();
        unit = GetComponent<Unit>();
        unit.lanzaInfernal = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && gm.selectedUnit != null && gm.selectedUnit.name == "Luis, el Angel 666(Clone)" && gm.selectedUnit.actionPoints >= 6 && gm.selectedUnit.renacimientoCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            Renacimiento(gm.selectedUnit);     
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && gm.selectedUnit != null && gm.selectedUnit.name == "Luis, el Angel 666(Clone)" && gm.selectedUnit.actionPoints >= 6 && gm.selectedUnit.invocacionDeAngelesCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            InvocacionDeAngeles(gm.selectedUnit);     
        }
    }

    public void Renacimiento(Unit unit)
    {
        if (unit.renacimientoCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.renacimientoCast = true;
            unit.actionPoints -= 6;
            
            foreach (Unit allies in FindObjectsOfType<Unit>())
            {
                Unit2 unit2 = allies.GetComponent<Unit2>();
                if (unit.playerNumber == allies.playerNumber)
                {
                    allies.photonView.RPC("RenacimientoAnimation", RpcTarget.All);
                    allies.health = unit2.maxHp;
                    allies.photonView.RPC("Heal", RpcTarget.All, (unit2.maxHp - allies.health)); // Se cambio de attackDamage a enemyDamage
                }
            }
        }
    }

    [PunRPC]
    public void RenacimientoAnimation()
    {
        Instantiate(pickupStar, this.transform.position, Quaternion.identity);
        Instantiate(resurrectionLightCircle, this.transform.position, Quaternion.identity);
    }

    public void InvocacionDeAngeles(Unit unit)
    {
        if (unit.invocacionDeAngelesCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.invocacionDeAngelesCast = true;
            unit.actionPoints -= 6;
            enemiesInAOERange.Clear();
            alliesInAOERange.Clear();
            
            foreach (Unit unitInRange in FindObjectsOfType<Unit>())
            {
            if ((Mathf.Abs(transform.position.x - unitInRange.transform.position.x) + Mathf.Abs(transform.position.y - unitInRange.transform.position.y) <= 2))
            {
                if (unit.playerNumber != unitInRange.playerNumber)
                {
                    this.enemiesInAOERange.Add(unitInRange);
                    if (this.enemiesInAOERange.Contains(unitInRange))
                    {
                        gm.selectedUnit.AttackUE(unitInRange, "invocacionDeAngelesEnemy");
                    }
                }

                if (unit.playerNumber == unitInRange.playerNumber)
                {
                    this.alliesInAOERange.Add(unitInRange);
                    if (this.alliesInAOERange.Contains(unitInRange))
                    {
                        gm.selectedUnit.AttackUE(unitInRange, "invocacionDeAngelesAlly");
                    }
                }
            }
            }
            unit.invocacionDeAngelesCast = false;
        }
    }

    [PunRPC]
    public void VictoryAnim()
    {
        character.Animator.SetTrigger("Victory Trigger");
    }
}