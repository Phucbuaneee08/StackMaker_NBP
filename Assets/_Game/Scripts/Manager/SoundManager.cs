using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


public class SoundManager : Singleton<SoundManager>
{

    public AudioClip soundAddBrick;
    public AudioClip soundRemoveBrick;
    public AudioClip soundWin;
    public AudioClip soundLose;
    public AudioClip soundInGame;

    public AudioSource audioSource;
    // Use this for initialization
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void PlaySound(Sound currentSound)
    {
        switch (currentSound)
        {
            case Sound.AddBrick:
                {
                    audioSource.PlayOneShot(soundAddBrick);
                }
                break;
            case Sound.RemoveBrick:
                {
                    audioSource.PlayOneShot(soundRemoveBrick);
                    //Invoke("PlaySoundDie", 0.3f);
                }
                break;
            case Sound.Win:
                {
                    audioSource.PlayOneShot(soundWin);
                }
                break;
            case Sound.Lose:
                {
                    audioSource.PlayOneShot(soundLose);
                }
                break;
            case Sound.InGame:
                {
                    audioSource.PlayOneShot(soundInGame);
                }
                break;
        }
    }
    public void StopSound()
    {
        audioSource.Stop();
    }
    //private void PlaySoundDie()
    //{
    //    PlaySound(Sound.die);
    //}

}

