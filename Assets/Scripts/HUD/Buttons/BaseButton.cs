using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public abstract class BaseButton : MonoBehaviour
{
    protected enum ButtonTrigger
    {
        OnClick = 0,
        OnPointerEnter = 1,
        OnPointerExit = 2,
    }

    public UnityEvent onClick;
    public UnityEvent onPointerEnter;
    public UnityEvent onPointerExit;

    protected virtual void Reset()
    {
        onClick = new UnityEvent();
        onPointerEnter = new UnityEvent();
        onPointerExit = new UnityEvent();
    }

    protected virtual void Awake()
    {
        onClick = onClick ?? new UnityEvent();
        onPointerEnter = onPointerEnter ?? new UnityEvent();
        onPointerExit = onPointerExit ?? new UnityEvent();
    }
}
