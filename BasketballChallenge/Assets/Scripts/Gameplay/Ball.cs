using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
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
                this.fireEffectsController.Play();
            }
            else
            {
                this.fireEffectsController.Stop();
            }
        }
    }

    public int PowerScoreMultiplier = 2;

    [SerializeField]
    private EffectsController fireEffectsController;

    [SerializeField]
    private EffectsController shootEffectsController;

    [SerializeField]
    private EffectsController ringEffectsController;

    [SerializeField]
    private EffectsController bounceEffectsController;

    [SerializeField]
    private Rigidbody rb;

    private int collisionCount;

    private int layerRing;
    private int layerFloor;
    private int layerBackboard;
    private bool hasCollidedWithBackboard;
    private bool hasTouchedFloor;
    private bool isPowerActive;

    private void Reset()
    {
        this.rb = base.GetComponent<Rigidbody>();
        this.rb.useGravity = false;
        this.rb.mass = 0.65f;
        this.rb.drag = 0.0f;
        this.rb.angularDrag = 0.05f;

        EffectsController[] componentsInChildren = base.GetComponentsInChildren<EffectsController>(true);
        this.fireEffectsController = componentsInChildren.FirstOrDefault((EffectsController hC) => hC.name.Contains("Fire"));
        this.shootEffectsController = componentsInChildren.FirstOrDefault((EffectsController hC) => hC.name.Contains("Shoot"));
        this.ringEffectsController = componentsInChildren.FirstOrDefault((EffectsController hC) => hC.name.Contains("Ring"));
        this.bounceEffectsController = componentsInChildren.FirstOrDefault((EffectsController hC) => hC.name.Contains("Bounce"));
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
            this.ringEffectsController.Play();
        }
        else if (layer == this.layerFloor)
        {
            if (!hasTouchedFloor)
            {
                this.hasTouchedFloor = true;

                if (this.TouchedFloor != null)
                {
                    this.TouchedFloor();
                    this.collisionCount = 0;
                }
            }

            this.hasCollidedWithBackboard = false;
            this.hasTouchedFloor = false;
            this.bounceEffectsController.Play();
        }
        else if (layer == this.layerBackboard)
        {
            this.hasCollidedWithBackboard = true;
            this.bounceEffectsController.Play();
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
        this.shootEffectsController.Play();
        this.rb.useGravity = true;
        this.rb.AddForce(force * this.rb.mass, ForceMode.Impulse);
        this.rb.AddTorque(torque);
    }

    public void Restore(Vector3 position)
    {
        this.collisionCount = 0;
        this.rb.useGravity = false;
        this.rb.velocity = Vector3.zero;
        this.rb.angularVelocity = Vector3.zero;
        base.transform.position = position;
    }
}


