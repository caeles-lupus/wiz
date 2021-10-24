using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyMath
{
    /// <summary>
    /// Находится ли n1 в диапазоне между n2-d и n2+d.
    /// </summary>
    /// <returns></returns>
    public static bool InArea(float n1, float n2, float d)
    {
        return n1 >= n2 - d && n1 <= n2 + d;
    }
}
