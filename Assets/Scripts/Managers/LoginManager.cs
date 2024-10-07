using Photon.Pun;
using TMPro;
using UnityEngine;

public class LoginManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField _playerNameInput;

    #region UI Callback Methods

    public void ConnectAnonymously()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public void ConnectWithName()
    {
        if (_playerNameInput != null)
        {
            PhotonNetwork.NickName = _playerNameInput.text;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    #endregion

    #region Photon Callback Methods

    public override void OnConnected()
    {
        Debug.Log("Connected to Internet!");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master Server with name: " + PhotonNetwork.NickName);
        PhotonNetwork.LoadLevel(MultiplayerVRConstants.SCENE_NAME_HOME);
    }

    #endregion
}
