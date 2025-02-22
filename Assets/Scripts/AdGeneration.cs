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

    [SerializeField] CodingMicrogame codingMicrogame;
    public bool adsPaused;

    void Start() {
        spawnChance = 10;
        spawnTimer = 0.0f;
    }

    void FixedUpdate() {
        //stop ads from showing up
        if(codingMicrogame.codeComplete == true) {
            StartCoroutine(AdsPaused());
            codingMicrogame.codeComplete = false;
        }

        spawnTimer += Time.deltaTime;

        if(Random.Range(0,100) < spawnChance && spawnTimer > 0.5f) {
            spawnTimer = 0.0f;

            randAd = adsPrefabs[Random.Range(0,adsPrefabs.Length)];
        
            float randPosX = Random.Range(-25,951);
            float randPosY = Random.Range(-25,526);

            Instantiate(randAd, new Vector3(randPosX,randPosY,0), Quaternion.Euler(Vector3.zero), this.transform);
        }   
    }

    IEnumerator AdsPaused() {
        adsPaused = true;

        while(adsPaused) {
            yield return new WaitForSeconds(10f);
            adsPaused = false;
        }
    }
}
