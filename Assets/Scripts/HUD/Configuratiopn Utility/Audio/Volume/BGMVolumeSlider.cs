using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class BGMVolumeSlider : AudioVolumeSlider
{
    private void Awake()
    {
        slider.onValueChanged.AddListener((value) => { SoundManager.GetInstance().bgmVolume = SetVolumeBySlider(value); });
        slider.onValueChanged.AddListener((value) => { UpdateVolumeViewer(value); });
        slider.onValueChanged.AddListener((value) => { SoundManager.GetInstance().PlayBGMOneShot("SoundTest"); });
    }

    private void Start()
    {
        slider.value = SoundManager.GetInstance().bgmVolume;
    }
}
