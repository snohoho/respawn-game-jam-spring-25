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
            "   }\n" +
            "   if(I == HIM && I == \"will\") {\n" +
            "       I = \"to be him\"\n" +
            "   }\n"
        };
    [SerializeField] private TextMeshProUGUI codeText;
    private int randCodeBlock;
    private int currStringCt;
    private bool buttonPressed;
    public bool codeComplete;
    private float responseTimer;
    private System.IDisposable keyboardListener;

    void Start() {
        codeText.text = "public class CounterHackProtocol {\n";

        randCodeBlock = Random.Range(0,codeBlocks.Length);
        currStringCt = 0;
        microgameOpen = true;
        buttonPressed = false;
        codeComplete = false;
    }

    void OnEnable() {
        if(!buttonPressed && !codeComplete) {
            keyboardListener = InputSystem.onAnyButtonPress.Call(AddText);
        }
    }

    void OnDisable() {
        keyboardListener.Dispose();
    }

    void FixedUpdate() {
        if(pinged && !microgameOpen) {
            responseTimer += Time.deltaTime;

            if(responseTimer > 10.0f && !codeComplete) {
                //count as a worse response failed
                pinged = false;
            }
        }

        if(microgameOpen) {
            responseTimer += Time.deltaTime;
            if(responseTimer > 10.0f && !codeComplete) {
                //count as a worse response failed
            }

            if(codeComplete) {

            }
        }
    }

    private void AddText(InputControl act) {
        buttonPressed = act.IsPressed();
        if(microgameOpen && act is KeyControl) {
            if(currStringCt >= codeBlocks[randCodeBlock].Length) {
                currStringCt = 0;
                codeComplete = true;
                codeText.text += "}\n\n";
                codeText.text += "public class CounterHackProtocol {\n";

                randCodeBlock = Random.Range(0,codeBlocks.Length);
            }
            else {
                codeText.text += codeBlocks[randCodeBlock][currStringCt];
                currStringCt++;
            }
        }
    }
}
