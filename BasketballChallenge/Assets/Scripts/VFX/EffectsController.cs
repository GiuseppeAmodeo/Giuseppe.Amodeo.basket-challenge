using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsController : MonoBehaviour
{
    private IEffect[] effects;

    private void Awake()
    {
        this.effects = base.GetComponentsInChildren<IEffect>();
    }

    public void Play()
    {
        for (int i = 0; i < this.effects.Length; i++)
        {
            this.effects[i].Play();
        }
    }

    public void Stop()
    {
        for (int i = 0; i < this.effects.Length; i++)
        {
            this.effects[i].Stop();
        }
    }
}
