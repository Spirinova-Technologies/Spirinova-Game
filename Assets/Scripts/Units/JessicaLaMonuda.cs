using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Assets.HeroEditor.Common.CharacterScripts;

public class JessicaLaMonuda : MonoBehaviourPunCallbacks
{
    GameMaster gm;

    public Character character;
    List<Unit> alliesInAOERange = new List<Unit>();

    void Start()
    {
        gm = FindObjectOfType<GameMaster>();
        character = GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && gm.selectedUnit != null && gm.selectedUnit.name == "Jessica, la Monuda(Clone)" && gm.selectedUnit.actionPoints >= 2 && gm.selectedUnit.curacionCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            Curacion(gm.selectedUnit);     
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && gm.selectedUnit != null && gm.selectedUnit.name == "Jessica, la Monuda(Clone)" && gm.selectedUnit.actionPoints >= 4 && gm.selectedUnit.invocacionDeVelocidadCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            InvocacionDeVelocidad(gm.selectedUnit);     
        }
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

    public void InvocacionDeVelocidad(Unit unit)
    {
        if (unit.invocacionDeVelocidadCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.invocacionDeVelocidadCast = true;
            unit.actionPoints -= 4;
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
                        gm.selectedUnit.AttackUE(unitInRange, "canto");
                    }
                }
            }
            }
            unit.invocacionDeVelocidadCast = false;
        }
    }

    [PunRPC]
    public void VictoryAnim()
    {
        character.Animator.SetTrigger("Victory Trigger");
    }
}