using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ViewersManager : MonoBehaviour
{
    [SerializeField] private MainMicrogame mainMicrogame;
    [SerializeField] private CodingMicrogame codingMicrogame;
    [SerializeField] private DiscordMicrogame discordMicrogame;
    [SerializeField] private DonoMicrogame donoMicrogame;
    [SerializeField] private ChatMicrogame chatMicrogame;

    public int viewers;
    [SerializeField] private TextMeshProUGUI viewerCountTxt;
    private float viewerLossTimer;
    private float multIncreaseTimer;
    private float viewerLossMult;
    [SerializeField] private TextMeshProUGUI uptimeTxt;

    void Start()
    {
        viewers = Random.Range(990,1011);
        viewerLossMult = Random.Range(0.95f, 1.05f);
    }

    void Update()
    {
        if(viewers <= 0) {
            viewerCountTxt.text = "Viewers: 0";
            return;
        }
        viewerCountTxt.text = "Viewers: " + viewers.ToString();

        int time = (int)Time.timeSinceLevelLoad;
        int ss = time%60;
        int mm = time/60%60;
        string timeTxt = string.Format("{0:00}:{1:00}",mm,ss);
        PersistentData.Time = timeTxt;

        uptimeTxt.text = "Uptime: " + timeTxt;
    }

    void FixedUpdate() {
        if(viewers <= 0) {
            StartCoroutine(GameOver());
            return;
        }

        if(PersistentData.PeakViewers < viewers) {
            PersistentData.PeakViewers = viewers;
        }

        viewerLossTimer += Time.deltaTime;
        multIncreaseTimer += Time.deltaTime;

        //lose viewers every second
        //every 15s increase the multiplier for lost viewers
        if(multIncreaseTimer >= 15) {
            viewerLossMult += Random.Range(0.15f,0.25f);
            multIncreaseTimer = 0f;
        }
        if(viewerLossTimer > 1f) {
            viewers -= (int)Mathf.Round(10.0f * viewerLossMult);
            viewerLossTimer = 0f;
        }

        //chat microgame
        if(chatMicrogame.chatClicked == true) {
            PersistentData.ChatResponded++;
            chatMicrogame.chatClicked = false;
            viewers += (int)Mathf.Round(Random.Range(90,111));
        }

        //main microgame
        if(mainMicrogame.successFlag == "success") {
            viewers += (int)Mathf.Round(Random.Range(150,211));
        }
        else if(mainMicrogame.successFlag == "fail") {
            viewers -= (int)Mathf.Round(Random.Range(50,101) * viewerLossMult);
        }
        mainMicrogame.successFlag = null;

        //discord microgame
        if(discordMicrogame.successFlag == "success") {
            PersistentData.DiscordMessagesResponded++;
            viewers += (int)Mathf.Round(Random.Range(150,211));
        }
        else if(discordMicrogame.successFlag == "fail") {
            PersistentData.DiscordMessagesResponded++;
            viewers -= (int)Mathf.Round(Random.Range(50,101) * viewerLossMult);
        }
        else if(discordMicrogame.successFlag == "worse") {
            PersistentData.DiscordMessagesResponded++;
            viewers -= (int)Mathf.Round(Random.Range(150,211) * viewerLossMult);
        }
        discordMicrogame.successFlag = null;

        //coding microgame
        if(codingMicrogame.successFlag == "success") {
            PersistentData.CounterhackSuccess++;
            viewers += (int)Mathf.Round(Random.Range(50,76));
        }
        else if(codingMicrogame.successFlag == "fail") {
            viewers -= (int)Mathf.Round(Random.Range(50,76) * viewerLossMult);
        }
        else if(codingMicrogame.successFlag == "worse") {
            viewers -= (int)Mathf.Round(Random.Range(90,121) * viewerLossMult);
        }
        codingMicrogame.successFlag = null;
        
        //dono microgame
        if(donoMicrogame.successFlag == "success") {
            PersistentData.DonoResponded++;
            donoMicrogame.setName = false;
            viewers += (int)Mathf.Round(Random.Range(150,211));
        }
        else if(donoMicrogame.successFlag == "fail") {
            donoMicrogame.setName = false;
            viewers -= (int)Mathf.Round(Random.Range(150,211) * viewerLossMult);
        }
        donoMicrogame.successFlag = null;
    }

    IEnumerator GameOver() {
        Time.timeScale = 3f;
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("GameOver");
    }
}
