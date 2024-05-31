using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SliderController : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private  Image sliderimage;

    [SerializeField] private  RectTransform indicatorRotator;


    [SerializeField] private Color startColor;
    [SerializeField] private Color endColor;

    [SerializeField] private AudioClip positive;
    [SerializeField] private AudioClip negative;


    public void UpdateProgress(int value)
    {
        value = Mathf.Clamp(value,0,10);
        slider.value = value;
        indicatorRotator.DOLocalRotate(new Vector3(0.0f,0.0f,Mathf.Lerp(90f,-90f,slider.value / 10)), 0.1f);

    }

    public void AnimateProgress(int value)
    {
        value = Mathf.Clamp(value,0,10);
        slider.DOValue(value, 0.5f, false).SetEase(Ease.InOutQuint);
        indicatorRotator.DOLocalRotate(new Vector3(0.0f,0.0f,Mathf.Lerp(90f,-90f,value / 10f)), 0.5f).SetEase(Ease.InOutQuint);
    }

    public void changeColor()
    {
        sliderimage.color = Color.Lerp(startColor, endColor, slider.value / 10);
    }

    public void PlayNegative()
    {
        GetComponent<AudioSource>().clip = negative;
        GetComponent<AudioSource>().Play();
    }

    public void PlayPositive()
    {
        GetComponent<AudioSource>().clip = positive;
        GetComponent<AudioSource>().Play();
    }
}
