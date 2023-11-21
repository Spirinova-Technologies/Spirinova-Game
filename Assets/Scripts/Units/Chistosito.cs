using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Assets.HeroEditor.Common.CharacterScripts;

public class Chistosito : MonoBehaviourPunCallbacks
{
    GameMaster gm;

    public Character character;
    List<Unit> enemiesInAOERange = new List<Unit>();
    List<Unit> alliesInAOERange = new List<Unit>();
    Unit unit;

    void Start()
    {
        gm = FindObjectOfType<GameMaster>();
        character = GetComponent<Character>();
        unit = GetComponent<Unit>();
        unit.absorcionDeEnergia = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && gm.selectedUnit != null && gm.selectedUnit.name == "Chistosito(Clone)" && gm.selectedUnit.actionPoints >= 4 && gm.selectedUnit.explosionCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            Explosion(gm.selectedUnit);     
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && gm.selectedUnit != null && gm.selectedUnit.name == "Chistosito(Clone)" && gm.selectedUnit.actionPoints >= 4 && gm.selectedUnit.baileCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            Baile(gm.selectedUnit);     
        }
        
    }

    public void Explosion(Unit unit)
    {
        if (unit.explosionCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.explosionCast = true;
            unit.actionPoints -= 4;
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
                        gm.selectedUnit.AttackUE(unitInRange, "demon");
                    }
                }
            }
            }
            unit.explosionCast = false;
        }
    }

    public void Baile(Unit unit)
    {
        if (unit.baileCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.baileCast = true;
            unit.actionPoints -= 4;
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
                        gm.selectedUnit.AttackUE(unitInRange, "baile");
                    }
                }
            }
            }
            unit.baileCast = false;
        }
    }

    [PunRPC]
    public void VictoryAnim()
    {
        character.Animator.SetTrigger("Victory Trigger");
    }
}