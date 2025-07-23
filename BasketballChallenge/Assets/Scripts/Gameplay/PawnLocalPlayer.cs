using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnLocalPlayer : Pawn, IInputReceiver
{
    public event Action<float> ForceChanged;

    public event Action<float> PerfectForceChanged;

    private Vector3 inputOrigin;
    private Vector3 inputEnd;
    private Vector3 previousInputEnd;

    protected override void Start()
    {
        base.Start();
        InputManager.Instance.IsInputEnabled = true;
    }

    public override void Setup()
    {
        base.Setup();
        float minForce = this.currentShootingPoint.MinForce;
        float maxForce = this.currentShootingPoint.MaxForce;    
        float perfectForce = this.currentShootingPoint.PerfectForce;
        float obj =(perfectForce - minForce) / (maxForce - minForce);

        if (this.PerfectForceChanged!=null)
        {
            this.PerfectForceChanged(obj);
        }

        InputManager.Instance.IsInputEnabled = true;
        CameraManager.Instance.EndMove();
        CameraManager.Instance.Translate(this.currentShootingPoint.transform.position);
        CameraManager.Instance.LookAt(Court.Instance.PointEnterHoop);
    }


    public override void Shoot()
    {
        base.Shoot();
        InputManager.Instance.IsInputEnabled = false;
        CameraManager.Instance.BeginMove();
    }


    void IInputReceiver.OnInputDown(Vector3 position)
    {
        Vector3 vector = CameraManager.Instance.ScreenToViewportPoint(position);
        this.inputOrigin = vector;
        this.inputEnd = vector;
    }

    void IInputReceiver.OnInputUp(Vector3 position)
    {
      
    }

    void IInputReceiver.OnInputPressed(Vector3 position)
    {
        this.previousInputEnd = this.inputEnd;
        this.inputEnd = CameraManager.Instance.ScreenToViewportPoint(position);
        Vector3 vector = this.inputEnd - this.previousInputEnd;

        if (vector.y > 0f)
        {
            base.ShootingForceNormalized += vector.y * this.ShootForceCoefficient;
            float num = Mathf.Clamp((this.inputEnd - this.inputOrigin).x, -0.5f, 0.5f) * 2f;
            base.ShootingSpread = num * this.ShootMaxSpread;

            if (this.ForceChanged != null)
            {
                this.ForceChanged(base.ShootingForceNormalized);
            }
        }
    }
}
