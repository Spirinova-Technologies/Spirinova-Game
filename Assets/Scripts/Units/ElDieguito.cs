using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Assets.HeroEditor.Common.CharacterScripts;

public class ElDieguito : MonoBehaviourPunCallbacks
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
        unit.espadaDeFuego = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2) && gm.selectedUnit != null && gm.selectedUnit.name == "El Dieguito(Clone)" && gm.selectedUnit.actionPoints >= 4 && gm.selectedUnit.espadaDeLaMuerteCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            EspadaDeLaMuerte(gm.selectedUnit);
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha3) && gm.selectedUnit != null && gm.selectedUnit.name == "El Dieguito(Clone)" && gm.selectedUnit.actionPoints >= 6 && gm.selectedUnit.escudoDeDragonCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            EscudoDeDragon(gm.selectedUnit);
            gm.UpdateStatsPanel();      
        }
    }

    public void EspadaDeLaMuerte(Unit unit)
    {
        if (unit.espadaDeLaMuerteCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.espadaDeLaMuerteCast = true;
            unit.actionPoints -= 4;
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