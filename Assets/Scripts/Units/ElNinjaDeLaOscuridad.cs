using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Assets.HeroEditor.Common.CharacterScripts;

public class ElNinjaDeLaOscuridad : MonoBehaviourPunCallbacks
{
    GameMaster gm;

    public Character character;
    Unit unit;

    public GameObject fireShield; // Attack Buff Animation

    void Start()
    {
        gm = FindObjectOfType<GameMaster>();
        character = GetComponent<Character>();
        unit = GetComponent<Unit>();
        unit.katana = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && gm.selectedUnit != null && gm.selectedUnit.name == "El Ninja de la Oscuridad(Clone)" && gm.selectedUnit.actionPoints >= 2 && gm.selectedUnit.fuerzaCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            Fuerza(gm.selectedUnit);  
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

    [PunRPC]
    public void VictoryAnim()
    {
        character.Animator.SetTrigger("Victory Trigger");
    }
}