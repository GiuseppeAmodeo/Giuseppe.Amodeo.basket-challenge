using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarBounce : MonoBehaviour
{
    private void OnEnable()
    {
        transform.localScale = Vector3.zero;

        LeanTween.scale(gameObject, Vector3.one, 0.5f)
            .setEase(LeanTweenType.easeOutElastic)
            .setOnComplete(() => {
            });
    }
}
