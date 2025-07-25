using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Unity.VisualScripting.Antlr3.Runtime.Tree;

public class Ball : MonoBehaviour
{
    public event Action TouchedFloor;
    public event Action<ScoreType> EnteredBasket;

    public bool IsPowerActive
    {
        get
        {
            return this.isPowerActive;
        }
        set
        {
            this.isPowerActive = value;

            if (value)
            {
                //On effectVFX
            }
            else
            {
                //Off effectVFX
            }
        }
    }

    public int PowerScoreMultiplier = 2;

    [SerializeField]
    private Rigidbody rb;

    private int collisionCount;

    private int layerRing;
    private int layerFloor;
    private int layerBackboard;
    private bool hasCollidedWithBackboard;
    private bool isPowerActive;

    private void Reset()
    {
        this.rb = base.GetComponent<Rigidbody>();
        this.rb.useGravity = false;
        this.rb.mass = 0.65f;
        this.rb.drag = 0.0f;
        this.rb.angularDrag = 0.05f;
    }

    private void Awake()
    {
        this.layerFloor = LayerMask.NameToLayer("Floor");
        this.layerRing = LayerMask.NameToLayer("Ring");
        this.layerBackboard = LayerMask.NameToLayer("Backboard");
    }

    private void FixedUpdate()
    {
        if (this.rb.IsSleeping())
        {
            this.rb.AddTorque(Vector3.forward * Physics.sleepThreshold);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        this.collisionCount++;

        int layer = collision.gameObject.layer;

        if (layer == this.layerRing)
        {
            //Hit Ring.
            Debug.Log("The ball touched the Ring");
        }
        else if (layer == this.layerFloor)
        {
            if (this.TouchedFloor != null)
            {
                this.TouchedFloor();
                this.collisionCount = 0;
            }

            this.hasCollidedWithBackboard = false;
        }
        else if (layer == this.layerBackboard)
        {
            this.hasCollidedWithBackboard = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (this.EnteredBasket != null)
        {
            if (this.collisionCount == 0)
            {
                if (this.EnteredBasket != null)
                {
                    this.EnteredBasket(ScoreType.PerfectScore);
                }
            }
            else
            {
                this.EnteredBasket((!this.hasCollidedWithBackboard) ? ScoreType.SimpleScore : Court.Instance.Backboard.CurrentBackboardScore);
            }
        }

        this.hasCollidedWithBackboard = false;
    }

    public void Shoot(Vector3 force, Vector3 torque)
    {
        this.rb.useGravity = true;
        this.rb.AddForce(force * this.rb.mass, ForceMode.Impulse);
        this.rb.AddTorque(torque);
    }

    public void Restore(Vector3 position)
    {
        this.rb.useGravity = false;
        this.rb.velocity = Vector3.zero;
        this.rb.angularVelocity = Vector3.zero;
        base.transform.position = position;
    }
}


