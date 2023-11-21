using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Assets.HeroEditor.Common.CharacterScripts;

public class Delphina : MonoBehaviourPunCallbacks
{
    GameMaster gm;

    public Character character;
    Unit unit;
    List<Unit> alliesInAOERange = new List<Unit>();

    void Start()
    {
        gm = FindObjectOfType<GameMaster>();
        character = GetComponent<Character>();
        unit = GetComponent<Unit>();
        unit.perfumeDeRosas = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && gm.selectedUnit != null && gm.selectedUnit.name == "Delphina(Clone)" && gm.selectedUnit.actionPoints >= 12 && gm.selectedUnit.ordenDeLaEmperatrizCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            OrdenDeLaEmperatriz(gm.selectedUnit);
            gm.UpdateStatsPanel();      
        }
    }

    public void OrdenDeLaEmperatriz(Unit unit)
    {
        if (unit.ordenDeLaEmperatrizCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.ordenDeLaEmperatrizCast = true;
            unit.actionPoints -= 12;
            alliesInAOERange.Clear();
            
            foreach (Unit unitInRange in FindObjectsOfType<Unit>())
            {
            if ((Mathf.Abs(transform.position.x - unitInRange.transform.position.x) + Mathf.Abs(transform.position.y - unitInRange.transform.position.y) <= 2))
            {
                if (unit.playerNumber == unitInRange.playerNumber)
                {
                    this.alliesInAOERange.Add(unitInRange);
                    if (this.alliesInAOERange.Contains(unitInRange))
                    {
                        gm.selectedUnit.AttackUE(unitInRange, "ordenDeLaEmperatrizAlly");
                    }
                }
            }
            }
            unit.ordenDeLaEmperatrizCast = false;
        }
    }

    [PunRPC]
    public void VictoryAnim()
    {
        character.Animator.SetTrigger("Victory Trigger");
    }
}