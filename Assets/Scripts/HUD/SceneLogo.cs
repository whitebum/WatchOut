using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class SceneLogo : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private Text text;

    private void Reset()
    {
        this.background = transform.Find("Image").TryGetComponent<Image>(out var background) ? background : null;
        this.text = transform.Find("Text").TryGetComponent<Text>(out var text) ? text : null;

        text.text = $"{SceneManager.GetInstance().GetCurrentScene().name}";
    }

    private void Awake()
    {
        if (!background)
            this.background = transform.Find("Image").TryGetComponent<Image>(out var background) ? background : null;

        if (!text)
            this.text = transform.Find("Text").TryGetComponent<Text>(out var text) ? text : null;

        text.text = $"{SceneManager.GetInstance().GetCurrentScene().name}";
    }

    private void OnValidate()
    {
        
    }

    private void Start()
    {
        var animator = TryGetComponent<Animator>(out var cashe) ? cashe : null;

        
    }
}
