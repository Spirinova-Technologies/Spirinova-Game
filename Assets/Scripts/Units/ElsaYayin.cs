using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Assets.HeroEditor.Common.CharacterScripts;

public class ElsaYayin : MonoBehaviourPunCallbacks
{
    GameMaster gm;

    public Character character;
    Unit unit;

    public GameObject pickupDiamond2; // Paralyze Animation
    public GameObject brokenHeart; // Charm Animation
    public GameObject fireShield; // Attack Buff Animation
    public GameObject pickupHeart;

    void Start()
    {
        gm = FindObjectOfType<GameMaster>();
        character = GetComponent<Character>();
        unit = GetComponent<Unit>();
        unit.golpeLetal = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && gm.selectedUnit != null && gm.selectedUnit.name == "Elsa Yayin(Clone)" && gm.selectedUnit.actionPoints >= 4 && gm.selectedUnit.arteMarcialCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            ArteMarcial(gm.selectedUnit);
            gm.UpdateStatsPanel();      
        }
    }

    public void ArteMarcial(Unit unit)
    {
        if (unit.arteMarcialCast == false)
        {
            photonView.RPC("ArteMarcialAnimation", RpcTarget.All);
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.arteMarcialCast = true;
            unit.actionPoints -= 4;
            unit.UpdateActionPointsText();
            unit.attackDamage += 2;
            unit.physicalArmor += 1;
            unit.defenseDamage += 2;
            unit.cantMove = true;
            unit.cantAttack = true;
            StartCoroutine(ArteMarcialUncast(gm.selectedUnit, 5f));
        }
    }

    [PunRPC]
    public void ArteMarcialAnimation()
    {
        Instantiate(fireShield, this.transform.position, Quaternion.identity);
        Instantiate(pickupHeart, this.transform.position, Quaternion.identity);
        Instantiate(pickupDiamond2, this.transform.position, Quaternion.identity);
        Instantiate(brokenHeart, this.transform.position, Quaternion.identity);
    }

    IEnumerator ArteMarcialUncast(Unit unit, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        unit.cantMove = false;
        unit.cantAttack = false;
        unit.arteMarcialCast = false;
    }
}