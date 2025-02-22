using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ViewersManager : MonoBehaviour
{
    //[SerializeField] MainMicrogame mainMicrogame;
    [SerializeField] CodingMicrogame codingMicrogame;
    [SerializeField] DiscordMicrogame discordMicrogame;
    //[SerializeField] DonoMicrogame donoMicrogame;
    //[SerializeField] ChatMicrogame chatMicrogame;
    private int viewers;
    private float sinceStartTimer;
    private float viewerLossTimer;
    private float viewerLossMult;

    void Start()
    {
        viewers = 1000;
        sinceStartTimer = 0f;
        viewerLossMult = 1f;
    }

    void Update()
    {
        
    }

    void FixedUpdate() {
        sinceStartTimer = Time.timeSinceLevelLoad;
        viewerLossTimer += Time.deltaTime;

        //every 30s increase the multiplier for lost viewers
        if(Mathf.Round(sinceStartTimer % 30) == 0) {
            viewerLossMult += 0.25f;
        }

        if(viewerLossTimer > 1f) {
            viewers -= (int)Mathf.Round(10.0f * viewerLossMult);
        }
    }
}
