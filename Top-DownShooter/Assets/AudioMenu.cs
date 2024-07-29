using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMenu : MonoBehaviour
{
    [Header("---Audio Menu---")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioClip audioClip;  

    void Start()
    {
        if (musicSource != null && audioClip != null)
        {
            musicSource.clip = audioClip;  
            musicSource.Play();            
        }
        else
        {
            Debug.LogWarning("!!!!!");
        }
    }
}
