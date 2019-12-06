using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioSource music;
    public AudioSource walk;
    public AudioSource clear;
    public AudioSource gameover;

    public void PlayGameover()
    {
        gameover.Play();
    }

    public void PlayWalk(){
        walk.Play();
    }

    public void PlayClear(){
        clear.Play();
    }

    public void StopMusic()
    {
        music.Stop();
    }

    public void PlayMusic()
    {
        music.Play();
    }
}
