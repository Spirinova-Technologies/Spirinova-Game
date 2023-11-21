using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Assets.HeroEditor.Common.CharacterScripts;

public class ElReyFantasma : MonoBehaviourPunCallbacks
{
    GameMaster gm;

    public Character character;
    Unit unit;

    public GameObject pickupHeart;
    public GameObject fireShield; // Attack Buff Animation

    void Start()
    {
        gm = FindObjectOfType<GameMaster>();
        character = GetComponent<Character>();
        unit = GetComponent<Unit>();
        unit.dobleAtaqueDeDemonio = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && gm.selectedUnit != null && gm.selectedUnit.name == "El Rey Fantasma(Clone)" && gm.selectedUnit.actionPoints >= 6 && gm.selectedUnit.ordenRealCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            OrdenReal(gm.selectedUnit);
            gm.UpdateStatsPanel();      
        }
    }

    public void OrdenReal(Unit unit)
    {
        if (unit.ordenRealCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.ordenRealCast = true;
            unit.actionPoints -= 6;
            foreach (Unit units in FindObjectsOfType<Unit>())
            {
                units.photonView.RPC("OrdenRealAnimation", RpcTarget.All);
                units.attackDamage += 1;
                units.physicalArmor += 1;
                units.defenseDamage += 1;
            }
            gm.UpdateStatsPanel();
            unit.ordenRealCast = false;
        }
    }

    [PunRPC]
    public void OrdenRealAnimation()
    {
        Instantiate(fireShield, this.transform.position, Quaternion.identity);
        Instantiate(pickupHeart, this.transform.position, Quaternion.identity);
    }

    [PunRPC]
    public void VictoryAnim()
    {
        character.Animator.SetTrigger("Victory Trigger");
    }
}