using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Assets.HeroEditor.Common.CharacterScripts;

public class GerardoElAngelGuardian : MonoBehaviourPunCallbacks
{
    GameMaster gm;

    public Character character;
    Unit unit;

    public GameObject pickupHeart; // Armor Buff

    void Start()
    {
        gm = FindObjectOfType<GameMaster>();
        character = GetComponent<Character>();
        unit = GetComponent<Unit>();
        unit.escudoMistico = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && gm.selectedUnit != null && gm.selectedUnit.name == "Gerardo, el Angel Guardian(Clone)" && gm.selectedUnit.actionPoints >= 4 && gm.selectedUnit.posicionDeDefensaCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            PosicionDeDefensa(gm.selectedUnit);
            gm.UpdateStatsPanel();      
        }
    }

    public void PosicionDeDefensa(Unit unit)
    {
        if (unit.posicionDeDefensaCast == false)
        {
            photonView.RPC("PosicionDeDefensaAnimation", RpcTarget.All);
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.posicionDeDefensaCast = true;
            unit.actionPoints -= 4;
            unit.UpdateActionPointsText();
            unit.physicalArmor += 1;
            unit.defenseDamage += 1;
            unit.posicionDeDefensaCast = false;
        }
    }

    [PunRPC]
    void PosicionDeDefensaAnimation ()
    {   
        Instantiate(pickupHeart, this.transform.position, Quaternion.identity);
    }

    [PunRPC]
    public void VictoryAnim()
    {
        character.Animator.SetTrigger("Victory Trigger");
    }
}