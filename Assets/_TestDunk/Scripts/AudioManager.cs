using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClips;

    private void Start()
    {
        instance = this;       
    }

    public void PlayOneShot(Clips clips)
    {
        audioSource.PlayOneShot(audioClips[(int)clips]);
    }
}

public enum Clips
{
    Catch,
    Dash,
    Die,
    Button,
    CatchStar
}