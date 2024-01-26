using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SliderController : MonoBehaviour
{
        public Slider slider;

    public void UpdateProgress(int value)
    {
        value = Mathf.Clamp(value,0,10);
        slider.value = value;
    }

    public void AnimateProgress(int value)
    {
        value = Mathf.Clamp(value,0,10);
        slider.DOValue(value, 0.5f, false);
    }
}
