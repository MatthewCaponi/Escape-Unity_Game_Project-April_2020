using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortScores : Comparer<ScoreItem>
{
    public override int Compare(ScoreItem x, ScoreItem y)
    {
        return y.Score.CompareTo(x.Score);
    }
}
