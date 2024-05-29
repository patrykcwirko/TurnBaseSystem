using System.Collections.Generic;
using UnityEngine;

public static class ListExtensions
{
    public static T First<T>(this List<T> _list)
    {
        return _list[0];
    }

    public static T GetRandom<T>(this List<T> list)
    {
        if (list.Count == 0)
        {
            return default;
        }

        var randomIndex = Random.Range(0, list.Count);

        var randomItem = list[randomIndex];

        return randomItem;
    }
}
