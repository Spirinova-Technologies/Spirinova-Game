using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Assets.HeroEditor.Common.CharacterScripts;

public class DelphiElReyDeLosMundos : MonoBehaviourPunCallbacks
{
    GameMaster gm;

    public Character character;
    Unit unit;

    public GameObject fireShield; // Attack Buff Animation
    public GameObject pickupDiamond2; // Paralyze Animation
    public GameObject brokenHeart; // Charm Animation

    void Start()
    {
        gm = FindObjectOfType<GameMaster>();
        character = GetComponent<Character>();
        unit = GetComponent<Unit>();
        unit.hachaDeTrueno = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && gm.selectedUnit != null && gm.selectedUnit.name == "Delphi, el Rey de los Mundos(Clone)" && gm.selectedUnit.actionPoints >= 8 && gm.selectedUnit.karmaCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            Karma(gm.selectedUnit);
            gm.UpdateStatsPanel();      
        }
    }

    public void Karma(Unit unit)
    {
        if (unit.karmaCast == false)
        {
            photonView.RPC("KarmaAnimation", RpcTarget.All);
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.karmaCast = true;
            unit.actionPoints -= 8;
            unit.UpdateActionPointsText();
            Unit2 unit2 = GetComponent<Unit2>();
            unit.attackDamage += (unit2.maxHp - unit2.curHp);
            unit.cantMove = true;
            unit.cantAttack = true;
            StartCoroutine(KarmaUncast(gm.selectedUnit, 5f));
        }
    }

    [PunRPC]
    public void KarmaAnimation()
    {
        Instantiate(fireShield, this.transform.position, Quaternion.identity);
        Instantiate(pickupDiamond2, this.transform.position, Quaternion.identity);
        Instantiate(brokenHeart, this.transform.position, Quaternion.identity);
    }

    IEnumerator KarmaUncast(Unit unit, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        unit.cantMove = false;
        unit.cantAttack = false;
        unit.karmaCast = false;
    }

    [PunRPC]
    public void VictoryAnim()
    {
        character.Animator.SetTrigger("Victory Trigger");
    }
}