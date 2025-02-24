using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.InputSystem.Controls;

public class CodingMicrogame : MonoBehaviour
{
    public bool microgameOpen;
    public bool pinged;
    [SerializeField] private TextMeshProUGUI pingedText;
    private bool flickering;

    string[] codeBlocks = new string[]
        {
            "   this.isNotReal(stopReading);\n" +
            "   if(godCanHelp) {\n" +
            "       Debug.Log(\"God cannot help you now.\")\n" +
            "   }\n",
            ////////////////////////////////////////////////
            "   string I = \"me\";\n" +
            "   string HIM = \"me\";\n" +
            "   if(I == HIM) {\n" +
            "       I = \"will\"\n" +
            "       continue;\n" +
            "   }\n",
            "   for(file in AllFiles)\n" +
            "   {\n" +
	        "       if (file == virus)\n" +
		    "           Delete file\n" +
		    "           Debug.Log(\"Malicious file deleted, moving on\")\n" +
	        "       else\n" +
		    "           Debug.Log(\"Normal file, moving on\")\n" +
            "   }\n",
            "   while (true)\n" +
            "   {\n" +
	        "       GettingHacked = false\n" +
	        "       AntiVirus.isOn = true\n" +
	        "       CounterHack(hacker)\n" +
	        "       SendUpdate(\"Get pwned loser\", \"1\")\n" +
	        "       Stream.bitrate = 99999\n" +
            "   }\n",
            "   if(GettingHacked)\n" +
            "   {\n" +
	        "       StopHack(virus)\n" +
	        "       CounterHack(hacker)\n" +
	        "       DeleteViruses()\n" +
	        "       AntiVirus.isOff = false\n" +
            "   }\n"
        };
    [SerializeField] private TextMeshProUGUI codeText;
    private int randCodeBlock;
    private int currStringCt;
    private bool buttonPressed;
    public bool codeComplete;
    private float responseTimer;
    public string successFlag;
    private System.IDisposable keyboardListener;
    private bool messageSelected;
    public float cooldown;
    private float timeIncDiff;
    private float responseTime;
    [SerializeField] AudioManager audioManager;


    void Start() {
        codeText.text = "public class CounterHackProtocol {\n";

        randCodeBlock = Random.Range(0,codeBlocks.Length);
        currStringCt = 0;
        microgameOpen = false;
        buttonPressed = false;
        codeComplete = false;
        pinged = false;
        flickering = false;
        messageSelected = false;
        cooldown = 0f;
        timeIncDiff = 40f;
        responseTime = 15f;
    }

    void OnEnable() {
        keyboardListener = InputSystem.onAnyButtonPress.Call(AddText);
    }

    void OnDisable() {
        keyboardListener.Dispose();
    }

    void FixedUpdate() {
        if(cooldown > 0f) {
            cooldown -= Time.deltaTime;
            return;
        }
        if(Time.timeSinceLevelLoad >= timeIncDiff && responseTime >= 5f) {
            timeIncDiff += 15f;
            responseTime -= 1f;
        }

        if(pinged) {
            codeComplete = false;

            if(!messageSelected) {
                messageSelected = true;
                codeText.text = "[HACK DETECTED. BEGIN COUNTERHACK!]\n" +
                            "public class CounterHackProtocol {\n";
                
                randCodeBlock = Random.Range(0,codeBlocks.Length);
            }

            if(!microgameOpen) {
                if(!flickering) {
                    StartCoroutine(FlickerPing());
                }

                responseTimer += Time.deltaTime;
                if(responseTimer > responseTime && !codeComplete) {
                    //count as a worse response failed
                    successFlag = "worse";
                    currStringCt = 0;
                    cooldown = 3f;
                    responseTimer = 0f;
                    codeComplete = true;
                    messageSelected = false;
                    pinged = false;
                    codeText.text += "\n\n[COUNTERHACK UNSUCCESSFUL]";
                    pingedText.gameObject.SetActive(false);
                    audioManager.CreateSource(audioManager.audioClips[10]);

                    flickering = false;
                }
            }
            else if(microgameOpen) {
                pingedText.gameObject.SetActive(true);

                responseTimer += Time.deltaTime;           
                if(responseTimer > responseTime && !codeComplete) {
                    successFlag = "fail";
                    currStringCt = 0;
                    cooldown = 3f;
                    responseTimer = 0f;
                    codeComplete = true;
                    messageSelected = false;
                    pinged = false;
                    codeText.text += "\n\n[COUNTERHACK UNSUCCESSFUL]";
                    pingedText.gameObject.SetActive(false);
                    audioManager.CreateSource(audioManager.audioClips[10]);
                }
            }

            int temp = (int)responseTime - (int)responseTimer;
            pingedText.text = temp.ToString();
        }
        else if(!pinged) {
            pingedText.gameObject.SetActive(false);
            codeText.text = "[NO HACKING DETECTED]";
        }
    }

    private void AddText(InputControl act) {
        buttonPressed = act.IsPressed();
        if(microgameOpen && !codeComplete) {
            if(currStringCt >= codeBlocks[randCodeBlock].Length) {
                successFlag = "success";
                currStringCt = 0;
                cooldown = 3f;
                responseTimer = 0f;
                codeComplete = true;
                messageSelected = false;
                pinged = false;
                codeText.text += "}\n\n";
                codeText.text += "[COUNTERHACK SUCCESSFUL]";
                pingedText.gameObject.SetActive(false);
                audioManager.CreateSource(audioManager.audioClips[9]);
            }
            else {
                codeText.text += codeBlocks[randCodeBlock][currStringCt];
                currStringCt++;
            }
        }
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
