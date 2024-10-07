using Photon.Pun;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerNetworkSetup : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject XRRigGO;
    [SerializeField] private int _localHeadLayerIndex;
    [SerializeField] private int _localBodyLayerIndex;

    [SerializeField] private GameObject _avatarHeadGO;
    [SerializeField] private GameObject _avatarBodyGO;

    [SerializeField] private GameObject[] _avatarModelPrefabs;

    [SerializeField] private TextMeshProUGUI _playerNameText;

    private int defaultLayerNumber = 0;

    private void Start()
    { 
        if (photonView.IsMine)
        {
            XRRigGO.SetActive(true);

            object avatarSelectionNumber;
            if (PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue(MultiplayerVRConstants.AVATAR_SELECTION_NUMBER_KEY, out avatarSelectionNumber))
            {
                Debug.Log("Avatar selection number: " + (int)avatarSelectionNumber);
                photonView.RPC("InitializeSelectedAvatarModel", RpcTarget.AllBuffered, (int)avatarSelectionNumber);
            }


            _avatarHeadGO.SetLayerRecursively(_localHeadLayerIndex);
            _avatarBodyGO.SetLayerRecursively(_localBodyLayerIndex);

            TeleportationArea[] teleportationAreas = GameObject.FindObjectsOfType<TeleportationArea>();

            if (teleportationAreas.Length > 0)
            {
                foreach (TeleportationArea area in teleportationAreas)
                {
                    area.teleportationProvider = XRRigGO.GetComponent<TeleportationProvider>();
                }
            }

        } else
        {
            XRRigGO.SetActive(false);

            _avatarHeadGO.SetLayerRecursively(defaultLayerNumber);
            _avatarBodyGO.SetLayerRecursively(defaultLayerNumber);
        }

        if (_playerNameText != null)
        {
            _playerNameText.text = photonView.Owner.NickName;
        }
    }

    [PunRPC]
    public void InitializeSelectedAvatarModel(int avatarSelectionNumber)
    {
        GameObject createdAvatar = Instantiate(_avatarModelPrefabs[avatarSelectionNumber], XRRigGO.transform);

        AvatarInputConverter avatarInputConverter = XRRigGO.GetComponent<AvatarInputConverter>();
        AvatarHolder avatarHolder = createdAvatar.GetComponent<AvatarHolder>();

        SetUpAvatarObject(avatarHolder.headTransform, avatarInputConverter.avatarHead);
        SetUpAvatarObject(avatarHolder.bodyTransform, avatarInputConverter.avatarBody);
        SetUpAvatarObject(avatarHolder.handLeftTransform, avatarInputConverter.avatarHandL);
        SetUpAvatarObject(avatarHolder.handRightTransform, avatarInputConverter.avatarHandR);
    }

    private void SetUpAvatarObject(Transform avatarObjectTransform, Transform parentTransform)
    {
        avatarObjectTransform.parent = parentTransform;
        avatarObjectTransform.localPosition = Vector3.zero;
        avatarObjectTransform.localRotation = Quaternion.identity;
    }
}
