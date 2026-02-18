using System.Collections.Generic;
using UnityEngine;

public class EscapeRoomManager : MonoBehaviour
{
    public int totalGatesRequired = 3;
    public GameObject doorToOpen;

    private HashSet<int> solvedGateIds = new HashSet<int>();
    private bool won = false;

    public void MarkGateSolved(int gateId)
    {
        if (won) return;

        solvedGateIds.Add(gateId);
        Debug.Log($"Solved gates: {solvedGateIds.Count}/{totalGatesRequired}");

        if (solvedGateIds.Count >= totalGatesRequired)
            Win();
    }

    private void Win()
    {
        Debug.Log("YOU WIN!");

        if (doorToOpen != null)
            doorToOpen.SetActive(false);


    }
}
