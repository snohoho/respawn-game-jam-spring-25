using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatMicrogame : MonoBehaviour
{
    private float cooldown;
    public bool chatClicked;
    void Start()
    {
        cooldown = 0f;
    }

    void FixedUpdate()
    {
        if(cooldown > 0) {
            cooldown -= Time.deltaTime;
        }
    }

    public void RespondToChat() {
        if(cooldown <= 0) {
            chatClicked = true;
        }
        cooldown = 15f;
    }
}
