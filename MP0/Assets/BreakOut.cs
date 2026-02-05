using UnityEngine;
using UnityEngine.InputSystem;

public class BreakOut : MonoBehaviour
{
    [Header("Input (New Input System)")]
    [SerializeField] private InputActionReference toggleAction;

    [Header("What to move (your XR rig root)")]
    [SerializeField] private Transform playerRig;

    [Header("Where to move")]
    [SerializeField] private Transform roomPoint;
    [SerializeField] private Transform outsidePoint;

    private bool isOutside = false;

    private void OnEnable()
    {
        if (toggleAction != null)
        {
            toggleAction.action.Enable();
            toggleAction.action.performed += OnToggle;
        }
    }

    private void OnDisable()
    {
        if (toggleAction != null)
            toggleAction.action.performed -= OnToggle;
    }

    private void OnToggle(InputAction.CallbackContext ctx)
    {
        if (playerRig == null || roomPoint == null || outsidePoint == null) return;

        isOutside = !isOutside;
        Transform target = isOutside ? outsidePoint : roomPoint;

        // Move + rotate the rig to match the target point
        playerRig.SetPositionAndRotation(target.position, target.rotation);
    }
}
