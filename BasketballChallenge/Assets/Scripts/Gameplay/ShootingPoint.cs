using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShootingPoint : MonoBehaviour
{

    public float MinForce
    {
        get
        {
            return 4f;
        }
    }

    public float MaxForce
    {
        get
        {
            return 7f;
        }
    }

    public float PerfectForce { get; private set; }

    public bool IsBusy { get; set; }

    public float Angle = 60f;
   

    // Start is called before the first frame update
    private void Start()
    {
        float y = Physics.gravity.y;
        Vector3 position= this.transform.position;
        Vector3 position2 = Court.Instance.PointEnterHoop.position;
        float num = Mathf.Abs(position.y - position2.y);
        position.y = 0f;
        position2.y = 0f;
        float magnitude = (position - position2).magnitude;
        float f = this.Angle * 0.017453292f;
        float num2 = y * magnitude * magnitude;
        float num3 = (num - Mathf.Tan(f) * magnitude) *2f * Mathf.Cos(f) * Mathf.Cos(f);
        float f2 = num2 / num3;
        float perfectForce = Mathf.Sqrt(f2);
        this.PerfectForce = perfectForce;
    }


}
