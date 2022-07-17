using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public sealed class MenuButtonByText : BaseButton, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image background;
    [SerializeField] private Text buttonLogo;
    [SerializeField] private Animator anim;

    [Space(5.0f)]
    [SerializeField] private SceneManager.SceneCode sceneCode;

    [Space(5.0f)]
    [SerializeField] private Sprite backgroundSprite;

    [Space(5.0f)]
    public string buttonLogoText = "새로운 메뉴";
    public string menuInfomation = "새로운 메뉴 버튼입니다.";

    public Navigation navigation;

    protected override void Reset()
    {
        base.Reset();

        background = transform.Find("Background").TryGetComponent(out background) ? background : throw new NullReferenceException("???");
        buttonLogo = transform.Find("Button Logo").TryGetComponent(out buttonLogo) ? buttonLogo : throw new NullReferenceException("???");
        anim = TryGetComponent(out anim) ? anim : throw new NullReferenceException("???");
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
            if (buttonLogo.text != buttonLogoText)
                buttonLogo.text = buttonLogoText;
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        SoundManager.GetInstance().PlaySFX("ButtonClicked");

        anim.SetTrigger("OnClick");

        SceneManager.GetInstance().LoadScene(sceneCode);

        onClick.Invoke();
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        SoundManager.GetInstance().PlaySFX("ButtonSelected");

        anim.SetTrigger("OnPointerEnter");

        onPointerEnter.Invoke();
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        anim.SetTrigger("OnPointerExit");

        onPointerExit.Invoke();
    }
}
