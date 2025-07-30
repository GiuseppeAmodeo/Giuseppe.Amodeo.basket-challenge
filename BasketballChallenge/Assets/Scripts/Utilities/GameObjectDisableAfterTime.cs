using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameObjectDisableAfterTime : MonoBehaviour
{

    [SerializeField]
    [FormerlySerializedAs("TimeToDisable")]
    private float timeToDisable = 1f;

    private float time;

    private void OnEnable()
    {
        this.time = this.timeToDisable;
    }

    // Update is called once per frame
    void Update()
    {
        this.time -= Time.deltaTime;

        if (this.time <= 0f)
        {
            this.gameObject.SetActive(false);
        }
    }
}
