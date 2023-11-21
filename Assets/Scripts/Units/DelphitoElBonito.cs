using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Assets.HeroEditor.Common.CharacterScripts;

public class DelphitoElBonito : MonoBehaviourPunCallbacks
{
    GameMaster gm;

    public Character character;
    List<Unit> enemiesInAOERange = new List<Unit>();
    List<Unit> alliesInAOERange = new List<Unit>();

    void Start()
    {
        gm = FindObjectOfType<GameMaster>();
        character = GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && gm.selectedUnit != null && gm.selectedUnit.name == "Delphito, el Bonito(Clone)" && gm.selectedUnit.actionPoints >= 6 && gm.selectedUnit.hacerOjitosCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            HacerOjitos(gm.selectedUnit);     
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && gm.selectedUnit != null && gm.selectedUnit.name == "Delphito, el Bonito(Clone)" && gm.selectedUnit.actionPoints >= 6 && gm.selectedUnit.contarChisteCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            ContarChiste(gm.selectedUnit);     
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha3) && gm.selectedUnit != null && gm.selectedUnit.name == "Delphito, el Bonito(Clone)" && gm.selectedUnit.actionPoints >= 6 && gm.selectedUnit.lloriqueoCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            Lloriqueo(gm.selectedUnit);     
        }
    }

    public void HacerOjitos(Unit unit)
    {
        if (unit.hacerOjitosCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.hacerOjitosCast = true;
            unit.actionPoints -= 6;
            enemiesInAOERange.Clear();
            
            foreach (Unit unitInRange in FindObjectsOfType<Unit>())
            {
            if ((Mathf.Abs(transform.position.x - unitInRange.transform.position.x) + Mathf.Abs(transform.position.y - unitInRange.transform.position.y) <= 3))
            {
                if (unit.playerNumber != unitInRange.playerNumber)
                {
                    this.enemiesInAOERange.Add(unitInRange);
                    if (this.enemiesInAOERange.Contains(unitInRange))
                    {
                        gm.selectedUnit.AttackUE(unitInRange, "hacerOjitosEnemy");
                    }
                }
            }
            }
            unit.hacerOjitosCast = false;
        }
    }

    public void ContarChiste(Unit unit)
    {
        if (unit.contarChisteCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.contarChisteCast = true;
            unit.actionPoints -= 6;
            enemiesInAOERange.Clear();
            alliesInAOERange.Clear();
            
            foreach (Unit unitInRange in FindObjectsOfType<Unit>())
            {
            if ((Mathf.Abs(transform.position.x - unitInRange.transform.position.x) + Mathf.Abs(transform.position.y - unitInRange.transform.position.y) <= 3))
            {
                if (unit.playerNumber != unitInRange.playerNumber)
                {
                    this.enemiesInAOERange.Add(unitInRange);
                    if (this.enemiesInAOERange.Contains(unitInRange))
                    {
                        gm.selectedUnit.AttackUE(unitInRange, "contarChisteEnemy");
                    }
                }

                if (unit.playerNumber == unitInRange.playerNumber)
                {
                    this.alliesInAOERange.Add(unitInRange);
                    if (this.alliesInAOERange.Contains(unitInRange))
                    {
                        gm.selectedUnit.AttackUE(unitInRange, "contarChisteAlly");
                    }
                }
            }
            }
            unit.contarChisteCast = false;
        }
    }
    
    public void Lloriqueo(Unit unit)
    {
        if (unit.lloriqueoCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.lloriqueoCast = true;
            unit.actionPoints -= 6;
            enemiesInAOERange.Clear();
            alliesInAOERange.Clear();
            
            foreach (Unit unitInRange in FindObjectsOfType<Unit>())
            {
            if ((Mathf.Abs(transform.position.x - unitInRange.transform.position.x) + Mathf.Abs(transform.position.y - unitInRange.transform.position.y) <= 3))
            {
                if (unit.playerNumber != unitInRange.playerNumber)
                {
                    this.enemiesInAOERange.Add(unitInRange);
                    if (this.enemiesInAOERange.Contains(unitInRange))
                    {
                        gm.selectedUnit.AttackUE(unitInRange, "lloriqueoEnemy");
                    }
                }

                if (unit.playerNumber == unitInRange.playerNumber)
                {
                    this.alliesInAOERange.Add(unitInRange);
                    if (this.alliesInAOERange.Contains(unitInRange))
                    {
                        gm.selectedUnit.AttackUE(unitInRange, "lloriqueoAlly");
                    }
                }
            }
            }
            unit.lloriqueoCast = false;
        }
    }

    [PunRPC]
    public void VictoryAnim()
    {
        character.Animator.SetTrigger("Victory Trigger");
    }
}