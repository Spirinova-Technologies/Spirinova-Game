using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Assets.HeroEditor.Common.CharacterScripts;

public class MiriamDelphos : MonoBehaviourPunCallbacks
{
    GameMaster gm;

    public Character character;

    public GameObject magicCircleO;
    public GameObject magicAuraBRunic;
    public GameObject pickupHeart; // Armor Buff

    void Start()
    {
        gm = FindObjectOfType<GameMaster>();
        character = GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && gm.selectedUnit != null && gm.selectedUnit.name == "Miriam Delphos(Clone)" && gm.selectedUnit.actionPoints >= 6 && gm.selectedUnit.liberacionCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            Liberacion(gm.selectedUnit);   
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && gm.selectedUnit != null && gm.selectedUnit.name == "Miriam Delphos(Clone)" && gm.selectedUnit.actionPoints >= 6 && gm.selectedUnit.escudoDeDragonCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            EscudoDeDragon(gm.selectedUnit);
            gm.UpdateStatsPanel();      
        }
    }

    public void Liberacion(Unit unit)
    {
        if (unit.liberacionCast == false)
        {
            photonView.RPC("LiberacionAnimation", RpcTarget.All);
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.liberacionCast = true;
            unit.actionPoints -= 6;
            foreach (Unit units in FindObjectsOfType<Unit>())
            {
                units.photonView.RPC("LiberacionBuffAnimation", RpcTarget.All);
                units.actionPoints = units.actionPoints * 2;
            }
        }
    }

    [PunRPC]
    public void LiberacionAnimation()
    {
        Instantiate(magicCircleO, this.transform.position, Quaternion.identity);
    }

    [PunRPC]
    public void LiberacionBuffAnimation()
    {
        Instantiate(magicAuraBRunic, this.transform.position, Quaternion.identity);
    }

    public void EscudoDeDragon(Unit unit)
    {
        if (unit.escudoDeDragonCast == false)
        {
            photonView.RPC("EscudoDeDragonAnimation", RpcTarget.All);
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.escudoDeDragonCast = true;
            unit.actionPoints -= 6;
            unit.UpdateActionPointsText();
            unit.physicalArmor += 2;
            unit.hollyArmor += 1;
            unit.demonArmor += 1;
            unit.escudoDeDragonCast = false;
        }
    }

    [PunRPC]
    public void EscudoDeDragonAnimation()
    {
        Instantiate(pickupHeart, this.transform.position, Quaternion.identity);
    }

    [PunRPC]
    public void VictoryAnim()
    {
        character.Animator.SetTrigger("Victory Trigger");
    }
}