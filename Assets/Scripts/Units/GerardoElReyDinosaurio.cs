using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Assets.HeroEditor.Common.CharacterScripts;

public class GerardoElReyDinosaurio : MonoBehaviourPunCallbacks
{
    GameMaster gm;

    public Character character;

    public GameObject darkMagicAuraA; // Attack Debuff Animation
    public GameObject pickupHeart; // Armor Buff
    public GameObject fireShield;

    void Start()
    {
        gm = FindObjectOfType<GameMaster>();
        character = GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && gm.selectedUnit != null && gm.selectedUnit.name == "Gerardo, el Rey Dinosaurio(Clone)" && gm.selectedUnit.actionPoints >= 6 && gm.selectedUnit.conquistaCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            Conquista(gm.selectedUnit);
            gm.UpdateStatsPanel();      
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && gm.selectedUnit != null && gm.selectedUnit.name == "Gerardo, el Rey Dinosaurio(Clone)" && gm.selectedUnit.actionPoints >= 4 && gm.selectedUnit.escamasDeDragonCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            EscamasDeDragon(gm.selectedUnit);
            gm.UpdateStatsPanel();      
        }
    }

    public void Conquista(Unit unit)
    {
        if (unit.conquistaCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.conquistaCast = true;
            unit.actionPoints -= 6;
            foreach (Unit units in FindObjectsOfType<Unit>())
            {
                units.photonView.RPC("ConquistaAnimation", RpcTarget.All);
                units.attackDamage += 1;
                units.physicalArmor += 1;
                units.defenseDamage += 1;
            }
            gm.UpdateStatsPanel();
            StartCoroutine(ConquistaUncast(gm.selectedUnit, 10f));
            gm.UpdateStatsPanel();
            unit.conquistaCast = false;
        }
    }

    [PunRPC]
    void ConquistaAnimation ()
    {   
        Instantiate(fireShield, this.transform.position, Quaternion.identity);
        Instantiate(pickupHeart, this.transform.position, Quaternion.identity);
    }

    IEnumerator ConquistaUncast(Unit unit, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        foreach (Unit units in FindObjectsOfType<Unit>())
            {
                units.attackDamage = Mathf.Max(0, units.attackDamage - 1);
                units.physicalArmor = Mathf.Max(0, units.physicalArmor  - 1);
                units.defenseDamage = Mathf.Max(0, units.defenseDamage - 1);
            }
    }

    public void EscamasDeDragon(Unit unit)
    {
        if (unit.escamasDeDragonCast == false)
        {
            photonView.RPC("EscamasDeDragonAnimation", RpcTarget.All);
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.escamasDeDragonCast = true;
            unit.actionPoints -= 4;
            unit.UpdateActionPointsText();
            unit.attackDamage = (int)(Mathf.Round(unit.attackDamage * 0.75f));
            unit.physicalArmor = (int)(Mathf.Round(unit.physicalArmor * 2));
            unit.escamasDeDragonCast = false;
        }
    }

    [PunRPC]
    public void EscamasDeDragonAnimation()
    {
        Instantiate(darkMagicAuraA, this.transform.position, Quaternion.identity);
        Instantiate(pickupHeart, this.transform.position, Quaternion.identity);
    }

    [PunRPC]
    public void VictoryAnim()
    {
        character.Animator.SetTrigger("Victory Trigger");
    }
}