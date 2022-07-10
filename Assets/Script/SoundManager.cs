using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//by Hisyam - Ref - YT - Epi 24.

//AudioSource will be auto attached to the same game object
[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    private bool _muteBackgroundMusic = false;  //Looping sound on the background.
    private bool _muteSoundFx = false;  //Button sound or when we drag button in game.
    public static SoundManager instance;

    private AudioSource _audioSource;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }


    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.Play();
    }

    public void ToggleBackgroundMusic()
    {
        _muteBackgroundMusic = !_muteBackgroundMusic;
        if(_muteBackgroundMusic)
        {
            _audioSource.Stop();
        }
        else
        {
            _audioSource.Play();
        }
    }

    public void ToggleSoundFX()
    {
        _muteSoundFx = !_muteSoundFx;
        GameEvents.OnToggleSoundFXMethod();
    }

    public bool IsBackgroundMusicMuted()
    {
        return _muteBackgroundMusic;
    }

    public bool IsSoundFXMuted()
    {
        return _muteSoundFx;
    }

    public void SilenceBackgroundMusic(bool Silence)
    {
        if(_muteBackgroundMusic == false)
        {
            if(Silence)
            _audioSource.volume = 0f;
            else
            _audioSource.volume = 1f;
        }
    }
}
