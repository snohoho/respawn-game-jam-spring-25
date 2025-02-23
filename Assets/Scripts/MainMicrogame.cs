using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System.Threading;

public class MainMicrogame : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private RectTransform playerTransform;
    private int hp;
    [SerializeField] private GameObject[] hpIcons;
    [SerializeField] private GameObject[] movementButtons;
    
    [SerializeField] private GameObject[] obstacles;
    [SerializeField] private GameObject[] obstacleSpawners;
    private float cooldown;
    public string successFlag;

    [SerializeField] GameObject resetPanel;
    [SerializeField] TextMeshProUGUI resetText;
    private bool resetting;
    private bool runningCR;
    public bool survived;
    private float survivalTimer;
    [SerializeField] AudioManager audioManager;

    void Start()
    {
        playerTransform = player.GetComponent<RectTransform>();
        cooldown = 0f;
        hp = 3;
        resetPanel.SetActive(false);
        survivalTimer = 0f;
        audioManager.CreateSource(audioManager.audioClips[5]);
    }

    void FixedUpdate()
    {
        if(resetting) {
            if(!runningCR) {
                runningCR = true;
                StartCoroutine(ResetGame());
            }
            return;
        }

        if(cooldown <= 0) {
            int spawner = Random.Range(0,3);
            GameObject obst = Instantiate(obstacles[Random.Range(0,obstacles.Length)], obstacleSpawners[spawner].transform);
            StartCoroutine(MoveObstacle(obst.GetComponent<RectTransform>()));

            cooldown = 3f;
        }

        cooldown -= Time.deltaTime;

        if(player.GetComponent<PlayerCollider>().hit) {
            Debug.Log("remove hp");

            hp--;
            hpIcons[hp].SetActive(false);
            StartCoroutine(PlayerHit());
            PersistentData.AsteroidsHit++;
            player.GetComponent<PlayerCollider>().hit = false;
            if(hp == 0) {
                audioManager.CreateSource(audioManager.audioClips[7]);
                successFlag = "fail";
                resetting = true;
                resetText.text = "Restarting...";
            }
        }

        if(survivalTimer >= 30f) {
            successFlag = "success";
            survivalTimer = 0f;
        }
        survivalTimer += Time.deltaTime;
    }

    public void MoveToButton(int button) {
        if(resetting) {
            return;
        }

        switch(button) {
            case 0:
                StartCoroutine(MovePlayer(playerTransform, 297));
                break;
            case 1:
                StartCoroutine(MovePlayer(playerTransform, 0));
                break;
            case 2:
                StartCoroutine(MovePlayer(playerTransform, -297));
                break;
            default:
                break;
        }
    }

    IEnumerator ResetGame() {
        resetPanel.SetActive(true);

        yield return new WaitForSeconds(3f);

        hpIcons[0].SetActive(true);
        hpIcons[1].SetActive(true);
        hpIcons[2].SetActive(true);
        hp = 3;
        survivalTimer = 0f;
        playerTransform.localPosition = new Vector3 (200,0,0);

        int countdown = 3;
        while(countdown > 0) {
            resetText.text = countdown.ToString();
            yield return new WaitForSeconds(1f);
            countdown--;
        }
        resetPanel.SetActive(false);
        resetting = false;
        runningCR = false;
        audioManager.CreateSource(audioManager.audioClips[5]);
    }

    IEnumerator PlayerHit() {
        audioManager.CreateSource(audioManager.audioClips[2]);

        int loopCt = 0;
        while(loopCt < 5) {
            player.GetComponent<Image>().enabled = false;
            yield return new WaitForSeconds(0.1f);
            player.GetComponent<Image>().enabled = true;
            yield return new WaitForSeconds(0.1f);

            loopCt++;
        }
    }

    IEnumerator MovePlayer(RectTransform obj, float goalPos) {
        float elapsedTime = 0f;
        float movTime = 0.4f;
        Vector3 startPos = obj.localPosition;

        while(obj.localPosition.y != goalPos) {
            obj.localPosition = new Vector3(obj.localPosition.x, Mathf.SmoothStep(startPos.y, goalPos, elapsedTime/movTime), 0);
            elapsedTime += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator MoveObstacle(RectTransform obj) {
        float movTime = Random.Range(2f,5f); 
        float elapsedTime = 0f;
        Vector3 startPos = obj.localPosition;
        Vector3 goalPos = new Vector3(-2300, obj.localPosition.y, 0);

        while(elapsedTime < movTime) {
            obj.localPosition = Vector3.Lerp(startPos, goalPos, elapsedTime/movTime);
            elapsedTime += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        Destroy(obj.gameObject);
    }
}
