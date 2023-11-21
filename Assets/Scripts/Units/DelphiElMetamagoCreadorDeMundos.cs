using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Assets.HeroEditor.Common.CharacterScripts;

public class DelphiElMetamagoCreadorDeMundos : MonoBehaviourPunCallbacks
{
    GameMaster gm;

    public Character character;
    List<Unit> enemiesInAOERange = new List<Unit>();
    public GameObject roseFire; // Explosion Estelar Animation

    void Start()
    {
        gm = FindObjectOfType<GameMaster>();
        character = GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && gm.selectedUnit != null && gm.selectedUnit.name == "Delphi, el Metamago Creador de Mundos(Clone)" && gm.selectedUnit.actionPoints >= 6 && gm.selectedUnit.explosionEstelarCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            ExplosionEstelar(gm.selectedUnit);     
        }
    }

    public void ExplosionEstelar(Unit unit)
    {
        if (unit.explosionEstelarCast == false)
        {
            photonView.RPC("ExplosionEstelarAnimation", RpcTarget.All);
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.explosionEstelarCast = true;
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
                        gm.selectedUnit.AttackUE(unitInRange, "universe");
                    }
                }
            }
            }
            unit.explosionEstelarCast = false;
        }
    }

    [PunRPC]
    public void ExplosionEstelarAnimation()
    {
        Instantiate(roseFire, this.transform.position, Quaternion.identity);
    }

    [PunRPC]
    public void VictoryAnim()
    {
        character.Animator.SetTrigger("Victory Trigger");
    }
}