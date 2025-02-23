using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class AdGeneration : MonoBehaviour
{
    [SerializeField] private GameObject[] adsPrefabs;
    private GameObject randAd;
    private float spawnTimer;
    public int spawnChance;

    [SerializeField] private CodingMicrogame codingMicrogame;
    public bool adsPaused;
    public bool incChance;

    void Start() {
        spawnChance = 25;
        spawnTimer = 0.0f;
    }

    void FixedUpdate() {
        //wait 10s before spawning microgames
        if(Time.timeSinceLevelLoad <= 10f) {
            return;
        }

        if(codingMicrogame.successFlag == "success") {
            StartCoroutine(AdsPaused());
        }
        else if(codingMicrogame.successFlag == "fail") {
            StartCoroutine(IncreasedChance());
            spawnChance = 50;
            
        }
        else if(codingMicrogame.successFlag == "worse") {
            StartCoroutine(IncreasedChance());
            spawnChance = 75;
        }

        spawnTimer += Time.deltaTime;

        if(Random.Range(0,100) < spawnChance && spawnTimer >= 1f && !adsPaused) {
            spawnTimer = 0.0f;

            randAd = adsPrefabs[Random.Range(0,adsPrefabs.Length)];
        
            float randPosX = Random.Range(-25,951);
            float randPosY = Random.Range(-25,526);

            Instantiate(randAd, new Vector3(randPosX,randPosY,0), Quaternion.Euler(Vector3.zero), this.transform);
        }
        if(spawnTimer >= 1f) {
            spawnTimer = 0.0f;
        }
    }

    IEnumerator AdsPaused() {
        adsPaused = true;

        while(adsPaused) {
            yield return new WaitForSeconds(10f);
            adsPaused = false;
        }
    }

    IEnumerator IncreasedChance() {
        incChance = true;

        while(incChance) {
            yield return new WaitForSeconds(10f);
            incChance = false;
        }
    }
}
