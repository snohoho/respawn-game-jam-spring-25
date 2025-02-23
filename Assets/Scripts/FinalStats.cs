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
        "Uptime: " + PersistentData.Time;
    }

    public void RestartGame() {
        SceneManager.LoadScene("MainMenu");
    }
}
