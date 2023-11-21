using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Assets.HeroEditor.Common.CharacterScripts;

public class EmethElDuendeTravieso : MonoBehaviourPunCallbacks
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
        if (Input.GetKeyDown(KeyCode.Alpha1) && gm.selectedUnit != null && gm.selectedUnit.name == "Emeth, el Duende Travieso(Clone)" && gm.selectedUnit.actionPoints >= 2 && gm.selectedUnit.alquimiaCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            Alquimia(gm.selectedUnit);     
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2) && gm.selectedUnit != null && gm.selectedUnit.name == "Emeth, el Duende Travieso(Clone)" && gm.selectedUnit.actionPoints >= 4 && gm.selectedUnit.baileCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            Baile(gm.selectedUnit);     
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && gm.selectedUnit != null && gm.selectedUnit.name == "Emeth, el Duende Travieso(Clone)" && gm.selectedUnit.actionPoints >= 4 && gm.selectedUnit.sonidoAturdidorCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            SonidoAturdidor(gm.selectedUnit);     
        }
    }

    public void Alquimia(Unit unit)
    {
        if (unit.alquimiaCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.alquimiaCast = true;
            unit.actionPoints -= 2;
            unit.UpdateActionPointsText();
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

    public void SonidoAturdidor(Unit unit)
    {
        if (unit.sonidoAturdidorCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.sonidoAturdidorCast = true;
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
                        gm.selectedUnit.AttackUE(unitInRange, "sonido aturdidor");
                    }
                }
            }
            }
            unit.sonidoAturdidorCast = false;
        }
    }

    [PunRPC]
    public void VictoryAnim()
    {
        character.Animator.SetTrigger("Victory Trigger");
    }
}