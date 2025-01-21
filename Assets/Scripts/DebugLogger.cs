using UnityEngine;
using TMPro;

public class DebugLogger : MonoBehaviour
{
    [Header("UI Components")]
    [Tooltip("Assign a TextMeshPro Text component to display logs.")]
    public TextMeshProUGUI logText;

    [Header("Log Settings")]
    [Tooltip("Maximum number of log lines to keep displayed.")]
    public int maxLines = 50;

    private static DebugLogger instance;
    private string currentLog = "";

    void Awake()
    {
        // Ensure singleton instance
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        if (logText == null)
        {
            Debug.LogError("DebugLogger: Please assign a TextMeshProUGUI component in the Inspector.");
        }
    }

    /// <summary>
    /// Static method to log a message to the UI.
    /// </summary>
    /// <param name="message">Message to log.</param>
    public static void Log(string message)
    {
        if (instance == null)
        {
            Debug.LogError("DebugLogger: No instance found. Ensure the DebugLogger script is in the scene.");
            return;
        }
        instance.AppendLog(message);
    }

    /// <summary>
    /// Appends a message to the log and updates the UI.
    /// </summary>
    /// <param name="message">Message to append.</param>
    private void AppendLog(string message)
    {
        currentLog += $"{message}\n";

        // Limit log lines to maxLines
        string[] lines = currentLog.Split('\n');
        if (lines.Length > maxLines)
        {
            currentLog = string.Join("\n", lines, lines.Length - maxLines, maxLines);
        }

        // Update TextMeshPro text
        if (logText != null)
        {
            logText.text = currentLog;
        }
    }
}
