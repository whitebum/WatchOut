using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class SceneLogo : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private Text text;

    [SerializeField] private BaseScene curScene;

    private void Reset()
    {
        transform.Find("Image").TryGetComponent(out background);
        transform.Find("Text").TryGetComponent(out text);

        transform.parent.parent.TryGetComponent(out curScene);
    }

    private void Awake()
    {
        text.text = (text && curScene) ? curScene.sceneName : "Unknown";
    }
}
