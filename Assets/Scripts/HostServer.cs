using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HostServer : MonoBehaviourPunCallbacks
{
    public GameObject serverConsole;
    public GameObject playerTemplate;

    private string gameVersion = "1";
    private string roomName = "NewRoom";

    private Text consoleOutput;
    private string outTxt;
    Dictionary<int, GameObject> playerObjList = new Dictionary<int, GameObject>();
    
    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    void Start()
    {
        consoleOutput = serverConsole.GetComponent<Text>();
        playerObjList.Clear();
        Connect();
    }

    public void Connect()
    {
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        // Server module will not joining any existing room
        PhotonNetwork.NickName = "ServerNode";
        PhotonNetwork.CreateRoom(roomName, new RoomOptions());
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Attemp to create room again..");
        PhotonNetwork.CreateRoom(roomName, new RoomOptions());
    }

    public override void OnJoinedRoom()
    {
    }

    public override void OnPlayerEnteredRoom(Player otherPlayer )
    {
        Debug.Log("Player [" + otherPlayer.ActorNumber + "] entered the room");
        GameObject playerObj = PhotonNetwork.Instantiate(otherPlayer.NickName, Vector3.up * 5, Quaternion.identity, 0);
        playerObjList.Add(otherPlayer.ActorNumber, playerObj);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("Player [" + otherPlayer.NickName + "] left the room.");
        GameObject playerObj = playerObjList[otherPlayer.ActorNumber];
        playerObjList.Remove(otherPlayer.ActorNumber);
        Destroy(playerObj);
    }

    void Update()
    {
        if(consoleOutput!=null)
        {
            outTxt = "Server Console\n";
            if (PhotonNetwork.IsConnectedAndReady && PhotonNetwork.CurrentRoom.IsOpen)
            {
                outTxt += "[ RoomID ] : " + PhotonNetwork.CurrentRoom.Name + "\n";
                foreach (Player player in PhotonNetwork.PlayerList)
                {
                    outTxt += "[ " + player.NickName + " ] \n";
                }
            }

            consoleOutput.text = outTxt;
        }
    }
}
