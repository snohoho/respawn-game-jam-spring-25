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

    void Start() {
        pinged = false;
        readDono = false;
        setName = false;
    }

    void Update() {
        
    }

    void FixedUpdate() {
        if(pinged) {
            if(!setName) {
                donoNameText.text = names[Random.Range(0,names.Length)] +
                                "GAVE $" + Random.Range(5,10000);
                donoMsgText.text = donoMsg[Random.Range(0,donoMsg.Length)];
                setName = true;
            }

            responseTimer += Time.deltaTime;

            if(responseTimer >= 10.0f && !readDono) {
                pinged = false;
                successFlag = "fail";
            }
        }
    }

    public void ReadDono() {
        pinged = false;
        successFlag = "success";
        readDono = true;
    }
}
