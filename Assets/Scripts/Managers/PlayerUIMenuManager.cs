using UnityEngine;
using UnityEngine.UI;

public class PlayerUIMenuManager : MonoBehaviour
{
    [SerializeField] private Button _goHomeButton;

    void Start()
    {
        _goHomeButton.onClick.AddListener(VirtualWorldManager.Instance.LeaveRoomAndGoHome);
    }
}
