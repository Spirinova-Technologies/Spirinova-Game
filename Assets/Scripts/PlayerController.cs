//4
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerController : MonoBehaviourPun
{
    public Player photonPlayer;
    public string[] unitsToSpawn;
    public Transform[] spawnPoints;

    public List<Unit2> units = new List<Unit2>();
    public List<Spacebase> spacebases = new List<Spacebase>();
    private Unit2 selectedUnit;
    private Spacebase selectedSpacebase;

    public static PlayerController me;
    public static PlayerController enemy;

    [PunRPC]
    void Initialize (Player player, int playerNumber)
    {
        photonPlayer = player;

        // if this is our local player, spawn the units
        if(player.IsLocal)
        {
            me = this;
            SpawnUnits(playerNumber);
        }
        else
        {
            enemy = this;
        }

        // set the UI player text
        GameUI.instance.SetPlayerText(this);
    }

    void SpawnUnits (int playerNumber)
    {
        for(int x = 0; x < unitsToSpawn.Length; ++x)
        {
            GameObject unit = PhotonNetwork.Instantiate(unitsToSpawn[x], spawnPoints[x].position, Quaternion.identity);
            Unit unitScript = unit.GetComponent<Unit>();
            if(unitScript != null){
                unitScript.photonView.RPC("setPlayerNumber", RpcTarget.All, playerNumber);
            }
            else{
                Spacebase spaceBaseScript = unit.GetComponent<Spacebase>();
                if(spaceBaseScript != null){
                    spaceBaseScript.photonView.RPC("setPlayerNumber", RpcTarget.All, playerNumber);
                }
            }
            unit.GetPhotonView().RPC("Initialize", RpcTarget.Others, false);
            unit.GetPhotonView().RPC("Initialize", photonPlayer, true);
        }
    }

    void Update ()
    {
        if (GameManager.instance.curPlayer != this || this != me)
            return;

        if (Input.GetMouseButtonDown(0) && GameManager.instance.curPlayer == this)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            TrySelect(new Vector3(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y), 0));
            TrySelectSpacebase(new Vector3(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y), 0));
        }
    }

    void TrySelect (Vector3 selectPos)
    {
        // are we selecting our unit?
        Unit2 unit = units.Find(x => x.transform.position == selectPos);

        if(unit != null)
        {
            SelectUnit(unit);
            return;
        }

        if(!selectedUnit)
            return;

        // are we selecting an enemy unit?
        Unit2 enemyUnit = enemy.units.Find(x => x.transform.position == selectPos);

        if(enemyUnit != null)
        {
            TryAttack(enemyUnit);
            return;
        }

        TryMove(selectPos);
    }

    void TrySelectSpacebase (Vector3 selectPos)
    {
        // are we selecting our unit?
        Spacebase spacebase = spacebases.Find(x => x.transform.position == selectPos);

        if(spacebase != null)
        {
            SelectSpacebase(spacebase);
            return;
        }

        if(!selectedSpacebase)
            return;

        // are we selecting an enemy unit?
        Spacebase enemySpacebase = enemy.spacebases.Find(x => x.transform.position == selectPos);

        if(enemySpacebase != null)
        {
            TryAttackSpacebase(enemySpacebase);
            return;
        }

        TryMoveSpacebase(selectPos);
    }

    // called when we click on a unit
    void SelectUnit (Unit2 unitToSelect)
    {
        // can we select the unit?
        if(!unitToSelect.CanSelect())
            return;

        selectedUnit = unitToSelect;

        // set the unit info text
        GameUI.instance.SetUnitInfoText(selectedUnit);
    }

    void SelectSpacebase (Spacebase spacebaseToSelect)
    {
        // can we select the unit?
        if(!spacebaseToSelect.CanSelectSpacebase())
            return;

        selectedSpacebase = spacebaseToSelect;

        // set the unit info text
        GameUI.instance.SetSpacebaseInfoText(selectedSpacebase);
    }

    void DeSelectUnit ()
    {
        selectedUnit = null;

        // disable the unit info text
        GameUI.instance.unitInfoText.gameObject.SetActive(false);
    }

    void DeSelectSpacebase ()
    {
        selectedSpacebase = null;

        // disable the unit info text
        GameUI.instance.spacebaseInfoText.gameObject.SetActive(false);
    }

    void SelectNextAvailableUnit ()
    {
        Unit2 availableUnit = units.Find(x => x.CanSelect());

        if(availableUnit != null)
            SelectUnit(availableUnit);
        else
            DeSelectUnit();
    }

    void SelectNextAvailableSpacebase ()
    {
        Spacebase availableSpacebase = spacebases.Find(x => x.CanSelectSpacebase());

        if(availableSpacebase != null)
            SelectSpacebase(availableSpacebase);
        else
            DeSelectSpacebase();
    }

    void TryAttack (Unit2 enemyUnit)
    {
        if(selectedUnit.CanAttack(enemyUnit.transform.position))
        {
            selectedUnit.Attack(enemyUnit);

            // update the UI
            GameUI.instance.UpdateWaitingUnitsText(units.FindAll(x => x.CanSelect()).Count);
        }
    }

    void TryAttackSpacebase (Spacebase enemySpacebase)
    {
        if(selectedUnit.CanAttack(enemySpacebase.transform.position))
        {
            selectedUnit.AttackSpacebase(enemySpacebase);

            // update the UI
            GameUI.instance.UpdateWaitingUnitsText(spacebases.FindAll(x => x.CanSelectSpacebase()).Count);
        }
    }

    void TryMove (Vector3 movePos)
    {
        if(selectedUnit.CanMove(movePos))
        {
            selectedUnit.Move(movePos);

            // update the UI
            GameUI.instance.UpdateWaitingUnitsText(units.FindAll(x => x.CanSelect()).Count);
        }
    }

    void TryMoveSpacebase (Vector3 movePos)
    {
        if(selectedSpacebase.CanMoveSpacebase(movePos))
        {
            selectedSpacebase.MoveSpacebase(movePos);

            // update the UI
            GameUI.instance.UpdateWaitingUnitsText(spacebases.FindAll(x => x.CanSelectSpacebase()).Count);
        }
    }
/* //NO TURNS CHECKPOINT
    [PunRPC]
    public void EndTurn()
    {
        // de-select the unit
        if(selectedUnit != null)
            DeSelectUnit();

        // start the next turn
        GameManager.instance.photonView.RPC("SetNextTurn", RpcTarget.All);
        //Input.GetKey(KeyCode.Space);
        //GameMaster.EndTurn();
        //GameMaster.instance.photonView.RPC("ChangeGameMasterInstancePlayerTurn", RpcTarget.Others);
    }

    public void BeginTurn ()
    {
        foreach(Unit2 unit in units)
            unit.usedThisTurn = false;
        
        // update the UI
        GameUI.instance.UpdateWaitingUnitsText(units.Count);
    }
*/ //NO TURNS CHECKPOINT
}
