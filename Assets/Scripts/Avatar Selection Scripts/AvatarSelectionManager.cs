using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarSelectionManager : MonoBehaviour
{
    [SerializeField] private GameObject AvatarSelectionPlatformGameobject;

    [SerializeField] private GameObject[] selectableAvatarModels;
    [SerializeField] private GameObject[] loadableAvatarModels;

    [SerializeField] private int avatarSelectionNumber = 0;

    [SerializeField] private AvatarInputConverter avatarInputConverter;


    /// <summary>
    /// Singleton Implementation
    /// </summary>
    public static AvatarSelectionManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        object storedAvatarSelectionNumber;
        if (PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue(MultiplayerVRConstants.AVATAR_SELECTION_NUMBER_KEY, out storedAvatarSelectionNumber))
        {
            Debug.Log("Stored avatar selection number: " + (int)storedAvatarSelectionNumber);
            avatarSelectionNumber = (int)storedAvatarSelectionNumber;
        }
        else
        {
            avatarSelectionNumber = 0;
        }

        ActivateAvatarModelAt(avatarSelectionNumber);
        LoadAvatarModelAt(avatarSelectionNumber);
    }

    public void ActivateAvatarSelectionPlatform()
    {
        AvatarSelectionPlatformGameobject.SetActive(true);
    }

    public void DeactivateAvatarSelectionPlatform()
    {
        AvatarSelectionPlatformGameobject.SetActive(false);

    }

    public void NextAvatar()
    {
        avatarSelectionNumber += 1;
        if (avatarSelectionNumber >= selectableAvatarModels.Length)
        {
            avatarSelectionNumber = 0;
        }
        ActivateAvatarModelAt(avatarSelectionNumber);

    }

    public void PreviousAvatar()
    {
        avatarSelectionNumber -= 1;

        if (avatarSelectionNumber < 0)
        {
            avatarSelectionNumber = selectableAvatarModels.Length - 1;
        }
        ActivateAvatarModelAt(avatarSelectionNumber);

    }

    /// <summary>
    /// Activates the selected Avatar model inside the Avatar Selection Platform
    /// </summary>
    /// <param name="avatarIndex"></param>
    private void ActivateAvatarModelAt(int avatarIndex)
    {
        foreach (GameObject selectableAvatarModel in selectableAvatarModels)
        {
            selectableAvatarModel.SetActive(false);
        }

        selectableAvatarModels[avatarIndex].SetActive(true);
        Debug.Log(avatarSelectionNumber);

        LoadAvatarModelAt(avatarSelectionNumber);
    }

    /// <summary>
    /// Loads the Avatar Model and integrates into the VR Player Controller gameobject
    /// </summary>
    /// <param name="avatarIndex"></param>
    private void LoadAvatarModelAt(int avatarIndex)
    {
        foreach (GameObject loadableAvatarModel in loadableAvatarModels)
        {
            loadableAvatarModel.SetActive(false);
        }

        loadableAvatarModels[avatarIndex].SetActive(true);

        AvatarHolder selectedAvatarHolder = loadableAvatarModels[avatarIndex].GetComponent<AvatarHolder>();

        avatarInputConverter.mainAvatarTransform = selectedAvatarHolder.mainAvatarTransform;

        avatarInputConverter.avatarBody = selectedAvatarHolder.bodyTransform;
        avatarInputConverter.avatarHead = selectedAvatarHolder.headTransform;
        avatarInputConverter.avatarHandL = selectedAvatarHolder.handLeftTransform;
        avatarInputConverter.avatarHandR = selectedAvatarHolder.handRightTransform;

        ExitGames.Client.Photon.Hashtable playerSelectionProp = new ExitGames.Client.Photon.Hashtable() { { MultiplayerVRConstants.AVATAR_SELECTION_NUMBER_KEY, avatarSelectionNumber } };
        PhotonNetwork.LocalPlayer.SetCustomProperties(playerSelectionProp);
    }
}
