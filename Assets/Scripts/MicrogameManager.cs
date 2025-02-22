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
    private Queue<int> microgameOrder;
    private int[] microgameOrderArr;

    void Start() {
        timeBeforeStart = 10f;

        newMicrogameTimer = 0f;
        newMicrogameChance = 50;
        randomMicrogame = 0;

        microgameOrder = new Queue<int>();
        microgameOrder.Enqueue(0);
        microgameOrder.Enqueue(1);
        microgameOrder.Enqueue(2);
    }

    void Update() {
        microgameOrderArr = microgameOrder.ToArray();
        mainCanvas.sortingOrder = System.Array.IndexOf(microgameOrderArr, 0) + 1;
        codeCanvas.sortingOrder = System.Array.IndexOf(microgameOrderArr, 1) + 1;
        discCanvas.sortingOrder = System.Array.IndexOf(microgameOrderArr, 2) + 1;
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
        int v;
        switch(microgame) {
            case "main":
                if(discordMicrogame.microgameOpen || codingMicrogame.microgameOpen) {
                    v = microgameOrder.Dequeue();
                    microgameOrder.Enqueue(v);
                }
                discordMicrogame.microgameOpen = false;
                codingMicrogame.microgameOpen = false;
                break;
            case "disc":
                if(!discordMicrogame.microgameOpen) {
                    v = microgameOrder.Dequeue();
                    microgameOrder.Enqueue(v);
                }
                discordMicrogame.microgameOpen = true;
                break;
            case "code":
                if(!codingMicrogame.microgameOpen) {
                    v = microgameOrder.Dequeue();
                    microgameOrder.Enqueue(v);
                }
                codingMicrogame.microgameOpen = true;
                break;
            default:
                break;
        }
    }
}
