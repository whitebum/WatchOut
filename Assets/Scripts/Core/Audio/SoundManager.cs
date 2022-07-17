using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public sealed class SoundManager : Singleton<SoundManager>
{
    private enum AudioChannel
    {
        BGM,
        SFX,
    }

    private readonly string bgmVolumeKey = "BGM Volume";
    private readonly string sfxVolumeKey = "SFX Volume";

    private readonly string bgmFilePath = "Audio/BGM";
    private readonly string sfxFilePath = "Audio/SFX";

    private readonly Dictionary<AudioChannel, AudioSource> channels = new Dictionary<AudioChannel, AudioSource>();
    private readonly Dictionary<AudioChannel, Dictionary<string, Audio>> audioBank = new Dictionary<AudioChannel, Dictionary<string, Audio>>();

    public AudioSource bgmChannel => channels.ContainsKey(AudioChannel.BGM) ? channels[AudioChannel.BGM] : InitAudioChannel(AudioChannel.BGM);
    public AudioSource sfxChannel => channels.ContainsKey(AudioChannel.SFX) ? channels[AudioChannel.SFX] : InitAudioChannel(AudioChannel.SFX);

    public Dictionary<string, Audio> bgmBank => audioBank.ContainsKey(AudioChannel.BGM) ? audioBank[AudioChannel.BGM] : InitAudioBank(AudioChannel.BGM);
    public Dictionary<string, Audio> sfxBank => audioBank.ContainsKey(AudioChannel.SFX) ? audioBank[AudioChannel.SFX] : InitAudioBank(AudioChannel.SFX);

    public float bgmVolume
    {
        get => PlayerPrefs.HasKey(bgmVolumeKey) ? PlayerPrefs.GetFloat(bgmVolumeKey) : InitAudioVolume(AudioChannel.BGM, 0.5f);
        set => InitAudioVolume(AudioChannel.BGM, value);
    }
    public float sfxVolume
    {
        get => PlayerPrefs.HasKey(sfxVolumeKey) ? PlayerPrefs.GetFloat(sfxVolumeKey) : InitAudioVolume(AudioChannel.SFX, 0.5f);
        set => InitAudioVolume(AudioChannel.SFX, value);
    }

    public BGM curPlayBGM { get; private set; }
    public SFX curPlaySFX { get; private set; }

    private void Reset()
    {
        InitAudioChannel(AudioChannel.BGM);
        InitAudioChannel(AudioChannel.SFX);

        InitAudioBank(AudioChannel.BGM);
        InitAudioBank(AudioChannel.SFX);
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (curPlayBGM)
        {
            if (curPlayBGM.isLoop)
            {
                if (bgmChannel.time >= curPlayBGM.loopEnd)
                    bgmChannel.time = curPlayBGM.loopStart;
            }
        }
    }

    private Dictionary<string, Audio> InitAudioBank(AudioChannel channel)
    {
        if (!audioBank.ContainsKey(channel))
            audioBank.Add(channel, new Dictionary<string, Audio>());

        if (audioBank[channel].Count <= 0)
        {
            var audioFilePath = channel == AudioChannel.BGM ? bgmFilePath : sfxFilePath;

            foreach (var audio in Resources.LoadAll<Audio>(audioFilePath))
                audioBank[channel].Add(audio.audioName, audio);

        }

        Debug.Log($"{channel} 파일 할당 완료!... 개수: {audioBank[channel].Count}");

        return audioBank[channel];
    }

    private AudioSource InitAudioChannel(AudioChannel channel)
    {
        if (!channels.ContainsKey(channel))
        {
            var newChannel = gameObject.AddComponent<AudioSource>();

            newChannel.playOnAwake = false;
            newChannel.volume = channel == AudioChannel.BGM ? bgmVolume : sfxVolume;

            channels.Add(channel, newChannel);
        }

        return channels[channel];
    }

    private float InitAudioVolume(AudioChannel channel, float volume)
    {
        var volumeKey = channel == AudioChannel.BGM ? bgmVolumeKey : sfxVolumeKey;

        PlayerPrefs.SetFloat(volumeKey, volume);

        return PlayerPrefs.GetFloat(volumeKey);
    }

    private void PlayBGM()
    {
        if (bgmChannel.clip)
            bgmChannel.Play();
    }

    private void PlayBGM(BGM bgm)
    {
        if (curPlayBGM != bgm)
        {
            bgmChannel.clip = bgm.audio;
            bgmChannel.Play();

            curPlayBGM = bgm;
        }
    }

    public void PlayBGM(string name)
    {
        if (bgmBank.ContainsKey(name))
            PlayBGM(bgmBank[name] as BGM);
    }

    public void PauseBGM()
    {
        if (bgmChannel.isPlaying)
            bgmChannel.Pause();
    }

    public void StopBGM()
    {
        if (bgmChannel.isPlaying)
            bgmChannel.Stop();
    }

    private void PlaySFX()
    {
        if (sfxChannel.clip)
            sfxChannel.Play();
    }

    private void PlaySFX(SFX sfx)
    {
        if (curPlaySFX != sfx)
        {
            sfxChannel.clip = sfx.audio;
            
            curPlaySFX = sfx;
        }

        sfxChannel.Play();
    }

    private IEnumerator PlaySpecialSFX(SFX sfx)
    {
        PauseBGM();
        
        PlaySFX(sfx);

        yield return new WaitForSeconds(curPlaySFX.audio.length);

        StartCoroutine(FadeInBGM(2.0f));
    }

    public void PlaySFX(string name)
    {
        if (sfxBank.ContainsKey(name))
        {
            var sfx = sfxBank[name] as SFX;

            if (sfx.isSpecial)
                StartCoroutine(PlaySpecialSFX(sfx));

            else
                PlaySFX(sfx);
        }
    }

    public IEnumerator FadeOutBGM(float time)
    {
        if (bgmChannel.isPlaying)
        {
            while (bgmChannel.volume > 0.0f)
            {
                bgmChannel.volume -= Time.deltaTime / time;

                yield return null;
            }

            bgmChannel.volume = 0.0f;

            PauseBGM();
        }

        yield return null;
    }

    public IEnumerator FadeInBGM(float time)
    {
        if (!bgmChannel.isPlaying)
        {
            PlayBGM();

            while (bgmChannel.volume > bgmVolume)
            {
                bgmChannel.volume += Time.deltaTime / time;

                yield return null;
            }

            bgmChannel.volume = bgmVolume;
        }

        yield return null;
    }
}