using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Assets.HeroEditor.Common.CharacterScripts;

public class LoboLobito : MonoBehaviourPunCallbacks
{
    GameMaster gm;

    public Character character;
    List<Unit> enemiesInAOERange = new List<Unit>();
    List<Unit> alliesInAOERange = new List<Unit>();

    public GameObject drillAirHit; // Terremoto Animation
    public GameObject pickupDiamond2; // Paralyze Animation

    void Start()
    {
        gm = FindObjectOfType<GameMaster>();
        character = GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && gm.selectedUnit != null && gm.selectedUnit.name == "Lobo Lobito(Clone)" && gm.selectedUnit.actionPoints >= 6 && gm.selectedUnit.terremotoCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            Terremoto(gm.selectedUnit);
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2) && gm.selectedUnit != null && gm.selectedUnit.name == "Lobo Lobito(Clone)" && gm.selectedUnit.actionPoints >= 6 && gm.selectedUnit.sonidoDeTigreCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            SonidoDeTigre(gm.selectedUnit);     
        }
    }

    public void Terremoto(Unit unit)
    {
        if (unit.terremotoCast == false)
        {
            photonView.RPC("TerremotoAnimation", RpcTarget.All);
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.terremotoCast = true;
            unit.actionPoints -= 6;
            foreach (Unit units in FindObjectsOfType<Unit>())
            {
                units.photonView.RPC("TerremotoDebuffAnimation", RpcTarget.All);
                units.cantMove = true;
                StartCoroutine(TerremotoUncast(units, 10f));
            }
            photonView.RPC("TerremotoEnemy", RpcTarget.Others);
            unit.terremotoCast = false;
        }
    }

    [PunRPC]
    public void TerremotoAnimation()
    {
        Instantiate(drillAirHit, this.transform.position, Quaternion.identity);
    }

    [PunRPC]
    public void TerremotoEnemy()
    {
        foreach (Unit units in FindObjectsOfType<Unit>())
        {
            units.photonView.RPC("TerremotoDebuffAnimation", RpcTarget.All);
            units.cantMove = true;
            StartCoroutine(TerremotoUncast(units, 10f));
        }
    }

    [PunRPC]
    public void TerremotoDebuffAnimation()
    {
        Instantiate(pickupDiamond2, this.transform.position, Quaternion.identity);
    }

    IEnumerator TerremotoUncast(Unit unit, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        unit.cantMove = false;
    }

    public void SonidoDeTigre(Unit unit)
    {
        if (unit.sonidoDeTigreCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.sonidoDeTigreCast = true;
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
                        gm.selectedUnit.AttackUE(unitInRange, "sonidoDeTigreEnemy");
                    }
                }

                if (unit.playerNumber == unitInRange.playerNumber)
                {
                    this.alliesInAOERange.Add(unitInRange);
                    if (this.alliesInAOERange.Contains(unitInRange))
                    {
                        gm.selectedUnit.AttackUE(unitInRange, "sonidoDeTigreAlly");
                    }
                }
            }
            }
            unit.sonidoDeTigreCast = false;
        }
    }

    [PunRPC]
    public void VictoryAnim()
    {
        character.Animator.SetTrigger("Victory Trigger");
    }
}