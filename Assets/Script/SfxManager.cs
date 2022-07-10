using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class SfxManager : MonoBehaviour
{
    public AudioSource Audio;
 
    public AudioClip Click;
 
    public static SfxManager sfxInstance;
 
    private void Awake()
    {
        if (sfxInstance != null && sfxInstance != this)
        {
            Destroy(this.gameObject);
            return;
        }
 
        sfxInstance = this;
        DontDestroyOnLoad(this);
    }
}