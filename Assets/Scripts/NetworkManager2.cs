using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager2 : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private byte maxPlayersPerRoom = 2;
    
    // instance
    public static NetworkManager2 instance;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        // connect to the master server
        //PhotonNetwork.ConnectUsingSettings();
    }

    // joins a random room or creates a new room
    public void CreateOrJoinRoom ()
    {
        // if there are available rooms, join a random one
        if(PhotonNetwork.CountOfRooms > 0)
            PhotonNetwork.JoinRandomRoom();
        // otherwise, create a new room
        else
        {
            PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom});
        }
    }

    // changes the scene using Photon's system
    [PunRPC]
    public void ChangeScene (int level)
    {
        PhotonNetwork.LoadLevel("SampleScene " + level);
    }
}
