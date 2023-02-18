using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class createJoinRooms : MonoBehaviourPunCallbacks
{
    public TMP_InputField createInput;
    public TMP_InputField joinInput;

    public void CreateROom() {
        PhotonNetwork.CreateRoom(createInput.text);
    
    }

    public void JoinROom()
    {
        PhotonNetwork.JoinRoom(createInput.text);

    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Demo");
    }
}
