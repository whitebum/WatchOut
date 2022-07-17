using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class TitleScene : BaseScene
{
    private bool isAleadyEnter = false;

    private Animator background;
    private Animator titleLogo;
    private Animator pressStart;

    protected override void Reset()
    {
        var Canvas = transform.Find("Canvas");

        this.background = Canvas.Find("Background").TryGetComponent<Animator>(out var background) ? background : null;
        this.titleLogo = Canvas.Find("Title Logo").TryGetComponent<Animator>(out var titleLogo) ? titleLogo : null;
        this.pressStart = Canvas.Find("Press Start").TryGetComponent<Animator>(out var pressStart) ? pressStart : null;
    }

    protected override void Awake()
    {
        if (!this.background)
            this.background = transform.Find("Canvas").Find("Title Logo").TryGetComponent<Animator>(out var background) ? background : null;

        if (!this.titleLogo)
            this.titleLogo = transform.Find("Canvas").Find("Title Logo").TryGetComponent<Animator>(out var titleLogo) ? titleLogo : null;

        if (!this.pressStart)
            this.pressStart = transform.Find("Canvas").Find("Press Start").TryGetComponent<Animator>(out var pressStart) ? pressStart : null;
    }

    protected override void Start()
    {
        SoundManager.GetInstance().PlayBGM("TitleScreen");
    }

    protected override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!isAleadyEnter)
                StartCoroutine(EnterMainMenu());
        }
    }

    private IEnumerator EnterMainMenu()
    {
        SoundManager.GetInstance().PlaySFX("EnterMenu");

        titleLogo.SetTrigger("Enter Menu");
        pressStart.SetTrigger("Enter Menu");

        yield return new WaitForSeconds(0.5f);

        SceneManager.GetInstance().LoadScene(SceneManager.SceneCode.MainMenu);

        isAleadyEnter = true; 
    }
}
