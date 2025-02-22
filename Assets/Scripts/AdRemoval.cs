using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdRemoval : MonoBehaviour
{
    public void RemoveAd() {
        Debug.Log("remove");
        Destroy(this.gameObject);
    }
}
