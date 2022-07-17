using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Random = UnityEngine.Random;
using SceneCode = SceneManager.SceneCode;

public sealed class MainScene : BaseScene
{
    private enum Time
    {
        DayTime = 0,
        NightTime = 1,
    }

    private readonly Dictionary<Time, byte[]> times = new Dictionary<Time, byte[]>()
    {
         // 아침 6시 ~ 저녁 6시(6~17)시까지는 DayTime(아침~낮).
        { Time.DayTime, new byte[]{ 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17} },
         // 저녁 6시 ~ 아침 6시(18~6)시까지는 NightTime(저녁~밤).
        { Time.NightTime, new byte[]{ 0, 1, 2, 3, 4, 5, 18, 19, 20, 21, 22, 23, } }
    };

    private readonly Dictionary<Time, byte[]> bgmsInTimes = new Dictionary<Time, byte[]>()
    {
        { Time.DayTime, new byte[]{ 0, 1, 2 } },
        { Time.NightTime, new byte[]{ 2, 3, 4 } }
    };

    [SerializeField] private MenuDialog menuDialog;

    [SerializeField] private List<BaseButton> menuButtons;

    protected override void Start()
    {
        SoundManager.GetInstance().PlayBGM($"MainMenu{GetBGMInTimes()}");

        if (menuButtons.Count > 0)
        {
            foreach (MenuButtonByText menuButton in menuButtons)
            {
                menuButton.onPointerEnter.AddListener(() => { menuDialog.SetPrompt(menuButton.menuInfomation); });
            }
        }
    }

    private byte GetBGMInTimes()
    {
        var nowTime = byte.Parse($"{DateTime.Now:hh}");

        // 시간대와 그 시간대에 속하는 시간을 가지고 있는 딕셔너리를 순환.
        // 그리고 그 딕셔너리의 값을 한 번 더 순환하며 해당 값이 현재 시간값과 일치하는지 판단.
        foreach (var time in times)
        {
            foreach (var time2 in time.Value)
            {
                if (nowTime == time2)
                {
                    Debug.Log($"{time.Key}");

                    // 탐색에 성공했을 경우, 해당 시간대에 일치하는 BGM들 중 무작위로 한 곡을 재생한다.
                    var min = bgmsInTimes[time.Key][0];
                    var max = bgmsInTimes[time.Key][bgmsInTimes[time.Key].Length - 1];

                    return (byte)Random.Range(min, max);
                }
            }
        }

        return 0;
    }
}
