using System;
using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class Pawn : MonoBehaviour
{
    public delegate void ScoreChangedHandler(int score, ScoreType scoreType);

    public event ScoreChangedHandler ScoreChanged;

    public event Action PawnSetup;
    public event Action PawnShoot;

    public Ball Ball { get; private set; }

    public int Score { get; private set; }

    public bool IsShooting { get; protected set; }

    public bool CanShoot { get; protected set; }

    public float ShootMaxSpread = 5f;

    public float ShootForceCoefficient = 1.5f;

    public float ShootingForceNormalized
    {
        get
        {
            return this.shootingForceNormalized;
        }
        set
        {
            this.shootingForceNormalized = Mathf.Clamp01(value);
        }
    }

    private float shootingForceNormalized;

    public float ShootingSpread
    {
        get
        {
            return this.shootingSpread;
        }
        set
        {
            this.shootingSpread = Mathf.Clamp(value, -this.ShootMaxSpread, this.ShootMaxSpread);
        }
    }


    private float shootingSpread;

    [SerializeField]
    private Ball ballPrefab;

    protected ShootingPoint currentShootingPoint;

    protected virtual void Start()
    {
        this.Ball = Instantiate<Ball>(this.ballPrefab);
        this.Ball.EnteredBasket += this.OnBallEnteredBasket;
        GameManager.CurrentMatch.Ended += this.OnCurrentMatchEnded;
        this.CanShoot = true;
    }

    private void OnDestroy()
    {
        this.Ball.EnteredBasket -= this.OnBallEnteredBasket;
        GameManager.CurrentMatch.Ended -= this.OnCurrentMatchEnded;
    }

    protected virtual void OnBallEnteredBasket(ScoreType scoreType)
    {
        int num = (int)scoreType;

        this.AddScore(num, scoreType);
    }

    private void OnCurrentMatchEnded()
    {
        base.StartCoroutine(this.WaitForPawnStopShooting());
    }

    private IEnumerator WaitForPawnStopShooting()
    {
        while (this.IsShooting)
        {
            yield return null;
        }

        this.Ball.gameObject.SetActive(false);
        base.gameObject.SetActive(false);
        yield break;
    }

    public virtual void Setup()
    {
        this.IsShooting = false;

        if (this.currentShootingPoint != null)
        {
            this.currentShootingPoint.IsBusy = false;
        }

        this.shootingForceNormalized = 0f;
        this.currentShootingPoint = Court.Instance.GetFreeRandomShootingPoint();
        this.currentShootingPoint.IsBusy = true;
        Vector3 normalized = (Court.Instance.PointEnterHoop.position - this.currentShootingPoint.transform.position).normalized;
        this.currentShootingPoint.transform.rotation = Quaternion.LookRotation(normalized);
        this.Ball.Restore(this.currentShootingPoint.BallLocator.position);

        if (this.PawnSetup != null)
        {
            this.PawnSetup();
        }
    }

    public void AddScore(int score, ScoreType scoreType)
    {
        this.Score += score;
        Debug.Log($"Score changed: {this.Score} ");

        if (this.ScoreChanged != null)
        {
            // Notify subscribers about the score change
            this.ScoreChanged(this.Score, scoreType);
        }
    }

    public void ResetScore()
    {
        this.Score = 0;
    }

    public virtual void Shoot()
    {
        this.IsShooting = true;
        Vector3 vector = (Court.Instance.PointEnterHoop.position - this.Ball.transform.position).normalized;
        ShootingPoint shootingPoint = this.currentShootingPoint;
        Transform transform = shootingPoint.transform;
        transform.forward = vector;
        float x = -shootingPoint.Angle;
        Vector3 eulerAngles = transform.rotation.eulerAngles;
        eulerAngles = new Vector3(x, eulerAngles.y + this.shootingSpread, eulerAngles.z);
        transform.rotation = Quaternion.Euler(eulerAngles);
        float minForce = this.currentShootingPoint.MinForce;
        float maxForce = this.currentShootingPoint.MaxForce;
        float d = (maxForce - minForce) * this.shootingForceNormalized + minForce;
        vector = transform.forward;
        this.Ball.Shoot(vector * d, transform.forward);

        if (this.PawnShoot != null)
        {
            this.PawnShoot();
        }

    }


}
