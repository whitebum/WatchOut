using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public sealed class SoundManager : Singleton<SoundManager>
{
    private enum AudioType
    {
        BGM,
        SFX,
    }

    public bool isDebugging = false;

    private readonly string bgmVolumeKey = "BGM Volume";
    private readonly string sfxVolumeKey = "SFX Volume";

    private readonly string bgmFilePath = "Audio/BGM";
    private readonly string sfxFilePath = "Audio/SFX";

    private readonly Dictionary<AudioType, AudioSource> speakers = new Dictionary<AudioType, AudioSource>();
    private readonly Dictionary<AudioType, Dictionary<string, Audio>> audioBank = new Dictionary<AudioType, Dictionary<string, Audio>>();

    public AudioSource bgmSpeaker
    {
        get
        {
            if (!speakers.ContainsKey(AudioType.BGM))
                SetAudioSpeaker(AudioType.BGM);

            return speakers[AudioType.BGM];
        }
    }

    public AudioSource sfxSpeaker
    {
        get
        {
            if (!speakers.ContainsKey(AudioType.SFX))
                SetAudioSpeaker(AudioType.SFX);

            return speakers[AudioType.SFX];
        }
    }

    public Dictionary<string, Audio> bgmBank
    {
        get
        {
            if (!audioBank.ContainsKey(AudioType.BGM))
            {
                audioBank.Add(AudioType.BGM, new Dictionary<string, Audio>());

                foreach (var audio in Resources.LoadAll<Audio>(bgmFilePath))
                    audioBank[AudioType.BGM].Add(audio.audioName, audio);
            }

            return audioBank[AudioType.BGM];
        }
    }

    public Dictionary<string, Audio> sfxBank
    {
        get
        {
            if (!audioBank.ContainsKey(AudioType.SFX))
            {
                audioBank.Add(AudioType.SFX, new Dictionary<string, Audio>());

                foreach (var audio in Resources.LoadAll<Audio>(sfxFilePath))
                    audioBank[AudioType.SFX].Add(audio.audioName, audio);
            }

            return audioBank[AudioType.SFX];
        }
    }

    public float bgmVolume
    {
        get => bgmSpeaker.volume;
        set => bgmSpeaker.volume = value;
    }

    public float sfxVolume
    {
        get => sfxSpeaker.volume;
        set => sfxSpeaker.volume = value;
    }

    public BGM curPlayBGM { get; private set; }
    public SFX curPlaySFX { get; private set; }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        for (var type = AudioType.BGM; type >= AudioType.SFX; ++type)
        {
            SetAudioSpeaker(type);
            SetAudioBank(type);
        }
    }

    private void Update()
    {
        if (!isDebugging)
        {
            if (curPlayBGM)
            {
                if (curPlayBGM.isLoop)
                {
                    if (bgmSpeaker.time >= curPlayBGM.loopEnd)
                        bgmSpeaker.time = curPlayBGM.loopStart;
                }
            }
        }
    }

    protected override void OnApplicationQuit()
    {
        base.OnApplicationQuit();

        for (var type = AudioType.BGM; type >= AudioType.SFX; ++type)
            SaveAudioVolume(type);
    }

    private void SetAudioSpeaker(AudioType type)
    {
        if (!speakers.ContainsKey(type))
        {
            speakers.Add(type, gameObject.AddComponent<AudioSource>());

            speakers[type].volume = SetAudioVolume(type);
            speakers[type].playOnAwake = false;
        }
    }

    private void SetAudioBank(AudioType type)
    {
        if (!audioBank.ContainsKey(type))
        {
            var path = type == AudioType.BGM ? bgmFilePath : sfxFilePath;

            audioBank.Add(type, new Dictionary<string, Audio>());

            foreach (var audio in Resources.LoadAll<Audio>(path))
                audioBank[type].Add(audio.audioName, audio);
        }
    }

    private float SetAudioVolume(AudioType type)
    {
        var key = type == AudioType.BGM ? bgmVolumeKey : sfxVolumeKey;

        if (PlayerPrefs.HasKey(key))
            PlayerPrefs.GetFloat(key);

        return 0.5f;
    }

    private void SaveAudioVolume(AudioType type)
    {
        var key = type == AudioType.BGM ? bgmVolumeKey : sfxVolumeKey;
        var volume = type == AudioType.BGM ? bgmSpeaker.volume : sfxSpeaker.volume;

        PlayerPrefs.SetFloat(key, volume);
    }

    public void PlayBGM(string name)
    {
        if (bgmBank.ContainsKey(name))
        {
            StopBGM();

            var bgm = curPlayBGM = bgmBank[name] as BGM;

            bgmSpeaker.clip = bgm.audio;
            bgmSpeaker.Play();
        }
    }

    public void PlayBGM(AudioSource audio)
    {
        foreach (var elem in bgmBank.Values)
        {
            if (elem.audio == audio)
            {
                StopBGM();

                var bgm = curPlayBGM = bgmBank[name] as BGM;

                bgmSpeaker.clip = bgm.audio;
                bgmSpeaker.Play();

                break;
            }
        }
    }

    public void PlayBGM(BGM audio)
    {
        foreach (var elem in bgmBank.Values)
        {
            if ((elem.audio == audio.audio) && (elem.audioName == audio.audioName))
            {
                StopBGM();

                var bgm = curPlayBGM = bgmBank[name] as BGM;

                bgmSpeaker.clip = bgm.audio;
                bgmSpeaker.Play();

                break;
            }
        }
    }

    public void PlayBGMOneShot(string name)
    {
        if (bgmBank.ContainsKey(name))
        {
            StopBGM();

            bgmSpeaker.PlayOneShot((curPlayBGM = bgmBank[name] as BGM).audio);
        }
    }

    public void PauseBGM()
    {
        if (bgmSpeaker.isPlaying)
            bgmSpeaker.Pause();
    }

    public void StopBGM()
    {
        if (bgmSpeaker.isPlaying)
            bgmSpeaker.Stop();
    }

    public void PlaySFX(string name)
    {
        if (sfxBank.ContainsKey(name))
        {
            StopSFX();

            var sfx = curPlaySFX = sfxBank[name] as SFX;

            sfxSpeaker.clip = sfx.audio;
            sfxSpeaker.Play();
        }
    }

    public void PlaySFX(AudioSource audio)
    {
        foreach (var elem in sfxBank.Values)
        {
            if (elem.audio == audio)
            {
                StopSFX();

                var sfx = curPlaySFX = sfxBank[name] as SFX;

                sfxSpeaker.clip = sfx.audio;
                sfxSpeaker.Play();

                break;
            }
        }
    }

    public void PlaySFX(SFX audio)
    {
        foreach (var elem in sfxBank.Values)
        {
            if ((elem.audio == audio.audio) && (elem.audioName == audio.audioName))
            {
                StopSFX();

                var sfx = curPlaySFX = sfxBank[name] as SFX;

                sfxSpeaker.clip = sfx.audio;
                sfxSpeaker.Play();

                break;
            }
        }
    }

    public void StopSFX()
    {
        if (sfxSpeaker.isPlaying)
            sfxSpeaker.Stop();
    }
}