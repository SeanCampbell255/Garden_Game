using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioSource music;
    public AudioSource walk;
    public AudioSource clear;

    public void PlayWalk(){
        walk.Play();
    }

    public void PlayClear(){
        clear.Play();
    }
}
