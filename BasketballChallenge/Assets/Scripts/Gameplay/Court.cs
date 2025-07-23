using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Court : MonoBehaviour
{
    public static Court Instance { get; private set; }

    public Transform PointEnterHoop;
    public Transform PointBackboard;
    public Transform PointRim;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(base.gameObject);
        }

        Instance = this;
    }
}
