using UnityEngine;

public class StartGame : MonoBehaviour
{
    public GameObject xrOrigin;
    public Transform escapeRoomSpawnPoint;
    public EscapeRoomManager escapeRoomManager;

    public void TeleportToEscapeRoom()
    {
        xrOrigin.transform.position = escapeRoomSpawnPoint.position;
        xrOrigin.transform.rotation = escapeRoomSpawnPoint.rotation;
        escapeRoomManager.StartTimer();
    }
}
