using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventTrigger))]
public class TempButton : MonoBehaviour
{
    private void Awake()
    {
        Button buton;
    }

    void Start()
    {
        TryGetComponent<EventTrigger>(out var trigger);

        var onClick = new EventTrigger.Entry();
        var onPointerExit = new EventTrigger.Entry();
        var onPointerEnter = new EventTrigger.Entry();

        onClick.eventID = EventTriggerType.PointerClick;
        onClick.callback.AddListener((data) => { OnClick(data as PointerEventData); });

        onPointerEnter.eventID = EventTriggerType.PointerEnter;
        onPointerEnter.callback.AddListener((data) => { OnPointerEnter(data as PointerEventData); });

        onPointerExit.eventID = EventTriggerType.PointerExit;
        onPointerExit.callback.AddListener((data) => { OnPointerExit(data as PointerEventData); });

        trigger.triggers.Add(onPointerEnter);

        trigger.triggers.Add(onClick);

        trigger.triggers.Add(onPointerExit);
    }
    
    private void OnClick(PointerEventData data)
    {
        Debug.Log("Click");
    }

    private void OnPointerEnter(PointerEventData data)
    {
        Debug.Log("Enter");
    }

    private void OnPointerExit(PointerEventData data)
    {
        Debug.Log("Exit");
    }
}
