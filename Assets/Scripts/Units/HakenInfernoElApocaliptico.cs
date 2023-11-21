using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Assets.HeroEditor.Common.CharacterScripts;

public class HakenInfernoElApocaliptico : MonoBehaviourPunCallbacks
{
    GameMaster gm;

    public Character character;

    public GameObject pickupHeart; // Armor Buff

    void Start()
    {
        gm = FindObjectOfType<GameMaster>();
        character = GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && gm.selectedUnit != null && gm.selectedUnit.name == "Haken Inferno, el Apocaliptico(Clone)" && gm.selectedUnit.actionPoints >= 1 && gm.selectedUnit.hachaDelDemonioCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            HachaDelDemonio(gm.selectedUnit);   
        }
       
        if (Input.GetKeyDown(KeyCode.Alpha2) && gm.selectedUnit != null && gm.selectedUnit.name == "Haken Inferno, el Apocaliptico(Clone)" && gm.selectedUnit.actionPoints >= 6 && gm.selectedUnit.escudoDeDragonCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            EscudoDeDragon(gm.selectedUnit);
            gm.UpdateStatsPanel();      
        }
    }

    public void HachaDelDemonio(Unit unit)
    {
        if (unit.hachaDelDemonioCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.hachaDelDemonioCast = true;
            unit.actionPoints -= 1;
            unit.UpdateActionPointsText();
        }
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