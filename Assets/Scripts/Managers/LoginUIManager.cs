using Photon.Pun;
using UnityEngine;

public class LoginUIManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject connectionOptionsPanel;
    [SerializeField] private GameObject connectionWithNamePanel;

    #region Unity Methods
    void Start()
    {
        connectionOptionsPanel.SetActive(true);
        connectionWithNamePanel.SetActive(false);
    }
    #endregion

}
