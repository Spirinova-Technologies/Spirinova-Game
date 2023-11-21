using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Assets.HeroEditor.Common.CharacterScripts;

public class JurgensaurioElDragonIndomable : MonoBehaviourPunCallbacks
{
    GameMaster gm;

    public Character character;
    List<Unit> enemiesInAOERange = new List<Unit>();
    Unit unit;

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
        if (Input.GetKeyDown(KeyCode.Alpha1) && gm.selectedUnit != null && gm.selectedUnit.name == "Jurgensaurio, el Dragon Indomable(Clone)" && gm.selectedUnit.actionPoints >= 6 && gm.selectedUnit.implosionNuclearCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            ImplosionNuclear(gm.selectedUnit);     
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

    [PunRPC]
    public void VictoryAnim()
    {
        character.Animator.SetTrigger("Victory Trigger");
    }
}