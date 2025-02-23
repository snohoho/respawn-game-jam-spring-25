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
    void Start()
    {
        cooldown = 0f;
    }

    void FixedUpdate()
    {
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
