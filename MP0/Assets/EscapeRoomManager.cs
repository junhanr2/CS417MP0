using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EscapeRoomManager : MonoBehaviour
{
    [Header("Win Condition")]
    public int totalGatesRequired = 3;
    public GameObject doorToOpen;

    [Header("UI (Wrist)")]
    public TMP_Text timerText;     // drag TimerText here
    public TMP_Text resultText;    // drag ResultText here

    [Header("Timer")]
    public float timeLimitSeconds = 300f; // 5 minutes

    private HashSet<int> solvedGateIds = new HashSet<int>();
    private bool won = false;
    private bool lost = false;
    private float timeRemaining;

    private void Start()
    {
        // initialize timer
        timeRemaining = timeLimitSeconds;

        // hide result at start
        if (resultText != null)
            resultText.gameObject.SetActive(false);

        UpdateTimerUI();
    }

    private void Update()
    {
        // stop timer if game ended
        if (won || lost) return;

        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0f)
        {
            timeRemaining = 0f;
            UpdateTimerUI();
            Lose();
            return;
        }

        UpdateTimerUI();
    }

    public void MarkGateSolved(int gateId)
    {
        if (won || lost) return;

        solvedGateIds.Add(gateId);
        Debug.Log($"Solved gates: {solvedGateIds.Count}/{totalGatesRequired}");

        if (solvedGateIds.Count >= totalGatesRequired)
            Win();
    }

    private void Win()
    {
        won = true; // <-- stops timer
        Debug.Log("YOU WIN!");

        if (doorToOpen != null)
            doorToOpen.SetActive(false);

        ShowResult("YOU WIN");
    }

    private void Lose()
    {
        lost = true; // <-- stops timer
        Debug.Log("YOU LOSS!");

        ShowResult("YOU LOSS");
    }

    private void ShowResult(string message)
    {
        if (resultText != null)
        {
            resultText.gameObject.SetActive(true);
            resultText.text = message;
        }
    }

    private void UpdateTimerUI()
    {
        if (timerText == null) return;

        int totalSeconds = Mathf.CeilToInt(timeRemaining);
        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;

        timerText.text = $"{minutes:00}:{seconds:00}";
    }
}
