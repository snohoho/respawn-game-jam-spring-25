using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class DiscordMicrogame : MonoBehaviour
{
    public bool microgameOpen;
    public bool pinged;
    [SerializeField] private TextMeshProUGUI pingedText;
    private bool flickering;

    string[] msgToRespond = new string[]
        {
            "Sponsorship deal. This will give you viewers! We're dave fans since 2009 so you should take it!"
        };

    string[] msgToIgnore = new string[]
        {
            "This is obviously spam. Do not respond to this. If you do you're a LOSER!!!!!!!!!!!!!!!!!"
        };
    string[] daveResponseGood = new string[]
        {
            "sounds good bro!"
        };
    string[] daveResponseBad = new string[]
        {
            "shut up man"
        };

    [SerializeField] private TextMeshProUGUI discMsg;
    [SerializeField] private TextMeshProUGUI daveResponse;
    private int randMsgType;
    private string randMsg;
    public bool responseComplete;
    public string successFlag;
    private float responseTimer;
    private float responseTime;
    [SerializeField] Button replyButton;
    [SerializeField] Button ignoreButton;
    private bool messageSelected;
    public float cooldown;
    private float timeIncDiff;

    void Start() {
        pinged = false;
        responseComplete = false;
        microgameOpen = false;
        responseTimer = 0;
        flickering = false;
        messageSelected = false;
        timeIncDiff = 40f;
        responseTime = 10f;
    }

    void FixedUpdate() {
        if(cooldown > 0f) {
            cooldown -= Time.deltaTime;
            return;
        }
        if(Time.timeSinceLevelLoad >= timeIncDiff && responseTime >= 5f) {
            timeIncDiff += 30f;
            responseTime -= 1f;
        }

        if(pinged) {
            responseComplete = false;
            daveResponse.text = "";

            if(!messageSelected) {
                messageSelected = true;
                randMsgType = Random.Range(0,2);

                if(randMsgType == 0) {
                    randMsg = msgToRespond[Random.Range(0,msgToRespond.Length)];
                }
                else {
                    randMsg = msgToIgnore[Random.Range(0,msgToIgnore.Length)];
                }

                discMsg.text = randMsg;

                replyButton.gameObject.SetActive(true);
                ignoreButton.gameObject.SetActive(true);
            }

            if(!microgameOpen) {
                if(!flickering) {
                    StartCoroutine(FlickerPing());
                }

                responseTimer += Time.deltaTime;
                if(responseTimer > responseTime && !responseComplete) {
                    //count as a worse response failed
                    successFlag = "worse";
                    pinged = false;
                    responseTimer = 0f;
                    cooldown = 3f;
                    responseComplete = true;
                    pinged = false;
                    messageSelected = false;
                    pingedText.gameObject.SetActive(false);

                    flickering = false;
                }
            }
            
            if(microgameOpen) {
                pingedText.gameObject.SetActive(true);

                responseTimer += Time.deltaTime;
                if(responseTimer > responseTime && !responseComplete) {
                    //count as a worse response failed
                    successFlag = "worse";
                    responseTimer = 0f;
                    cooldown = 3f;
                    responseComplete = true;
                    pinged = false;
                    messageSelected = false;
                    pingedText.gameObject.SetActive(false);
                }
            }

            int temp = (int)responseTime - (int)responseTimer;
            pingedText.text = temp.ToString();
        }
        else if(!pinged) {
            replyButton.gameObject.SetActive(false);
            ignoreButton.gameObject.SetActive(false);
            pingedText.gameObject.SetActive(false);

            discMsg.text = "No new messages.";
            daveResponse.text = "";
        }
    }

    public void ButtonResponse(string resp) {
        responseTimer = 0f;
        cooldown = 3f;
        responseComplete = true;
        pinged = false;
        messageSelected = false;
        pingedText.gameObject.SetActive(false);

        //respond good/approve
        if(resp == "good") {
            daveResponse.text = daveResponseGood[Random.Range(0,daveResponseGood.Length)];
            if(randMsgType == 0) {
                //count as response success
                successFlag = "success";
            }
            else {
                //count as response failed
                successFlag = "fail";
            }
        }
        //respond poorly/deny
        if(resp == "bad") {
            daveResponse.text = daveResponseBad[Random.Range(0,daveResponseBad.Length)];
            if(randMsgType == 0) {
                //count as response failed
                successFlag = "fail";
            }
            else {
                //count as response success
                successFlag = "success";
            }
        }

        replyButton.gameObject.SetActive(false);
        ignoreButton.gameObject.SetActive(false);
    }

    IEnumerator FlickerPing() {
        flickering = true;

        while(pinged && !microgameOpen && flickering) {
            pingedText.gameObject.SetActive(!pingedText.gameObject.activeSelf);

            yield return new WaitForSeconds(0.1f);
        }

        flickering = false;
    }
}
