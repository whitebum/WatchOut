using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGMPlaylist : MonoBehaviour
{
    [SerializeField] private List<BGM> bgmList = new List<BGM>();

    [SerializeField] private Dropdown playlist;
    [SerializeField] private Button playButton;
    [SerializeField] private Text timer;

    private void Reset()
    {
        transform.Find("Playlist").TryGetComponent(out playlist);
        transform.Find("Play Button").TryGetComponent(out playButton);
        transform.Find("Timer").TryGetComponent(out timer);

        InitPlaylist();
    }

    private void Awake()
    {
        
    }

    private void InitPlaylist()
    {
        playlist.options.Clear();

        foreach (var item in SoundManager.GetInstance().bgmBank)
            bgmList.Add(item.Value as BGM);

        for (int count = 1; count < bgmList.Count; ++count)
        {
            var option = new Dropdown.OptionData();

            option.text = bgmList[count].audioName;

            playlist.options.Add(option);
        }
    }
}
