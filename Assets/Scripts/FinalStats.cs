using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalStats : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI finalStatsText;
    void Start()
    {
        finalStatsText.text =
        "Peak Viewership: " + PersistentData.PeakViewers + "\n" +
        "Uptime: " + PersistentData.Time + "\n" +
        "Chat Responses: " + PersistentData.ChatResponded + "\n" +
        "Donation Responses: " + PersistentData.DonoResponded + "\n" +
        "Ads Popped: " + PersistentData.AdsClosed + "\n" +
        "Counterhacks: " + PersistentData.CounterhackSuccess + "\n" +
        "Discord Responses: " + PersistentData.DiscordMessagesResponded;
    }

    public void RestartGame() {
        SceneManager.LoadScene("MainMenu");
    }
}
