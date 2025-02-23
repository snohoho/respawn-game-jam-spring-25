using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private GameObject audioSource;
    [SerializeField] public AudioClip[] audioClips;

    public void CreateSource(AudioClip clip) {
        var newSource = Instantiate(audioSource);    
        AudioSource source = newSource.GetComponent<AudioSource>();
        source.clip = clip;
        float length = clip.length;

        source.Play();
        Destroy(newSource, length);
    }
    
}
