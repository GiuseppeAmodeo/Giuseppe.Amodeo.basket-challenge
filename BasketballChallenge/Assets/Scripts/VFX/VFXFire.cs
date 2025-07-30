using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXFire : MonoBehaviour, IEffect
{
    public ParticleSystem ParticleSystem;

    private Vector3 euler = new Vector3(-90.0f, 0.0f, 0.0f);

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
