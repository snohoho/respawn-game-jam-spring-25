using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class DaveWebcam : MonoBehaviour
{
    [SerializeField] private ViewersManager viewersManager;
    [SerializeField] private Sprite[] daveExpressions;
    [SerializeField] private Image webcam;

    void FixedUpdate()
    {
       if(viewersManager.viewers >= 1000) {
            webcam.sprite = daveExpressions[0];
       }
       else if(viewersManager.viewers >= 800) {
            webcam.sprite = daveExpressions[1];
       }
       else if(viewersManager.viewers >= 600) {
            webcam.sprite = daveExpressions[2];   
       }
       else if(viewersManager.viewers >= 400) {
            webcam.sprite = daveExpressions[3];
       }
       else if(viewersManager.viewers <= 200) {
            webcam.sprite = daveExpressions[4];
       }
    }
}
