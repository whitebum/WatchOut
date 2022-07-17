using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public sealed class MenuButtonByImage : BaseButton, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image background;
    [SerializeField] private Image buttonLogo;

    [Space(5.0f)]
    [SerializeField] private Sprite backgroundSprite;
    [SerializeField] private Sprite buttonLogoSprite;

    [Space(5.0f)]
    [SerializeField] private SceneManager.SceneCode sceneCode;

    [Space(5.0f)]
    public string menuInfomation = "일반 메뉴 버튼입니다.";

    protected override void Reset()
    {
        base.Reset();

        background = transform.Find("Background").TryGetComponent(out background) ? background : throw new NullReferenceException();
        buttonLogo = transform.Find("Button Logo").TryGetComponent(out buttonLogo) ? buttonLogo : throw new NullReferenceException();
    }

    protected override void Awake()
    {
        base.Awake();

        background = background ? background : transform.Find("Background").TryGetComponent(out background) ? background : throw new NullReferenceException();
        buttonLogo = buttonLogo ? buttonLogo : transform.Find("Button Logo").TryGetComponent(out buttonLogo) ? buttonLogo : throw new NullReferenceException();
    }

    private void OnValidate()
    {
        if (background)
            if (background.sprite != backgroundSprite)
                background.sprite = backgroundSprite;

        if (buttonLogo)
            if (buttonLogo.sprite != buttonLogoSprite)
                buttonLogo.sprite = buttonLogoSprite;
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        SoundManager.GetInstance().PlaySFX("ButtonClicked");

        GetComponent<Animator>().SetTrigger("OnClick");

        SceneManager.GetInstance().LoadScene(sceneCode);

        onClick.Invoke();
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        SoundManager.GetInstance().PlaySFX("ButtonSelected");

        GetComponent<Animator>().SetTrigger("OnPointerEnter");
        
        onPointerEnter.Invoke();
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Animator>().SetTrigger("OnPointerExit");

        onPointerExit.Invoke();
    }
}
