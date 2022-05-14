using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class HandsController : MonoBehaviour
{
    [SerializeField] private InputActionReference teleportInput;

    public UnityEvent OnTeleportActivate;
    public UnityEvent OnTeleportCancel;

    private void OnEnable()
    {
        teleportInput.action.performed += ActivateTeleport;
        teleportInput.action.canceled += DeactivateTeleport;
    }

    private void OnDisable()
    {
        teleportInput.action.performed -= ActivateTeleport;
        teleportInput.action.canceled -= DeactivateTeleport;
    }
    
    private void ActivateTeleport(InputAction.CallbackContext obj)
    {
        OnTeleportActivate?.Invoke();
    }
    
    private void DeactivateTeleport(InputAction.CallbackContext obj)
    {
        Invoke("TurnOffTeleport", 0f);
    }
    
    private void TurnOffTeleport()
    {
        OnTeleportCancel?.Invoke();
    }
}
