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
    private bool[] onTop;
    private bool[] open;
    [SerializeField] private Canvas[] appCanvases;
    private bool minimizing;

    void Start() {
        newMicrogameTimer = 0f;
        newMicrogameChance = 50;
        randomMicrogame = 0;
        timeIncDiff = 40f;
        onTop = new bool[] {true, false, false};
        open = new bool[] {true, true, true};
    }

    void FixedUpdate() {
        //wait 10s before spawning microgames
        if(Time.timeSinceLevelLoad <= 10f) {
            return;
        }
        if(Time.timeSinceLevelLoad >= timeIncDiff) {
            timeIncDiff += 15f;
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
        //switch that changes the initial orders
        switch(microgame) {
            case "main":
                //not on top, open
                if(mainCanvas.sortingOrder != 3 && mainCanvas.sortingOrder > -500) {
                    //then move to top
                    open[0] = true;
                    onTop[0] = true;
                    onTop[1] = false;
                    onTop[2] = false;

                    discordMicrogame.microgameOpen = false;
                    codingMicrogame.microgameOpen = false;
                }
                //on top
                else if(mainCanvas.sortingOrder == 3) {
                    //then minimize anim
                    mainAnimator.ResetTrigger("maximize");
                    mainAnimator.SetTrigger("minimize");
                    minimizing = true;

                    open[0] = false;
                    onTop[0] = false;

                    //if other apps open in background, choose which to set on top
                    if(open[1]) {
                        onTop[1] = true;
                    }
                    else if(open[2]) {
                        onTop[2] = true;
                    }
                }
                //not open
                else if(mainCanvas.sortingOrder <= -500) {
                    //then maximize anim
                    Debug.Log("maximize");
                    mainAnimator.ResetTrigger("minimize");
                    mainAnimator.SetTrigger("maximize");

                    discordMicrogame.microgameOpen = false;
                    codingMicrogame.microgameOpen = false;

                    open[0] = true;
                    onTop[0] = true;
                    onTop[1] = false;
                    onTop[2] = false;
                }
                break;

            case "disc":
                //not on top, open
                if(discCanvas.sortingOrder != 3 && discCanvas.sortingOrder > -500) {
                    //then move to top
                    open[1] = true;
                    onTop[0] = false;
                    onTop[1] = true;
                    onTop[2] = false;

                    discordMicrogame.microgameOpen = true;
                }
                //on top
                else if(discCanvas.sortingOrder == 3) {
                    //then minimize anim
                    discAnimator.ResetTrigger("maximize");
                    discAnimator.SetTrigger("minimize");
                    minimizing = true;

                    discordMicrogame.microgameOpen = false;

                    open[1] = false;
                    onTop[1] = false;

                    //if other apps open in background, choose which to set on top
                    if(open[2] && mainCanvas.sortingOrder < codeCanvas.sortingOrder) {
                        onTop[2] = true;
                    }
                    else {
                        onTop[0] = true;
                    }

                }
                //not open
                else if(discCanvas.sortingOrder <= -500) {
                    //then maximize anim
                    discAnimator.ResetTrigger("minimize");
                    discAnimator.SetTrigger("maximize");

                    discordMicrogame.microgameOpen = true;

                    open[1] = true;
                    onTop[0] = false;
                    onTop[1] = true;
                    onTop[2] = false;
                }
                if(!open[1] && !open[2]) {
                    onTop[0] = true;
                }
                break;

            case "code":
                //not on top, open
                if(codeCanvas.sortingOrder != 3 && codeCanvas.sortingOrder > -500) {
                    //then move to top
                    open[2] = true;
                    onTop[0] = false;
                    onTop[1] = false;
                    onTop[2] = true;

                    codingMicrogame.microgameOpen = true;
                }
                //on top
                else if(codeCanvas.sortingOrder == 3) {
                    //then minimize anim
                    codeAnimator.ResetTrigger("maximize");
                    codeAnimator.SetTrigger("minimize");
                    minimizing = true;

                    codingMicrogame.microgameOpen = false;

                    open[2] = false;
                    onTop[2] = false;

                    //if other apps open in background, choose which to set on top
                    if(open[1] && mainCanvas.sortingOrder < discCanvas.sortingOrder) {
                        onTop[1] = true;
                    }
                    else {
                        onTop[0] = true;
                    }
                }
                //not open
                else if(codeCanvas.sortingOrder <= -500) {
                    //then maximize anim
                    codeAnimator.ResetTrigger("minimize");
                    codeAnimator.SetTrigger("maximize");

                    codingMicrogame.microgameOpen = true;

                    open[2] = true;
                    onTop[0] = false;
                    onTop[1] = false;
                    onTop[2] = true;
                }
                if(!open[1] && !open[2]) {
                    onTop[0] = true;
                }
                break;
            default:
                break;
        }
        
        StartCoroutine(SetSortingOrder());
    }

    IEnumerator SetSortingOrder() {
        for(int i=0; i<open.Length; i++) {
            //check if open
            if(open[i]) {
                //sub 1 from sorting order
                appCanvases[i].sortingOrder--;
            }
            else if(!open[i] && appCanvases[i].sortingOrder > -500) {
                //otherwise its minimized so set to -500
                var animator = appCanvases[i].GetComponent<Animator>();

                while(minimizing) {
                    Debug.Log("minimizing " + appCanvases[i].name);
                    yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f);
                    yield return new WaitWhile(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f);
                    minimizing = false;
                }
                appCanvases[i].sortingOrder = -500;
            }
            if(onTop[i]) {
                //if on top, then set sorting order to 3
                //only one canvas should be on top at a time
                appCanvases[i].sortingOrder = 3;
            }

            yield return new WaitForEndOfFrame();
        }
    }
}
