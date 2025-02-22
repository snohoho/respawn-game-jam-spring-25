using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ViewersManager : MonoBehaviour
{
    [SerializeField] private MainMicrogame mainMicrogame;
    [SerializeField] private CodingMicrogame codingMicrogame;
    [SerializeField] private DiscordMicrogame discordMicrogame;
    [SerializeField] private DonoMicrogame donoMicrogame;
    [SerializeField] private ChatMicrogame chatMicrogame;

    private int viewers;
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

        //main microgame


        //chat microgame
        if(chatMicrogame.chatClicked == true) {
            chatMicrogame.chatClicked = false;
            viewers += Random.Range(90,111);
        }

        //discord microgame
        if(discordMicrogame.successFlag == "success") {
            viewers += Random.Range(150,211);
        }
        else if(discordMicrogame.successFlag == "fail") {
            viewers -= Random.Range(50,101);
        }
        else if(discordMicrogame.successFlag == "worse") {
            viewers -= Random.Range(150,211);
        }

        //coding microgame
        if(codingMicrogame.successFlag == "success") {
            viewers += Random.Range(50,76);
        }
        else if(codingMicrogame.successFlag == "fail") {
            viewers -= Random.Range(50,76);
        }
        else if(codingMicrogame.successFlag == "worse") {
            viewers -= Random.Range(90,121);
        }
        
        //dono microgame
        if(donoMicrogame.successFlag == "success") {
            donoMicrogame.setName = false;
            viewers += Random.Range(150,211);
        }
        else if(donoMicrogame.successFlag == "fail") {
            donoMicrogame.setName = false;
            viewers -= Random.Range(150,211);
        }
    }
}
