using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Assets.HeroEditor.Common.CharacterScripts;

public class ElGuerreroCalavera : MonoBehaviourPunCallbacks
{
    GameMaster gm;

    public Character character;

    public GameObject pickupHeart; // Armor Buff
    public GameObject fireShield; // Attack Buff Animation

    void Start()
    {
        gm = FindObjectOfType<GameMaster>();
        character = GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && gm.selectedUnit != null && gm.selectedUnit.name == "El Guerrero Calavera(Clone)" && gm.selectedUnit.actionPoints >= 2 && gm.selectedUnit.entrenamientoCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            Entrenamiento(gm.selectedUnit);  
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2) && gm.selectedUnit != null && gm.selectedUnit.name == "El Guerrero Calavera(Clone)" && gm.selectedUnit.actionPoints >= 2 && gm.selectedUnit.iraCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            Ira(gm.selectedUnit);
            gm.UpdateStatsPanel();      
        }
    }

    public void Entrenamiento(Unit unit)
    {
        if (unit.entrenamientoCast == false)
        {
            photonView.RPC("EntrenamientoAnimation", RpcTarget.All);
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.entrenamientoCast = true;
            unit.actionPoints -= 2;
            unit.UpdateActionPointsText();
            unit.physicalArmor += 1;
            unit.entrenamientoCast = false;
            gm.UpdateStatsPanel();
            StartCoroutine(EntrenamientoUncast(gm.selectedUnit, 10f));
            gm.UpdateStatsPanel();
        }
    }

    [PunRPC]
    public void EntrenamientoAnimation()
    {
        Instantiate(pickupHeart, this.transform.position, Quaternion.identity);
    }

    IEnumerator EntrenamientoUncast(Unit unit, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        unit.physicalArmor -= 1;
    }

    public void Ira(Unit unit)
    {
        if (unit.iraCast == false)
        {
            photonView.RPC("IraAnimation", RpcTarget.All);
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.iraCast = true;
            unit.actionPoints -= 2;
            unit.UpdateActionPointsText();
            unit.attackDamage += 1;
            unit.health -= 2;
            unit.photonView.RPC("TakeDamage", PlayerController.enemy.photonPlayer, 2);
            unit.iraCast = false;
        }
    }

    [PunRPC]
    public void IraAnimation()
    {
        Instantiate(fireShield, this.transform.position, Quaternion.identity);
    }
    
    [PunRPC]
    public void VictoryAnim()
    {
        character.Animator.SetTrigger("Victory Trigger");
    }
}