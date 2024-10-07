using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class VirtualWorldManager : MonoBehaviourPunCallbacks
{
    public static VirtualWorldManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return; 
        } else
        {
            Instance = this;
        }
    }

    public void LeaveRoomAndGoHome()
    {
        PhotonNetwork.LeaveRoom();
    }


    #region Photon Callback Methods

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log($"Player {newPlayer.NickName} joined room! Current players count {PhotonNetwork.CurrentRoom.PlayerCount}");
    }

    public override void OnLeftRoom()
    {
        Debug.Log("Local player left the room.");
        PhotonNetwork.Disconnect();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Local player disconnected: " + cause.ToString());
        PhotonNetwork.LoadLevel(MultiplayerVRConstants.SCENE_NAME_HOME);
    }

    #endregion

}
