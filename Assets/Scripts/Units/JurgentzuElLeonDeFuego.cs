using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Assets.HeroEditor.Common.CharacterScripts;

public class JurgentzuElLeonDeFuego : MonoBehaviourPunCallbacks
{
    GameMaster gm;

    public Character character;
    List<Unit> enemiesInAOERange = new List<Unit>();

    void Start()
    {
        gm = FindObjectOfType<GameMaster>();
        character = GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && gm.selectedUnit != null && gm.selectedUnit.name == "Jurgentzu, el Leon de Fuego(Clone)" && gm.selectedUnit.actionPoints >= 6 && gm.selectedUnit.implosionNuclearCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            ImplosionNuclear(gm.selectedUnit);     
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && gm.selectedUnit != null && gm.selectedUnit.name == "Jurgentzu, el Leon de Fuego(Clone)" && gm.selectedUnit.actionPoints >= 4 && gm.selectedUnit.explosionCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            Explosion(gm.selectedUnit);     
        }
        
    }

    public void ImplosionNuclear(Unit unit)
    {
        if (unit.implosionNuclearCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.implosionNuclearCast = true;
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
                        gm.selectedUnit.AttackUE(unitInRange, "demon");
                    }
                }
            }
            }
            unit.implosionNuclearCast = false;
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

    
}