using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public sealed class RankingManager : Singleton<RankingManager>
{
    private readonly List<Rank> rankDatas = new List<Rank>();

    private void Awake()
    {
        var rankData = File.ReadAllLines(@$"{Directory.GetCurrentDirectory()}\Assets\StreamingAssets\RankingData.txt");

        foreach (var data in rankData)
            Debug.Log(data);
    }
}
