using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class UIHandler : MonoBehaviourPunCallbacks
{
    public InputField createRoomTF;
    public InputField joinRoomTF;

    PhotonView photonView;

    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }
    public void OnClick_JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinRoomTF.text, null);
    }

    public void OnClick_CreateRoom()
    {
        PhotonNetwork.CreateRoom(createRoomTF.text, new RoomOptions { MaxPlayers = 2 }, null);
    }

    public override void OnJoinedRoom()
    {
        print("Room joined Success");
        PhotonNetwork.LoadLevel(1);

    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        print("RoomFailed" + returnCode + "Message" + message);
    }
}
