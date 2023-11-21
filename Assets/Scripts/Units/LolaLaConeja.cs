using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Assets.HeroEditor.Common.CharacterScripts;

public class LolaLaConeja : MonoBehaviourPunCallbacks
{
    GameMaster gm;

    public Character character;
    List<Unit> alliesInAOERange = new List<Unit>();
    List<Unit> enemiesInAOERange = new List<Unit>();

    void Start()
    {
        gm = FindObjectOfType<GameMaster>();
        character = GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && gm.selectedUnit != null && gm.selectedUnit.name == "Lola, la Coneja(Clone)" && gm.selectedUnit.actionPoints >= 3 && gm.selectedUnit.cantoCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            Canto(gm.selectedUnit);     
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2) && gm.selectedUnit != null && gm.selectedUnit.name == "Lola, la Coneja(Clone)" && gm.selectedUnit.actionPoints >= 6 && gm.selectedUnit.serenataCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            Serenata(gm.selectedUnit);     
        }
    }

    public void Canto(Unit unit)
    {
        if (unit.cantoCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.cantoCast = true;
            unit.actionPoints -= 3;
            alliesInAOERange.Clear();
            
            foreach (Unit unitInRange in FindObjectsOfType<Unit>())
            {
            if ((Mathf.Abs(transform.position.x - unitInRange.transform.position.x) + Mathf.Abs(transform.position.y - unitInRange.transform.position.y) <= 3))
            {
                if (unit.playerNumber == unitInRange.playerNumber)
                {
                    this.alliesInAOERange.Add(unitInRange);
                    if (this.alliesInAOERange.Contains(unitInRange))
                    {
                        gm.selectedUnit.AttackUE(unitInRange, "canto");
                    }
                }
            }
            }
            unit.cantoCast = false;
        }
    }

    public void Serenata(Unit unit)
    {
        if (unit.serenataCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.serenataCast = true;
            unit.actionPoints -= 6;
            enemiesInAOERange.Clear();
            
            foreach (Unit unitInRange in FindObjectsOfType<Unit>())
            {
            if ((Mathf.Abs(transform.position.x - unitInRange.transform.position.x) + Mathf.Abs(transform.position.y - unitInRange.transform.position.y) <= 2))
            {
                if (unit.playerNumber != unitInRange.playerNumber)
                {
                    this.enemiesInAOERange.Add(unitInRange);
                    if (this.enemiesInAOERange.Contains(unitInRange))
                    {
                        gm.selectedUnit.AttackUE(unitInRange, "serenataEnemy");
                    }
                }
            }
            }
            unit.serenataCast = false;
        }
    }

    [PunRPC]
    public void VictoryAnim()
    {
        character.Animator.SetTrigger("Victory Trigger");
    }
}