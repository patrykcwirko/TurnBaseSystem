using System.Collections.Generic;
using UnityEngine;

public static class Waiters
{
    private static Dictionary<float, WaitForSeconds> timeCollection = new();

    public static WaitForSeconds WaitForSeconds(float _time)
    {
        if (timeCollection.ContainsKey(_time) == false)
        {
            timeCollection.Add(_time, new WaitForSeconds(_time));
        }

        return timeCollection[_time];
    }
}
