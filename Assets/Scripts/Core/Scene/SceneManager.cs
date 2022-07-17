using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using LegacySceneMng = UnityEngine.SceneManagement.SceneManager;

public sealed class SceneManager : Singleton<SceneManager>
{
    public enum SceneCode
    {
        MainMenu = 0,
        HowToGame = 1,
        HallOfFame = 2,
        SettingConfig = 3,
        SoundTest = 4,
        GameStage = 5,
    }

    [SerializeField] private CanvasHandler canvas;

    [SerializeField] private Image background;
    [SerializeField] private Image loading;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (!canvas)
        {
            var findCanvas = transform.Find("Canvas");

            if (findCanvas)
            {
                if (!findCanvas.TryGetComponent(out canvas))
                    canvas = findCanvas.gameObject.AddComponent<CanvasHandler>();
            }

            else
                canvas = Instantiate(Resources.Load<CanvasHandler>("Prefabs/Canvas"), transform);
        }

        if (!background)
        {
            var findBackground = canvas.transform.Find("Background");

            if (findBackground)
            {
                if (!findBackground.TryGetComponent(out background))
                {
                    background = findBackground.gameObject.AddComponent<Image>();
                    background.color = Color.black;
                }
            }

            else
                background = Instantiate(Resources.Load<Image>("Prefabs/Background"), canvas.transform);


            background.color = background.color.r > 0.0f ? new Color(background.color.r, background.color.g, background.color.b, 0.0f) : background.color;
        }
    }

    public void LoadScene(string name)
    {
        StartCoroutine(SceneTranslate(name));
    }

    public void LoadScene(SceneCode code)
    {
        StartCoroutine(SceneTranslate(code));
    }

    public Scene GetCurrentScene()
    {
        return LegacySceneMng.GetActiveScene();
    }

    private IEnumerator FadeInBackground()
    {
        if (!background.gameObject.activeSelf)
            background.gameObject.SetActive(true);

        var r = background.color.r;
        var g = background.color.g;
        var b = background.color.b;
        var a = background.color.a;

        while (background.color.a < 1.0f)
        {
            background.color = new Color(r, g, b, a += Time.deltaTime / 1.0f);

            yield return null;
        }

        background.color = new Color(r, g, b, 1.0f);

        yield return null;
    }

    private IEnumerator FadeOutBackground()
    {
        if (!background.gameObject.activeSelf)
            background.gameObject.SetActive(true);

        var r = background.color.r;
        var g = background.color.g;
        var b = background.color.b;
        var a = background.color.a;

        while (background.color.a > 0.0f)
        {
            background.color = new Color(r, g, b, a -= Time.deltaTime / 1.0f);

            yield return null;
        }

        background.color = new Color(r, g, b, 0.0f);
        background.gameObject.SetActive(false);

        yield return null;
    }

    private IEnumerator SceneTranslate(string name)
    {
        SoundManager.GetInstance().StopBGM();

        background.gameObject.SetActive(true);

        yield return StartCoroutine(FadeInBackground());

        var load = LegacySceneMng.LoadSceneAsync(name);

        while(load.isDone)
        {
            Debug.Log($"{load.progress * 100.0f: 0.0%}");

            yield return null;
        }

        yield return StartCoroutine(FadeOutBackground());

        background.gameObject.SetActive(false);
        yield return null;
    }

    private IEnumerator SceneTranslate(SceneCode code)
    {
        SoundManager.GetInstance().StopBGM();

        background.gameObject.SetActive(true);

        yield return StartCoroutine(FadeInBackground());

        var load = LegacySceneMng.LoadSceneAsync((int)code);

        while (load.isDone)
        {
            Debug.Log($"{load.progress * 100.0f: 0.0%}");

            yield return null;
        }

        yield return StartCoroutine(FadeOutBackground());

        background.gameObject.SetActive(false);
        yield return null;
    }
}
