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
    private float newMicrogameTimer;
    private int newMicrogameChance;
    private int randomMicrogame;
    private float timeIncDiff;

    void Start() {
        newMicrogameTimer = 0f;
        newMicrogameChance = 50;
        randomMicrogame = 0;
        timeIncDiff = 40f;
    }

    void FixedUpdate() {
        //wait 10s before spawning microgames
        if(Time.timeSinceLevelLoad <= 10f) {
            return;
        }
        if(Time.timeSinceLevelLoad >= timeIncDiff) {
            timeIncDiff += 30f;
            newMicrogameChance += 10;
            newMicrogameTimer -= 0.5f;
        }

        newMicrogameTimer += Time.deltaTime;
        
        if(Random.Range(0,100) < newMicrogameChance && newMicrogameTimer >= 5f) {
            randomMicrogame = Random.Range(0,3);
            Debug.Log("selected microgame: " + randomMicrogame);
            switch(randomMicrogame) {
                case 0:
                    if(codingMicrogame.pinged == false && codingMicrogame.cooldown <= 0f) {
                        Debug.Log("run coding mg");
                        codingMicrogame.pinged = true;
                    }
                    break;
                case 1:
                    Debug.Log("run disc mg");
                    if(discordMicrogame.pinged == false) {
                        discordMicrogame.pinged = true;
                    }
                    break;
                case 2:
                    Debug.Log("run dono mg");
                    if(donoMicrogame.pinged == false) {
                        
                        donoMicrogame.pinged = true;
                    }
                    break;
                default:
                    break;
            }
        }
        if(newMicrogameTimer >= 5f) {
            newMicrogameTimer = 0.0f;
        }
    }

    public void OpenMicrogame(string microgame) {
        switch(microgame) {
            case "main":
                if(mainCanvas.sortingOrder != 3) {
                    mainCanvas.sortingOrder = 3;
                }
                else if(mainCanvas.sortingOrder == 3) {
                    mainCanvas.sortingOrder = -500;
                }
                discCanvas.sortingOrder--;
                codeCanvas.sortingOrder--;

                discordMicrogame.microgameOpen = false;
                codingMicrogame.microgameOpen = false;
                break;
            case "disc":
                if(discCanvas.sortingOrder != 3) {
                    discCanvas.sortingOrder = 3;
                    discordMicrogame.microgameOpen = true;
                }
                else if(discCanvas.sortingOrder == 3) {
                    discCanvas.sortingOrder = -500;
                    discordMicrogame.microgameOpen = false;
                }
                mainCanvas.sortingOrder--;
                codeCanvas.sortingOrder--;
                break;
            case "code":
                if(codeCanvas.sortingOrder != 3) {
                    codeCanvas.sortingOrder = 3;
                    codingMicrogame.microgameOpen = true;
                }
                else if(codeCanvas.sortingOrder == 3) {
                    codeCanvas.sortingOrder = -500;
                    codingMicrogame.microgameOpen = false;
                }
                mainCanvas.sortingOrder--;
                discCanvas.sortingOrder--;
                break;
            default:
                break;
        }
    }
}
