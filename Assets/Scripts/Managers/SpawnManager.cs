using Photon.Pun;
using UnityEngine;

public class SpawnManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject _genericVRPlayerPrefab;
    [SerializeField] private Vector3 _spawnPosition;

    private bool _wasSpawned = false;

    void Start()
    {
        Debug.Log($"[Client] Start called on client: {PhotonNetwork.LocalPlayer.NickName}");

        TrySpawnPlayer();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log($"[Client] {PhotonNetwork.LocalPlayer.NickName} successfully joined room: {PhotonNetwork.CurrentRoom.Name} with {PhotonNetwork.CurrentRoom.PlayerCount} players");

        TrySpawnPlayer();
    }

    private void TrySpawnPlayer()
    {
        Debug.Log($"[Client] Trying spawn player: {PhotonNetwork.LocalPlayer.NickName}");
        if (PhotonNetwork.IsConnectedAndReady && !_wasSpawned)
        {
            Debug.Log($"[Client] Instantiating player object on client: {PhotonNetwork.LocalPlayer.NickName}");
            PhotonNetwork.Instantiate(_genericVRPlayerPrefab.name, _spawnPosition, Quaternion.identity);
            _wasSpawned = true;
        } else
        {
            Debug.LogWarning($"[Client] Not connected or ready on client: {PhotonNetwork.LocalPlayer.NickName}");
        }
    }
}
