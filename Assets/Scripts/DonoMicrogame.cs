using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.UI;

public class DonoMicrogame : MonoBehaviour
{
    string[] names = new string[]
        {
            "MinecraftSteve"
        };

    string[] donoMsg = new string[]
        {
            "hey man"
        };

    public bool pinged;
    private bool readDono;
    public bool setName;
    private float responseTimer;
    public string successFlag;
    [SerializeField] private TextMeshProUGUI donoNameText;
    [SerializeField] private TextMeshProUGUI donoMsgText;
    [SerializeField] private Canvas donoCanvas;
    [SerializeField] private TextMeshProUGUI pingedText;
    private bool flickering;

    void Start() {
        pinged = false;
        readDono = false;
        setName = false;
        responseTimer = 0f;
        flickering = false;
    }

    void FixedUpdate() {
        if(pinged) {
            readDono = false;
            
            if(!flickering) {
                StartCoroutine(FlickerPing());
            }

            if(!setName) {
                donoNameText.text = names[Random.Range(0,names.Length)] +
                                " GAVE $" + Random.Range(5,10000);
                donoMsgText.text = donoMsg[Random.Range(0,donoMsg.Length)];
                setName = true;

                donoCanvas.sortingOrder = 8;
            }

            responseTimer += Time.deltaTime;

            if(responseTimer >= 3.0f && !readDono) {
                pinged = false;
                successFlag = "fail";
                setName = false;
                responseTimer = 0f;
                donoCanvas.sortingOrder = -500;
            }

            int temp = 3 - (int)responseTimer;
            pingedText.text = temp.ToString();
        }
    }

    public void ReadDono() {
        pinged = false;
        successFlag = "success";
        readDono = true;
        setName = false;
        responseTimer = 0f;
        donoCanvas.sortingOrder = -500;
    }

    IEnumerator FlickerPing() {
        flickering = true;

        while(pinged) {
            pingedText.gameObject.SetActive(!pingedText.gameObject.activeSelf);

            yield return new WaitForSeconds(0.1f);
        }

        flickering = false;
    }
}
