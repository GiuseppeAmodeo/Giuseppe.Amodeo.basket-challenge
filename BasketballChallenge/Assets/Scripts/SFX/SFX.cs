using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour, IEffect
{

    public AudioSource AudioSource;

    private void Reset()
    {
        this.AudioSource = base.GetComponent<AudioSource>();
    }

    void IEffect.Play()
    {
        this.AudioSource.Play();
    }

    void IEffect.Stop()
    {
        this.AudioSource.Stop();
    }
}
