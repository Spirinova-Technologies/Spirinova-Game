using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Assets.HeroEditor.Common.CharacterScripts;

public class ElTroncoVerde : MonoBehaviourPunCallbacks
{
    GameMaster gm;

    public Character character;
    List<Unit> alliesInAOERange = new List<Unit>();

    public GameObject pickupHeart; // Armor Buff

    void Start()
    {
        gm = FindObjectOfType<GameMaster>();
        character = GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && gm.selectedUnit != null && gm.selectedUnit.name == "El Tronco Verde(Clone)" && gm.selectedUnit.actionPoints >= 2 && gm.selectedUnit.entrenamientoCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            Entrenamiento(gm.selectedUnit);  
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && gm.selectedUnit != null && gm.selectedUnit.name == "El Tronco Verde(Clone)" && gm.selectedUnit.actionPoints >= 6 && gm.selectedUnit.furiaDeLasBestiasCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            FuriaDeLasBestias(gm.selectedUnit);  
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

    public void FuriaDeLasBestias(Unit unit)
    {
        if (unit.furiaDeLasBestiasCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.furiaDeLasBestiasCast = true;
            unit.actionPoints -= 6;
            alliesInAOERange.Clear();
            
            foreach (Unit unitInRange in FindObjectsOfType<Unit>())
            {
            if ((Mathf.Abs(transform.position.x - unitInRange.transform.position.x) + Mathf.Abs(transform.position.y - unitInRange.transform.position.y) <= 2))
            {
                if (unit.playerNumber == unitInRange.playerNumber)
                {
                    this.alliesInAOERange.Add(unitInRange);
                    if (this.alliesInAOERange.Contains(unitInRange))
                    {
                        gm.selectedUnit.AttackUE(unitInRange, "furiaDeLasBestiasAlly");
                    }
                }
            }
            }
            unit.furiaDeLasBestiasCast = false;
        }
    }

    [PunRPC]
    public void VictoryAnim()
    {
        character.Animator.SetTrigger("Victory Trigger");
    }
}