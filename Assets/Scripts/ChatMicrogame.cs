using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatMicrogame : MonoBehaviour
{
    private float cooldown;
    public bool chatClicked;
    private bool flickering;
    private bool pinged;
    [SerializeField] private TextMeshProUGUI pingedText;
    string[] messages = new string[]
        {
            "MinecraftSteve: hi"
        };
    [SerializeField] private TextMeshProUGUI chat;
    private float chatCD;

    void Start()
    {
        cooldown = 0f;
        chatCD = 0f;
    }

    void FixedUpdate()
    {
        if(chatCD <= 0f) {
            chat.text += "\n" + messages[Random.Range(0,messages.Length)];
            chatCD = Random.Range(0.1f,1f);
        }
        chatCD -= Time.deltaTime;

        if(cooldown > 0) {
            cooldown -= Time.deltaTime;
        }
        if(cooldown <= 0) {
            if(!flickering) {
                pingedText.gameObject.SetActive(true);
                StartCoroutine(FlickerPing());
            }
            
            pinged = true;       
        }
    }

    public void RespondToChat() {
        if(cooldown <= 0) {
            pinged = false;
            chatClicked = true;
            pingedText.gameObject.SetActive(false);
        }
        cooldown = 15f;
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
