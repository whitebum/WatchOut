using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class BGMPitchSlider : AudioPitchSlider
{
    private void Awake()
    {
        slider.wholeNumbers = true;

        slider.minValue = 0.0f;
        slider.maxValue = 100.0f;

        slider.value = SoundManager.GetInstance().bgmSpeaker.pitch;

        slider.onValueChanged.AddListener((value) => { pitchViewer.text = $"{(value)}"; });
        slider.onValueChanged.AddListener((value) => { SoundManager.GetInstance().bgmSpeaker.pitch = value / 100.0f; });
        slider.onValueChanged.AddListener((value) => { SoundManager.GetInstance().PlaySFX("ButtonSelected"); });
    }

    private void OnDestroy()
    {
        SoundManager.GetInstance().bgmSpeaker.pitch = 1.0f;
    }
}
