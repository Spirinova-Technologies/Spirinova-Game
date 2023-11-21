using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Assets.HeroEditor.Common.CharacterScripts;

public class ElDruidaDeLaTierra : MonoBehaviourPunCallbacks
{
    GameMaster gm;

    public Character character;
    List<Unit> alliesInAOERange = new List<Unit>();
    List<Unit> enemiesInAOERange = new List<Unit>();

    public GameObject bubblesWhirl; // Danza de Lluvia Animation
    public GameObject bigSplash; // Danza de Lluvia Debuff Animation

    void Start()
    {
        gm = FindObjectOfType<GameMaster>();
        character = GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && gm.selectedUnit != null && gm.selectedUnit.name == "El Druida de la Tierra(Clone)" && gm.selectedUnit.actionPoints >= 6 && gm.selectedUnit.sonidoDeTigreCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            SonidoDeTigre(gm.selectedUnit);     
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && gm.selectedUnit != null && gm.selectedUnit.name == "El Druida de la Tierra(Clone)" && gm.selectedUnit.actionPoints >= 6 && gm.selectedUnit.danzaDeLluviaCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            DanzaDeLluvia(gm.selectedUnit);     
        }
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

    public void DanzaDeLluvia(Unit unit)
    {
        if (unit.danzaDeLluviaCast == false)
        {
            photonView.RPC("DanzaDeLluviaAnimation", RpcTarget.All);
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.danzaDeLluviaCast = true;
            unit.actionPoints -= 6;
            foreach (Unit units in FindObjectsOfType<Unit>())
            {
                units.photonView.RPC("DanzaDeLluviaDebuffAnimation", RpcTarget.All);
                units.danzaDeLluviaCast = true;
                units.StartCoroutine(DanzaDeLluviaUncast(units, 10f));
            }
            photonView.RPC("DanzaDeLluviaEnemy", RpcTarget.Others);
            unit.danzaDeLluviaCast = false;
        }
    }

    [PunRPC]
    public void DanzaDeLluviaAnimation()
    {
        Instantiate(bubblesWhirl, this.transform.position, Quaternion.identity);
    }

    [PunRPC]
    public void DanzaDeLluviaEnemy()
    {
        foreach (Unit units in FindObjectsOfType<Unit>())
        {
            units.photonView.RPC("DanzaDeLluviaDebuffAnimation", RpcTarget.All);
            units.danzaDeLluviaCast = true;
            units.StartCoroutine(DanzaDeLluviaUncast(units, 10f));
        }
    }

    [PunRPC]
    public void DanzaDeLluviaDebuffAnimation()
    {
        Instantiate(bigSplash, this.transform.position, Quaternion.identity);
    }

    IEnumerator DanzaDeLluviaUncast(Unit unit, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        unit.danzaDeLluviaCast = false;
    }

    [PunRPC]
    public void VictoryAnim()
    {
        character.Animator.SetTrigger("Victory Trigger");
    }
}