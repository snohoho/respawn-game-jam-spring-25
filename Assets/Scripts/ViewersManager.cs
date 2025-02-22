using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ViewersManager : MonoBehaviour
{
    //[SerializeField] MainMicrogame mainMicrogame;
    [SerializeField] CodingMicrogame codingMicrogame;
    [SerializeField] DiscordMicrogame discordMicrogame;
    //[SerializeField] DonoMicrogame donoMicrogame;
    [SerializeField] ChatMicrogame chatMicrogame;

    private int viewers;
    [SerializeField] TextMeshProUGUI viewerCountTxt;
    private float viewerLossTimer;
    private float multIncreaseTimer;
    private float viewerLossMult;
    [SerializeField] TextMeshProUGUI uptimeTxt;

    void Start()
    {
        viewers = Random.Range(990,1011);
        viewerLossMult = Random.Range(0.95f, 1.05f);
    }

    void Update()
    {
        viewerCountTxt.text = "Viewers: " + viewers.ToString();

        int time = (int)Time.timeSinceLevelLoad;
        int ss = time%60;
        int mm = time/60%60;
        string timeTxt = string.Format("{0:00}:{1:00}",mm,ss);

        uptimeTxt.text = "Uptime: " + timeTxt;
    }

    void FixedUpdate() {
        viewerLossTimer += Time.deltaTime;
        multIncreaseTimer += Time.deltaTime;

        //every 15s increase the multiplier for lost viewers
        if(multIncreaseTimer >= 15) {
            viewerLossMult += Random.Range(0.15f,0.25f);
            multIncreaseTimer = 0f;
        }

        //lose viewers every second
        if(viewerLossTimer > 1f) {
            viewers -= (int)Mathf.Round(10.0f * viewerLossMult);
            viewerLossTimer = 0f;
        }

        if(chatMicrogame.chatClicked == true) {
            chatMicrogame.chatClicked = false;
            viewers += 100;
        }
    }
}
