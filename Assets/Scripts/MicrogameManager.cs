using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicrogameManager : MonoBehaviour
{
    [SerializeField] private MainMicrogame mainMicrogame;
    [SerializeField] private Canvas mainCanvas;
    [SerializeField] private CodingMicrogame codingMicrogame;
    [SerializeField] private Canvas codeCanvas;
    [SerializeField] private DiscordMicrogame discordMicrogame;
    [SerializeField] private Canvas discCanvas;
    [SerializeField] private DonoMicrogame donoMicrogame;
    [SerializeField] private ChatMicrogame chatMicrogame;
    private float timeBeforeStart;
    private float newMicrogameTimer;
    private int newMicrogameChance;
    private int randomMicrogame;

    void Start() {
        timeBeforeStart = 10f;

        newMicrogameTimer = 0f;
        newMicrogameChance = 50;
        randomMicrogame = 0;
    }

    void Update() {
        
    }

    void FixedUpdate() {
        //wait 10s before spawning microgames
        if(timeBeforeStart > 0) {
            timeBeforeStart -= Time.deltaTime;
            return;
        }

        newMicrogameTimer += Time.deltaTime;
        
        if(Random.Range(0,100) < newMicrogameChance && newMicrogameTimer >= 5f) {
            randomMicrogame = Random.Range(0,3);
            switch(randomMicrogame) {
                case 0:
                    if(codingMicrogame.pinged == false) {
                        codingMicrogame.pinged = true;
                    }
                    break;
                case 1:
                    if(discordMicrogame.pinged == false) {
                        discordMicrogame.pinged = true;
                    }
                    break;
                case 2:
                    if(donoMicrogame.pinged == false) {
                        donoMicrogame.pinged = true;
                    }
                    break;
                default:
                    break;
            }
        }
        if(newMicrogameTimer >= 1f) {
            newMicrogameTimer = 0.0f;
        }
    }

    public void OpenMicrogame(string microgame) {
        switch(microgame) {
            case "main":
                if(mainCanvas.sortingOrder != 2) {
                    mainCanvas.sortingOrder = 2;
                    
                    discCanvas.sortingOrder--;
                    codeCanvas.sortingOrder--;
                }
                discordMicrogame.microgameOpen = false;
                codingMicrogame.microgameOpen = false;
                break;
            case "disc":
                if(discCanvas.sortingOrder != 2) {
                    discCanvas.sortingOrder = 2;

                    mainCanvas.sortingOrder--;
                    codeCanvas.sortingOrder--;
                }
                discordMicrogame.microgameOpen = true;
                break;
            case "code":
                if(codeCanvas.sortingOrder != 2) {
                    codeCanvas.sortingOrder = 2;

                    mainCanvas.sortingOrder--;
                    discCanvas.sortingOrder--;
                }
                codingMicrogame.microgameOpen = true;
                break;
            default:
                break;
        }
    }
}
