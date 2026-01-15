using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_Text StatusText;
    [SerializeField] private GameData playerData;

    private void Awake()
    {
        PhotonNetwork.ConnectUsingSettings(); // 서버 접속
    }

    private void Update()
    {
        StatusText.text = PhotonNetwork.NetworkClientState.ToString(); // 서버 상태 표시
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.LocalPlayer.NickName = playerData.PlayerName;
        Debug.Log("서버 접속 완료" + PhotonNetwork.LocalPlayer.NickName);
    }
}
