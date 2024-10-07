using UnityEngine;
using UnityEngine.InputSystem;

public class UIInteractionController : MonoBehaviour
{
    [SerializeField] private GameObject _UIController;
    [SerializeField] private GameObject _baseController;
    [SerializeField] private InputActionReference _UISwitchAction;
    [SerializeField] private GameObject _UIGameObject;

    private void OnEnable()
    {
        _UISwitchAction.action.performed += ActivateUI;
    }

    private void OnDisable()
    {
        _UISwitchAction.action.performed -= ActivateUI;
    }

    private void Start()
    {
        if (_UIGameObject != null)
        {
            _UIGameObject.SetActive(false);
        }

        _UIController.SetActive(false);
    }

    private void ActivateUI(InputAction.CallbackContext ctx)
    {
        if (ctx.action.WasPressedThisFrame() && _UIGameObject != null)
        {
            ChangeUIState(!_UIGameObject.activeSelf);
        }
    }

    public void ChangeUIState(bool isActive)
    {
        _UIGameObject.SetActive(isActive);
        _UIController.SetActive(isActive);
    }
}
