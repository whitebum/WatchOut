using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class AudioVolumeSlider : MonoBehaviour
{
    [SerializeField] protected Slider slider;
    [SerializeField] protected Text volumeViewer;

    protected float SetVolumeBySlider(float value) => value / 100.0f;
    protected void UpdateVolumeViewer(float value) => volumeViewer.text = $"{value}";

    private void Reset()
    {
        transform.Find("Slider").TryGetComponent(out slider);
        transform.Find("Volume Amount").TryGetComponent(out volumeViewer);

        slider.wholeNumbers = true;

        slider.minValue = 0.0f;
        slider.maxValue = 100.0f;
    }
}
