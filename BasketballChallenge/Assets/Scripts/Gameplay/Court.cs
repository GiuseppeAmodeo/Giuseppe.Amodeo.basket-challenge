using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Court : MonoBehaviour
{
    public static Court Instance { get; private set; }

    public Transform PointEnterHoop;
    public Transform PointBackboard;
    public Transform PointRim;

    public Backboard Backboard;

    public List<ShootingPoint> ShootingPoints;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(base.gameObject);
        }

        Instance = this;
    }


    //Only one shooting point
    public ShootingPoint GetFreeRandomShootingPoint()
    {
        ShootingPoint shootingPoint;
        
        do
        {
            int index = Random.Range(0,ShootingPoints.Count);
            shootingPoint = ShootingPoints[index];
        }
        while (shootingPoint.IsBusy);
        return shootingPoint;
    }
}
