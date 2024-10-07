using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoomManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TextMeshProUGUI _playerCountTextSchool;
    [SerializeField] private TextMeshProUGUI _playerCountTextOutdoor;

    private string _currentMapType;
    private const byte _expectedMaxPlayersProperty = 0;

    #region Unity Methods
    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;

        if (!PhotonNetwork.IsConnectedAndReady)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            PhotonNetwork.JoinLobby();
        }
    }
    #endregion

    #region UI Callback Methods

    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public void OnEnterButtonClicked_Outdoor()
    {
        _currentMapType = MultiplayerVRConstants.MAP_TYPE_VALUE_OUTDOOR;
        CreatePropertiesAndJoinRoom();
    }

    public void OnEnterButtonClicked_School()
    {
        _currentMapType = MultiplayerVRConstants.MAP_TYPE_VALUE_SCHOOL;
        CreatePropertiesAndJoinRoom();
    }

    #endregion

    #region Photon Callback Methods

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log(message);

        CreateAndJoinRoom();
    }

    public override void OnCreatedRoom()
    {
        Debug.Log($"Room with name {PhotonNetwork.CurrentRoom.Name} was created");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log($"The local player {PhotonNetwork.LocalPlayer.NickName} joined room {PhotonNetwork.CurrentRoom.Name} with {PhotonNetwork.CurrentRoom.PlayerCount} players");

        if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey(MultiplayerVRConstants.MAP_TYPE_KEY))
        {
            object mapType;

            if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue(MultiplayerVRConstants.MAP_TYPE_KEY, out mapType))
            {
                Debug.Log($"Joined Room with the map: {mapType}");
            }

            Debug.Log("IsMasterClient " + PhotonNetwork.IsMasterClient);
            if (PhotonNetwork.IsMasterClient)
            {
                switch (mapType)
                {
                    case MultiplayerVRConstants.MAP_TYPE_VALUE_OUTDOOR:
                        PhotonNetwork.LoadLevel(MultiplayerVRConstants.SCENE_NAME_OUTDOOR);
                        break;
                    case MultiplayerVRConstants.MAP_TYPE_VALUE_SCHOOL:
                        PhotonNetwork.LoadLevel(MultiplayerVRConstants.SCENE_NAME_SCHOOL);
                        break;
                    default:
                        Debug.LogError($"Map with name {mapType} was not found!");
                        break;
                }
            }
            else
            {
                Debug.Log("Waiting for the scene to be loaded by the MasterClient.");
            }
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log($"Player {newPlayer.NickName} joined room! Current players count {PhotonNetwork.CurrentRoom.PlayerCount}");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if (roomList.Count == 0)
        {
            // There is no room, so 0 players at every room

            _playerCountTextOutdoor.text = $"{0}/{MultiplayerVRConstants.MAX_PLAYERS}";
            _playerCountTextSchool.text = $"{0}/{MultiplayerVRConstants.MAX_PLAYERS}";
        }

        foreach (RoomInfo room in roomList)
        {
            if (room.Name.Contains(MultiplayerVRConstants.MAP_TYPE_VALUE_OUTDOOR))
            {
                _playerCountTextOutdoor.text = $"{room.PlayerCount}/{MultiplayerVRConstants.MAX_PLAYERS}";
            }
            else if (room.Name.Contains(MultiplayerVRConstants.MAP_TYPE_VALUE_SCHOOL))
            {
                _playerCountTextSchool.text = $"{room.PlayerCount}/{MultiplayerVRConstants.MAX_PLAYERS}";
            }
        }
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined the Lobby.");
    }

    #endregion

    #region Private Methods

    private void CreateAndJoinRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 20;

        string roomName = MultiplayerVRConstants.ROOM_NAME_PREFIX + _currentMapType + UnityEngine.Random.Range(0, 10000);

        string[] roomPropsInLobby = { MultiplayerVRConstants.MAP_TYPE_KEY };
        // There is 2 maps
        // 1. Outdoor
        // 2. School

        ExitGames.Client.Photon.Hashtable customRoomProperties = new ExitGames.Client.Photon.Hashtable() { { MultiplayerVRConstants.MAP_TYPE_KEY, _currentMapType } };


        roomOptions.CustomRoomPropertiesForLobby = roomPropsInLobby;
        roomOptions.CustomRoomProperties = customRoomProperties;

        PhotonNetwork.CreateRoom(roomName, roomOptions);
    }

    private void CreatePropertiesAndJoinRoom()
    {
        ExitGames.Client.Photon.Hashtable roomProperties = new ExitGames.Client.Photon.Hashtable() { { MultiplayerVRConstants.MAP_TYPE_KEY, _currentMapType } };
        PhotonNetwork.JoinRandomRoom(roomProperties, _expectedMaxPlayersProperty);
    }

    #endregion
}
