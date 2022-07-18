using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class SFXVolumeSlider : AudioVolumeSlider
{
    private void Awake()
    {
        slider.onValueChanged.AddListener((value) => { SoundManager.GetInstance().sfxVolume = SetVolumeBySlider(value); });
        slider.onValueChanged.AddListener((value) => { UpdateVolumeViewer(value); });
        slider.onValueChanged.AddListener((value) => { SoundManager.GetInstance().PlaySFX("SoundTest"); });

        
    }

    private void Start()
    {
        slider.value = SoundManager.GetInstance().sfxVolume;
    }
}
