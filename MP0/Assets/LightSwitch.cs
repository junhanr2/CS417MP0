using UnityEngine;
using UnityEngine.InputSystem;

public class LightSwitch : MonoBehaviour
{
    public InputActionReference action;   // assign RightHand/SecondaryButton here
    private Light _light;
    private int _state = 0; // 0=red, 1=green, 2=blue

    void Start()
    {
        _light = GetComponent<Light>();

        action.action.Enable();
        action.action.performed += OnPressed;
    }

    private void OnDestroy()
    {
        if (action != null)
            action.action.performed -= OnPressed;
    }

    private void OnPressed(InputAction.CallbackContext ctx)
    {
        _state = (_state + 1) % 3;

        if (_state == 0) _light.color = new Color(1f, 0f, 0f); // red
        else if (_state == 1) _light.color = new Color(0f, 1f, 0f); // green
        else _light.color = new Color(0f, 0f, 1f); // blue
    }
}
