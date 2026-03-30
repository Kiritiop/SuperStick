using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour 
{

    public static SoundEffectManager instance;

    [SerializeField] private AudioSource soundFxObject;
    
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

    }

    public void PlaySoundEffect(AudioClip audioclip, Transform spawntransform, float volume)
    {
        AudioSource audiosource = Instantiate(soundFxObject, spawntransform.position, Quaternion.identity);
        audiosource.clip = audioclip;
        audiosource.volume = volume;
        audiosource.Play();
        float cliplength = audiosource.clip.length;
        Destroy(audiosource.gameObject, cliplength);
        
    }

}
