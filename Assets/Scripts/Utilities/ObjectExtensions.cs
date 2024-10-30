using UnityEngine;

public static class ObjectExtensions
{
    public static T OrNull<T>(this T _obj) where T : Object => _obj ? _obj : null;
}
