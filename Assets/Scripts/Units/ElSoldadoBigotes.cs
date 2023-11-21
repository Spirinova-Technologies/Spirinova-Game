using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Assets.HeroEditor.Common.CharacterScripts;

public class ElSoldadoBigotes : MonoBehaviourPunCallbacks
{
    GameMaster gm;

    public Character character;

    public GameObject fireShield; // Attack Buff Animation
    public GameObject pickupHeart; // Armor Buff

    void Start()
    {
        gm = FindObjectOfType<GameMaster>();
        character = GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {   
        if (Input.GetKeyDown(KeyCode.Alpha1) && gm.selectedUnit != null && gm.selectedUnit.name == "El Soldado Bigotes(Clone)" && gm.selectedUnit.actionPoints >= 2 && gm.selectedUnit.fuerzaCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            Fuerza(gm.selectedUnit);  
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2) && gm.selectedUnit != null && gm.selectedUnit.name == "El Soldado Bigotes(Clone)" && gm.selectedUnit.actionPoints >= 2 && gm.selectedUnit.entrenamientoCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            Entrenamiento(gm.selectedUnit);  
        }
    }

    public void Fuerza(Unit unit)
    {
        if (unit.fuerzaCast == false)
        {
            photonView.RPC("FuerzaAnimation", RpcTarget.All);
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.fuerzaCast = true;
            unit.actionPoints -= 2;
            unit.UpdateActionPointsText();
            unit.attackDamage += 1;
            unit.fuerzaCast = false;
            gm.UpdateStatsPanel();
            StartCoroutine(FuerzaUncast(gm.selectedUnit, 10f));
            gm.UpdateStatsPanel();
        }
    }

    [PunRPC]
    public void FuerzaAnimation()
    {
        Instantiate(fireShield, this.transform.position, Quaternion.identity);
    }

    IEnumerator FuerzaUncast(Unit unit, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        unit.attackDamage = Mathf.Max(0, unit.attackDamage - 1);
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

    [PunRPC]
    public void VictoryAnim()
    {
        character.Animator.SetTrigger("Victory Trigger");
    }
}