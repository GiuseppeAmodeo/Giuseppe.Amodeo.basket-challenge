using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class Pawn : MonoBehaviour
{
    public Ball Ball { get; private set; }

    // Token: 0x040000DB RID: 219
    public float ShootMaxSpread = 5f;

    // Token: 0x040000DC RID: 220
    public float ShootForceCoefficient = 1.5f;

    // Token: 0x040000DD RID: 221
    [SerializeField]
    private Ball ballPrefab;

    private float shootingForceNormalized;

    private float shootingSpread = 0.0f;

    public float Angle = 60f;

    public float MinForce = 4f;
    public float MaxForce = 7f;

    private void Start()
    {
        this.Ball = Instantiate<Ball>(this.ballPrefab);
    }

    /// <summary>
    /// Test the ball shooting mechanics.
    /// </summary>
    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 30), "Swish"))
        {
            ShootSwish();
        }
        if (GUI.Button(new Rect(10, 50, 100, 30), "Bank"))
        {
            ShootBank();
        }
        if (GUI.Button(new Rect(10, 90, 100, 30), "RimHitOrMiss"))
        {
            ShootRimHitOrMiss();
        }
    }

    public void ShootSwish()
    {
        this.Ball.Shoot(GetPerfectForce(Court.Instance.PointEnterHoop.transform), transform.forward);
    }

    public void ShootBank()
    {
        this.Ball.Shoot(GetPerfectForce(Court.Instance.PointBackboard.transform), transform.forward);
    }

    public void ShootRimHitOrMiss()
    {
        this.Ball.Shoot(GetPerfectForce(Court.Instance.PointRim.transform), transform.forward);
    }


    private Vector3 GetPerfectForce(Transform target)
    {
        Vector3 vector = (target.transform.position - this.Ball.transform.position).normalized;

        Transform transform = this.Ball.transform;
        transform.forward = vector;

        Vector3 eulerAngles = transform.rotation.eulerAngles;
        float x = -Angle;

        eulerAngles = new Vector3(x, eulerAngles.y + this.shootingSpread, eulerAngles.z);
        transform.rotation = Quaternion.Euler(eulerAngles);

        vector = transform.forward;

        float y = Physics.gravity.y;
        Vector3 position = this.Ball.transform.position;
        Vector3 position2 = target.transform.position;
        float num = Mathf.Abs(position.y - position2.y);
        position.y = 0f;
        position2.y = 0f;
        float magnitude = (position - position2).magnitude;
        float f = this.Angle * 0.017453292f;
        float num2 = y * magnitude * magnitude;
        float num3 = (num - Mathf.Tan(f) * magnitude) * 2f * Mathf.Cos(f) * Mathf.Cos(f);
        float f2 = num2 / num3;
        float perfectForce = Mathf.Sqrt(f2);

        return perfectForce * vector;
    }

}
