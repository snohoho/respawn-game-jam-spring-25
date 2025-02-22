using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DiscordMicrogame : MonoBehaviour
{
    public bool microgameOpen;
    public bool pinged;
    public bool responding;

    string[] msgToRespond = new string[]
        {
            "Sponsorship deal. This will give you viewers! We're dave fans since 2009 so you should take it!"
        };

    string[] msgToIgnore = new string[]
        {
            "This is obviously spam. Do not respond to this. If you do you're a LOSER!!!!!!!!!!!!!!!!!"
        };

    [SerializeField] private TextMeshProUGUI discMsg;
    private int randMsgType;
    private string randMsg;
    public bool responseComplete;
    private float responseTimer;

    void Start() {
        pinged = false;
        responseComplete = false;
        microgameOpen = false;
        responseTimer = 0;
    }

    void Update() {
        if(pinged && microgameOpen && !responding) {
            responding = true;
            randMsgType = Random.Range(0,2);

            if(randMsgType == 0) {
                randMsg = msgToRespond[Random.Range(0,msgToRespond.Length)];
            }
            else {
                randMsg = msgToRespond[Random.Range(0,msgToIgnore.Length)];
            }

            discMsg.text = randMsg;
        }
    }

    void FixedUpdate() {
        if(pinged && !microgameOpen) {
            responseTimer += Time.deltaTime;

            if(responseTimer > 10.0f && !responseComplete) {
                //count as a worse response failed
                pinged = false;
            }
        }

        if(microgameOpen) {
            responseTimer += Time.deltaTime;
            if(responseTimer > 10.0f && !responseComplete) {
                //count as a worse response failed
            }

            if(responseComplete) {

            }
        }
    }

    public void buttonResponseReply() {
        responseComplete = true;
        pinged = false;
        if(randMsgType == 0) {
            //count as response success
        }
        else {
            //count as response failed
        }
    }

    public void buttonResponseIgnore() {
        responseComplete = true;
        pinged = false;
        if(randMsgType == 0) {
            //count as response failed
        }
        else {
            //count as response success
        }
    }
}
