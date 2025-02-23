using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    [SerializeField] AudioManager audioManager;
    [SerializeField] Animator mainAnimator;
    [SerializeField] Animator discAnimator;
    [SerializeField] Animator codeAnimator;

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
            switch(randomMicrogame) {
                case 0:
                    if(codingMicrogame.pinged == false && codingMicrogame.cooldown <= 0f) {
                        Debug.Log("run coding mg");
                        codingMicrogame.pinged = true;
                        audioManager.CreateSource(audioManager.audioClips[8]);
                    }
                    break;
                case 1:
                    Debug.Log("run disc mg");
                    if(discordMicrogame.pinged == false && discordMicrogame.cooldown <= 0f) {
                        discordMicrogame.pinged = true;
                        audioManager.CreateSource(audioManager.audioClips[0]);
                    }
                    break;
                case 2:
                    Debug.Log("run dono mg");
                    if(donoMicrogame.pinged == false) { 
                        donoMicrogame.pinged = true;
                        audioManager.CreateSource(audioManager.audioClips[1]);
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
                    mainAnimator.ResetTrigger("minimize");
                    mainAnimator.SetTrigger("maximize");
                    discordMicrogame.microgameOpen = false;
                    codingMicrogame.microgameOpen = false;
                }
                else if(mainCanvas.sortingOrder == 3) {
                    StartCoroutine(WaitForAnim(mainCanvas, mainAnimator));
                    mainAnimator.ResetTrigger("maximize");
                    mainAnimator.SetTrigger("minimize");

                    if(discCanvas.sortingOrder <= -500) {
                        discordMicrogame.microgameOpen = true;
                    }
                    if(codeCanvas.sortingOrder <= -500) {
                        codingMicrogame.microgameOpen = true;
                    }
                    
                    codingMicrogame.microgameOpen = false;
                }
                
                if(discCanvas.sortingOrder <= -500) {
                    discordMicrogame.microgameOpen = false;
                }
                if(codeCanvas.sortingOrder <= -500) {
                    codingMicrogame.microgameOpen = false;
                }

                discCanvas.sortingOrder--;
                codeCanvas.sortingOrder--;
                break;
            case "disc":
                if(discCanvas.sortingOrder != 3) {
                    discCanvas.sortingOrder = 3;
                    discAnimator.ResetTrigger("minimize");
                    discAnimator.SetTrigger("maximize");
                    discordMicrogame.microgameOpen = true;
                }
                else if(discCanvas.sortingOrder == 3) {
                    StartCoroutine(WaitForAnim(discCanvas, discAnimator));
                    discAnimator.ResetTrigger("maximize");
                    discAnimator.SetTrigger("minimize");
                    discordMicrogame.microgameOpen = false;
                }
                mainCanvas.sortingOrder--;
                codeCanvas.sortingOrder--;
                break;
            case "code":
                if(codeCanvas.sortingOrder != 3) {
                    codeCanvas.sortingOrder = 3;
                    codeAnimator.ResetTrigger("minimize");
                    codeAnimator.SetTrigger("maximize");
                    codingMicrogame.microgameOpen = true;
                }
                else if(codeCanvas.sortingOrder == 3) {
                    StartCoroutine(WaitForAnim(codeCanvas, codeAnimator));
                    codeAnimator.ResetTrigger("maximize");
                    codeAnimator.SetTrigger("minimize");
                    codingMicrogame.microgameOpen = false;
                }
                mainCanvas.sortingOrder--;
                discCanvas.sortingOrder--;
                break;
            default:
                break;
        }
    }

    IEnumerator WaitForAnim(Canvas canvas, Animator animator) {
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f );
        yield return new WaitWhile(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f);
        canvas.sortingOrder = -500;
    }
}
