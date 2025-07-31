using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXBasket : MonoBehaviour, IEffect
{
    public ParticleSystem ParticleSystem;

    private void Reset()
    {
        this.ParticleSystem = base.GetComponent<ParticleSystem>();
    }

    void IEffect.Play()
    {
        this.ParticleSystem.Play();
    }

    void IEffect.Stop()
    {
        this.ParticleSystem.Stop();
    }
}
