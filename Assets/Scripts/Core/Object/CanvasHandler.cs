using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using ScaleMode = UnityEngine.UI.CanvasScaler.ScaleMode;
using ScreenMatchMode = UnityEngine.UI.CanvasScaler.ScreenMatchMode;

[RequireComponent(typeof(Canvas))]
[RequireComponent(typeof(CanvasScaler))]
[RequireComponent(typeof(GraphicRaycaster))]
public sealed class CanvasHandler : MonoBehaviour
{
    private readonly RenderMode renderMode = RenderMode.ScreenSpaceCamera;

    private readonly ScaleMode scaleMode = ScaleMode.ScaleWithScreenSize;
    private readonly Vector2 resolution = new Vector2(1920.0f, 1080.0f);
    private readonly ScreenMatchMode screenMatchMode = ScreenMatchMode.MatchWidthOrHeight;
    private readonly float matchWidthOrHeight = 1.0f;

    [SerializeField] private Canvas canvas;
    [SerializeField] private CanvasScaler canvasScaler;

    private bool isAleadyInit = false;

    private void Reset()
    {
        SetProperites();
    }

    private void Awake()
    {
        if (!isAleadyInit)
            SetProperites();
    }

    private void FixedUpdate()
    {
        if (!canvas.worldCamera)
            canvas.worldCamera = Camera.main;
    }

    private void SetProperites()
    {
        TryGetComponent(out canvas);
        TryGetComponent(out canvasScaler);

        canvas.renderMode = renderMode;
        canvas.worldCamera = Camera.main;

        canvasScaler.uiScaleMode = scaleMode;
        canvasScaler.referenceResolution = resolution;
        canvasScaler.screenMatchMode = screenMatchMode;
        canvasScaler.matchWidthOrHeight = matchWidthOrHeight;

        isAleadyInit = true;
    }
}
