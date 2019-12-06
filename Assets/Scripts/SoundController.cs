using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioSource music;
    public AudioSource walk;
    public AudioSource gameover;
    public AudioSource pull;
    public AudioSource push;
    public AudioSource bonk;
    public AudioSource select;

    public AudioSource[] comboArray;

    public void PlayCombo(int comboNum)
    {
        comboArray[comboNum].Play();
    }

    public void PlayPull()
    {
        pull.Play();
    }

    public void PlayPush()
    {
        push.Play();
    }

    public void PlayBonk()
    {
        bonk.Play();
    }

    public void PlaySelect()
    {
        select.Play();
    }

    public void PlayGameover()
    {
        gameover.Play();
    }

    public void PlayWalk(){
        walk.Play();
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
