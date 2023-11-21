//4
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
    public PlayerController leftPlayer;
    public PlayerController rightPlayer;

    public PlayerController curPlayer;

    public float postGameTime;


    // instance
    public static GameManager instance;

    Leaderboard leaderboard;

    void Awake ()
    {
        instance = this;
    }

    void LoadRandomLevel()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            Debug.LogError("PhotonNetwork is Trying to Load a Level but WE ARE NOT the Master Client.");
        }
        int randomLevel = UnityEngine.Random.Range(2, 5);
        Debug.LogFormat("PhotonNetwork : Loading Level : {0}", randomLevel);
        PhotonNetwork.LoadLevel("Sample Scene " + randomLevel);
    }

    void Start ()
    {
        // the master client will set the players
        if(PhotonNetwork.IsMasterClient)
            {
                SetPlayers();
            }
        leaderboard = FindObjectOfType<Leaderboard>();
    }

    

    void SetPlayers ()
    {
        // set the owners of the two player's photon views
        leftPlayer.photonView.TransferOwnership(1);
        rightPlayer.photonView.TransferOwnership(2);

        // initialize the players
        leftPlayer.photonView.RPC("Initialize", RpcTarget.AllBuffered, PhotonNetwork.CurrentRoom.GetPlayer(1), 1);
        rightPlayer.photonView.RPC("Initialize", RpcTarget.AllBuffered, PhotonNetwork.CurrentRoom.GetPlayer(2), 2);

        //photonView.RPC("SetNextTurn", RpcTarget.AllBuffered); NO TURNS CHECKPOINT
    }
/* //NO TURNS CHECKPOINT
    [PunRPC]
    void SetNextTurn ()
    {
        if(curPlayer == null)
            curPlayer = leftPlayer;
        else
            curPlayer = curPlayer == leftPlayer ? rightPlayer : leftPlayer;
            
        if(curPlayer == PlayerController.me)
            PlayerController.me.BeginTurn();

        // toggle the end turn button
        GameUI.instance.ToggleEndTurnButton(curPlayer == PlayerController.me);
    }
*/ //NO TURNS CHECKPOINT
    public PlayerController GetOtherPlayer (PlayerController player)
    {
        return player == leftPlayer ? rightPlayer : leftPlayer;
    }

    public void CheckWinCondition ()
    {
        if(PlayerController.me.units.Count == 0)
        {
            photonView.RPC("WinGame", RpcTarget.All, PlayerController.enemy == leftPlayer ? 1 : 0);
        }
        if(PlayerController.enemy.units.Count == 0)
        {
            photonView.RPC("WinGame", RpcTarget.All, PlayerController.enemy == leftPlayer ? 0 : 1);
        }

        if(PlayerController.me.spacebases.Count == 0)
        {
            photonView.RPC("WinGame", RpcTarget.All, PlayerController.enemy == leftPlayer ? 1 : 0);
        }
        if(PlayerController.enemy.spacebases.Count == 0)
        {
            photonView.RPC("WinGame", RpcTarget.All, PlayerController.enemy == leftPlayer ? 0 : 1);
        }
    }

    [PunRPC]
    void WinGame (int winner)
    {
        //PlayerController player = winner == 0 ? leftPlayer : rightPlayer; asi estaba y creo que esta alrevez
        PlayerController player = winner == 0 ? rightPlayer : leftPlayer;

        GameUI.instance.SetWinText(player.photonPlayer.NickName);

        //Timer.instance.leftTimerIsRunning = false;
        //Timer.instance.rightTimerIsRunning = false;
        Invoke("SendAndGetLeaderboard", postGameTime);
        //Leaderboard.instance.SendLeaderboard(0);
        //Leaderboard.instance.GetLeaderboard();

        Invoke("GoBackToMenu", postGameTime);
    }

    void GoBackToMenu ()
    {
        PhotonNetwork.LeaveRoom();
        //NetworkManager.instance.ChangeScene(0);
        //cursor.visible = true;
        Cursor.visible = true;
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(1);
    }

    public void LeaveRoom ()
    {
        //unit.victoryPanel.SetActive(true); 
        photonView.RPC("WinGame", RpcTarget.All, PlayerController.enemy == leftPlayer ? 1 : 0);
        //PhotonNetwork.LeaveRoom();
    }
/*
    void LoadLevelBasedOnNumberOfPlayers()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            Deub.LogError("PhotonNetwork is Trying to Load a Level but WE ARE NOT the Master Client.")
        }
        Debug.LogFormat("PhotonNetwork : Loading Level : {0}", PhotonNetwork.CurrentRoom.PlayerCount);
        PhotonNetwork.LoadLevel("Sample Scene " + PhotonNetwork.CurrentRoom.PlayerCount);
    }
*/
    void SendAndGetLeaderboard()
    {
        Leaderboard.leaderboard.SendLeaderboard(0);
        Debug.Log("Send Leaderboard.");
        Leaderboard.leaderboard.GetLeaderboard();
        Debug.Log("Get Leaderboard.");
    }
}
