using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class BGMDebugUtility : MonoBehaviour
{
    [SerializeField] private Toggle useDefaultLoop;
    [SerializeField] private Toggle useInGameLoop;
    [SerializeField] private Button audioFadeEffect;

    private void Reset()
    {
        transform.Find("Use Defalut Loop").TryGetComponent(out useDefaultLoop);
        transform.Find("Use InGame Loop").TryGetComponent(out useInGameLoop);
        transform.Find("Fade Audio Volume").TryGetComponent(out audioFadeEffect);
    }

    private void Start()
    {
        useDefaultLoop.onValueChanged.AddListener((value) => { SoundManager.GetInstance().isDebugging = SoundManager.GetInstance().bgmSpeaker.loop = value; });
        useDefaultLoop.onValueChanged.AddListener((value) => { useInGameLoop.isOn = !value; });

        useInGameLoop.onValueChanged.AddListener((value) => { SoundManager.GetInstance().isDebugging = SoundManager.GetInstance().bgmSpeaker.loop = !value; });
        useInGameLoop.onValueChanged.AddListener((value) => { useDefaultLoop.isOn = !value; });

        audioFadeEffect.onClick.AddListener(() => { StartCoroutine(FadeAudioVolume()); });

        useDefaultLoop.isOn = SoundManager.GetInstance().isDebugging;
        useInGameLoop.isOn = !SoundManager.GetInstance().isDebugging;
    }

    private IEnumerator FadeAudioVolume()
    {
        if (SoundManager.GetInstance().bgmVolume == 0.0f)
            yield return null;



        yield return null;
    }
}
