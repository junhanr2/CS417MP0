using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor))]
public class GateSocket : MonoBehaviour
{
    public int gateId = 1;
    public int requiredKeyId = 1;
    public EscapeRoomManager manager;

    private UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor socket;
    private bool solved = false;

    void Awake()
    {
        socket = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor>();
    }

    void OnEnable()
    {
        socket.selectEntered.AddListener(OnSelectEntered);
    }

    void OnDisable()
    {
        socket.selectEntered.RemoveListener(OnSelectEntered);
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (solved) return;

        var key = args.interactableObject.transform.GetComponent<KeyId>();
        if (key == null || key.keyId != requiredKeyId)
        {
            // wrong object -> eject
            socket.interactionManager.SelectExit(socket, args.interactableObject);
            return;
        }

        solved = true;

        // lock it in place by disabling grab (optional)
        var grab = args.interactableObject.transform.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        if (grab != null) grab.enabled = false;

        // Notify manager
        if (manager != null)
            manager.MarkGateSolved(gateId);

        // Disappear after 0.3 seconds
        GameObject keyObj = args.interactableObject.transform.gameObject;
        StartCoroutine(DisappearAfter(keyObj, 0.3f));
    }

    private IEnumerator DisappearAfter(GameObject obj, float seconds)
    {
        yield return new WaitForSeconds(seconds);

        if (obj != null)
        {
            // Make sure it's not still "selected" by the socket when we hide it
            var interactable = obj.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.IXRSelectInteractable>();
            if (interactable != null && socket != null && socket.interactionManager != null)
            {
                socket.interactionManager.SelectExit(socket, interactable);
            }

            obj.SetActive(false); // disappears
        }
    }
}
