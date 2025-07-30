using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public event Action EnteredBasket;

    private void Start()
    {
        EnteredBasket += OnBallEnteredBasket;
    }

    private void OnDestroy()
    {
        EnteredBasket -= OnBallEnteredBasket;
    }

    private void OnBallEnteredBasket()
    {
       Debug.Log("Ball has entered the basket!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (this.EnteredBasket != null)
        {
            this.EnteredBasket();
        }
    }
}
