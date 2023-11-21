using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Assets.HeroEditor.Common.CharacterScripts;

public class Rakata : MonoBehaviourPunCallbacks
{
    GameMaster gm;

    public Character character;

    public GameObject pickupHeart;
    public GameObject fireShield; // Attack Buff Animation

    void Start()
    {
        gm = FindObjectOfType<GameMaster>();
        character = GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && gm.selectedUnit != null && gm.selectedUnit.name == "Rakata(Clone)" && gm.selectedUnit.actionPoints >= 6 && gm.selectedUnit.ordenRealCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            OrdenReal(gm.selectedUnit);
            gm.UpdateStatsPanel();      
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2) && gm.selectedUnit != null && gm.selectedUnit.name == "Rakata(Clone)" && gm.selectedUnit.actionPoints >= 4 && gm.selectedUnit.granadaAturdidoraCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            GranadaAturdidora(gm.selectedUnit);   
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
            unit.conquistaCast = false;
        }
    }

    [PunRPC]
    public void OrdenRealAnimation()
    {
        Instantiate(fireShield, this.transform.position, Quaternion.identity);
        Instantiate(pickupHeart, this.transform.position, Quaternion.identity);
    }

    public void GranadaAturdidora(Unit unit)
    {
        if (unit.granadaAturdidoraCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.granadaAturdidoraCast = true;
            unit.actionPoints -= 4;
            unit.UpdateActionPointsText();
        }
    }

    [PunRPC]
    public void VictoryAnim()
    {
        character.Animator.SetTrigger("Victory Trigger");
    }
}