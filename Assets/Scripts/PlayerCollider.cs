using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    private BoxCollider2D playerCol;
    public bool hit;
    void Start()
    {
        playerCol = GetComponent<BoxCollider2D>();
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("player hit");
        hit = true;
    }
}
