using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControl : MonoBehaviour
{
    public AudioClip[] music;

    public AudioSource[] source;

    void Start()
    {
        source[0].clip = music[0];
        source[0].Play(); //ambient
    }

    public void FlashLight_turn_On()
    {
        source[1].clip = music[1];
        source[1].Play();
    }
    public void FlashLight_turn_Off()
    {
        source[1].clip = music[2];
        source[1].Play();
    }
    public void FootStep()
    {
        source[2].clip = music[3];
        source[2].Play();
    }
}
