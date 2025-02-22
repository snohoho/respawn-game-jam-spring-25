using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdGeneration : MonoBehaviour
{
    [SerializeField] GameObject[] adsPrefabs;
    GameObject randAd;
    float spawnTimer;
    int spawnChance;

    void Start() {
        spawnChance = 10;
    }

    void Update() {
        
    }

    void FixedUpdate() {
        spawnTimer += Time.deltaTime;

        if(Random.Range(0,100) < spawnChance && spawnTimer > 0.5f) {
            spawnTimer = 0.0f;

            randAd = adsPrefabs[Random.Range(0,adsPrefabs.Length)];
        
            float randPosX = Random.Range(-25,951);
            float randPosY = Random.Range(-25,526);

            Instantiate(randAd, new Vector3(randPosX,randPosY,0), Quaternion.Euler(Vector3.zero), this.transform);
        }   
    }

    
}
