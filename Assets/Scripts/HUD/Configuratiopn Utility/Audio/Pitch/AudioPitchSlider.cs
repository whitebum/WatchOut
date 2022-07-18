using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class AudioPitchSlider : MonoBehaviour
{
    [SerializeField] protected Slider slider;
    [SerializeField] protected Text pitchViewer;
}
