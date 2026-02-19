using System.Collections.Generic;
using System.Collections;
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
    public TMP_Text scoreText;

    [Header("Timer")]
    public float timeLimitSeconds = 300f; // 5 minutes

    private HashSet<int> solvedGateIds = new HashSet<int>();
    private bool won = false;
    private bool lost = false;
    private float timeRemaining;
    private bool timerRunning = false;
    public GameObject xrOrigin;
    public Transform winSpawnPoint;

    public void StartTimer()
    {
        timerRunning = true;
    }

    private void Start()
    {
        // initialize timer
        timeRemaining = timeLimitSeconds;
        timerRunning = false;
        // hide result at start
        if (resultText != null)
            resultText.gameObject.SetActive(false);

        UpdateTimerUI();
    }

    private void Update()
    {
        // stop timer if game ended
        if (won || lost || !timerRunning) return;

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

        public void MarkGateSolved(int gateId) {
        if (won || lost || !timerRunning) return;

        solvedGateIds.Add(gateId);
        Debug.Log($"Solved gates: {solvedGateIds.Count}/{totalGatesRequired}");
        if (scoreText != null) {
            scoreText.text = $"Gates: {solvedGateIds.Count}/{totalGatesRequired}";
        }
        
        if (solvedGateIds.Count >= totalGatesRequired)
            Win();
    }


    private void Win()
    {
        won = true; // <-- stops timer
        Debug.Log("YOU WIN!");
        if (timerText != null) timerText.gameObject.SetActive(false);
        if (scoreText != null) scoreText.gameObject.SetActive(false);
        if (doorToOpen != null)
            doorToOpen.SetActive(false);

        ShowResult("YOU WIN");
        StartCoroutine(DelayedTeleport(3f));
    }

    private IEnumerator DelayedTeleport(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        xrOrigin.transform.position = winSpawnPoint.position;
    }

    private void Lose()
    {
        lost = true; // <-- stops timer
        if (timerText != null) timerText.gameObject.SetActive(false);
        if (scoreText != null) scoreText.gameObject.SetActive(false);
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
    public void ResetGame()
    {
        solvedGateIds.Clear();
        won = false;
        lost = false;
        timerRunning = false;
        timeRemaining = timeLimitSeconds;
        if (resultText != null)
            resultText.gameObject.SetActive(false);
        if (scoreText != null)
            scoreText.text = "Gates: 0/3";
    }
}
