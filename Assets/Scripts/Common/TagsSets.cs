using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TagsSets
{

    /// <summary>
    /// ����� �����������. �.�. ���� ���, ���� ����� ��������� �������.
    /// </summary>
    public static List<string> EnemiesOfObstacle = new List<string> { "Player", "Monsters" };

    /// <summary>
    /// ������ �����, �������� �������� �������, �� ������� �� ����� ������.
    /// </summary>
    public static List<string> tagsOfRealObjects = new List<string> { /*"Monsters", */"Decor", "Ground", "Traps", "Platform" };

    /// <summary>
    /// ������ �����, �������� �������� �������, �� ������� ������ �� �������� AI �� ������ �������.
    /// </summary>
    public static List<string> tagsForAI = new List<string> { "Player", "Coin", "Crystal_Orange",
        "Crystal_DarkBlue", "Crystal_Blue", "Crystal_Red", "Flowers" };
    
    /// <summary>
    /// ������ �����, �������� �������� ���������.
    /// </summary>
    public static List<string> tagsCrystals = new List<string> { "Crystal_Orange", "Crystal_DarkBlue", "Crystal_Blue", "Crystal_Red" };
    
    /// <summary>
    /// ������ �����, �������� �������� �������, ������� �� ����� ���� ������ �����.
    /// </summary>
    public static List<string> tagsNonTarget = new List<string> { "Ground", "Platform", "" };

    

}
