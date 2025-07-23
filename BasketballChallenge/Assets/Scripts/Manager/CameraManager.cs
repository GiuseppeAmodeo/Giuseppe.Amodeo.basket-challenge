using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get; private set; }

    public Camera Camera;

    private void Reset()
    {
        this.Camera = base.GetComponent<Camera>();
    }

    private void Awake()
    {
        if (CameraManager.Instance!=null)
        {
            Destroy(base.gameObject);
        }

        CameraManager.Instance = this;
    }

   public Vector3 ScreenToViewportPoint(Vector3 position)
    {
        return this.Camera.ScreenToViewportPoint(position);
    }
}
