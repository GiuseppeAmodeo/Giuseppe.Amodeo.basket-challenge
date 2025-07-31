using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour
{
    [SerializeField]
    private EffectsController basketEffectsController;

    private void Reset()
    {
        this.basketEffectsController = base.GetComponentInChildren<EffectsController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        this.basketEffectsController.Play();
    }

}
