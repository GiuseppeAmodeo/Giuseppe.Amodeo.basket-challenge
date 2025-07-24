using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GUIBar : MonoBehaviour
{
    public Slider CurrentValueSlider;

    public Slider ThresholdValueSlider;

    private void Reset()
    {
        RectTransform[] componentsInChildren = base.GetComponentsInChildren<RectTransform>();

        this.CurrentValueSlider = (from hR in componentsInChildren
                                   where hR.name.Contains("Current")
                                   select hR).FirstOrDefault<RectTransform>().GetComponent<Slider>();

        this.ThresholdValueSlider = (from hR in componentsInChildren
                                     where hR.name.Contains("Threshold")
                                     select hR).FirstOrDefault<RectTransform>().GetComponent<Slider>();
    }

    public void SetCurrentForce(float value)
    {
        this.CurrentValueSlider.value = value;
    }

    public void SetPerfectForce(float value)
    {
        this.ThresholdValueSlider.value = value;
    }
}
