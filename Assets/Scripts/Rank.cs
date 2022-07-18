using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Rank
{
    public string playerInitial;
    public string playerScore;

    public Rank(string playerInitial, string playerScore)
    {
        this.playerInitial = playerInitial;
        this.playerScore = playerScore;
    }
}
