using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class Menu2 : MonoBehaviourPunCallbacks
{
    public AudioSource music;
    [Header("Screens")]
    public GameObject loginScreen;
    public GameObject mainScreen;
    public GameObject lobbyScreen;

    [Header("Main Screen")]
    public Button playButton;

    [Header("Lobby Screen")]
    public TextMeshProUGUI player1NameText;
    public TextMeshProUGUI player2NameText;
    public TextMeshProUGUI gameStartingText;

    [Header("Levels")]
    [SerializeField]
    int levels = 0;

    // instance
    public static Menu2 instance;

    void Awake ()
    {
        instance = this;
        music.mute = PlayerPrefs.GetInt("isMusicMuted", 0)==1;
    }

    void Start()
    {
        // disable the play button before we connect to the master server
        playButton.interactable = false;
        gameStartingText.gameObject.SetActive(false);
    }

    void Update(){
        // toggle music mute
        if(Input.GetKeyDown(KeyCode.M)){
            music.mute = !music.mute;
            PlayerPrefs.SetInt("isMusicMuted", music.mute?1:0);
        }
    }

    public override void OnConnectedToMaster()
    {
        playButton.interactable = true;
    }

    // toggles the currently visible screen
    public void SetScreen (GameObject screen)
    {
        // disable all screens
        loginScreen.SetActive(false);
        mainScreen.SetActive(false);
        lobbyScreen.SetActive(false);

        // enable the requested screen
        screen.SetActive(true);
    }

    // updates the player's nickname
    public void OnUpdatePlayerNameInput (TMP_InputField nameInput)
    {
        PhotonNetwork.NickName = nameInput.text;
    }

    public void OnPlayButton ()
    {
        NetworkManager.instance.CreateOrJoinRoom();
    }

    // called when we login
    public void OnLogin()
    {
        SetScreen(mainScreen);
    }

    // called when we join a room
    public override void OnJoinedRoom()
    {
        SetScreen(lobbyScreen);
        photonView.RPC("UpdateLobbyUI", RpcTarget.All);
    }

    public override void OnPlayerLeftRoom (Player otherPlayer)
    {
        UpdateLobbyUI();
    }

    // updates the lobby screen UI
    [PunRPC]
    void UpdateLobbyUI()
    {
        // set the player name texts
        player1NameText.text = PhotonNetwork.CurrentRoom.GetPlayer(1).NickName;
        player2NameText.text = PhotonNetwork.PlayerList.Length == 2 ? PhotonNetwork.CurrentRoom.GetPlayer(2).NickName : "...";

        // set the game starting text
        if(PhotonNetwork.PlayerList.Length == 2)
        {
            gameStartingText.gameObject.SetActive(true);
        
            if(PhotonNetwork.IsMasterClient)
                Invoke("TryStartGame", 3.0f);
        }
    }

    void TryStartGame ()
    {
        if(PhotonNetwork.PlayerList.Length == 2)
            NetworkManager2.instance.photonView.RPC("ChangeScene", RpcTarget.All, UnityEngine.Random.Range(2, levels + 6)); // CAUTION: +2 is because we need to skip the first 2 Scenes because of Scene 0 being Main Screen & Scene 1 being Second Main Screen.
        else
            gameStartingText.gameObject.SetActive(false);
    }

    public void OnLeaveButton ()
    {
        PhotonNetwork.LeaveRoom();
        SetScreen(mainScreen);
    }
}
