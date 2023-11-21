using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Assets.HeroEditor.Common.CharacterScripts;

public class ElHechizeroDeLaNaturaleza : MonoBehaviourPunCallbacks
{
    GameMaster gm;

    public Character character;
    List<Unit> enemiesInAOERange = new List<Unit>();
    List<Unit> alliesInAOERange = new List<Unit>();

    public GameObject brokenHeart; // Charm Animation

    void Start()
    {
        gm = FindObjectOfType<GameMaster>();
        character = GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && gm.selectedUnit != null && gm.selectedUnit.name == "El Hechizero de la Naturaleza(Clone)" && gm.selectedUnit.actionPoints >= 6 && gm.selectedUnit.sonidoDeTigreCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            SonidoDeTigre(gm.selectedUnit);     
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && gm.selectedUnit != null && gm.selectedUnit.name == "El Hechizero de la Naturaleza(Clone)" && gm.selectedUnit.actionPoints >= 6 && gm.selectedUnit.maldicionCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            Maldicion(gm.selectedUnit);     
        }
    }

    public void SonidoDeTigre(Unit unit)
    {
        if (unit.sonidoDeTigreCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.sonidoDeTigreCast = true;
            unit.actionPoints -= 6;
            enemiesInAOERange.Clear();
            alliesInAOERange.Clear();
            
            foreach (Unit unitInRange in FindObjectsOfType<Unit>())
            {
            if ((Mathf.Abs(transform.position.x - unitInRange.transform.position.x) + Mathf.Abs(transform.position.y - unitInRange.transform.position.y) <= 2))
            {
                if (unit.playerNumber != unitInRange.playerNumber)
                {
                    this.enemiesInAOERange.Add(unitInRange);
                    if (this.enemiesInAOERange.Contains(unitInRange))
                    {
                        gm.selectedUnit.AttackUE(unitInRange, "sonidoDeTigreEnemy");
                    }
                }

                if (unit.playerNumber == unitInRange.playerNumber)
                {
                    this.alliesInAOERange.Add(unitInRange);
                    if (this.alliesInAOERange.Contains(unitInRange))
                    {
                        gm.selectedUnit.AttackUE(unitInRange, "sonidoDeTigreAlly");
                    }
                }
            }
            }
            unit.sonidoDeTigreCast = false;
        }
    }

    public void Maldicion(Unit unit)
    {
        if (unit.maldicionCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.maldicionCast = true;
            unit.actionPoints -= 6;
            foreach (Unit units in FindObjectsOfType<Unit>())
            {
                units.photonView.RPC("MaldicionAnimation", RpcTarget.All);
                units.cantAttack = true;
                StartCoroutine(MaldicionUncast(units, 5f));
            }
            unit.photonView.RPC("MaldicionEnemy", RpcTarget.Others);
        }
        unit.maldicionCast = false;
    }

    [PunRPC]
    public void MaldicionAnimation()
    {
        Instantiate(brokenHeart, this.transform.position, Quaternion.identity);
    }

    IEnumerator MaldicionUncast(Unit unit, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        unit.cantAttack = false;
    }

    [PunRPC]
    public void VictoryAnim()
    {
        character.Animator.SetTrigger("Victory Trigger");
    }
}