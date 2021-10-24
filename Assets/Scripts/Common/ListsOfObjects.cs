using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ListsOfObjects
{
    private static List<Monster> Monsters;
    private static List<Obstacle> Obstacles;
    private static List<Decor> Decors;

    //Instance

    public static void AddMonster(Monster monster)
    {
        if (Monsters == null) Monsters = new List<Monster>();
        Monsters.Add(monster);
    }
    public static void AddObstacle(Obstacle obstacle)
    {
        if (Obstacles == null) Obstacles = new List<Obstacle>();
        Obstacles.Add(obstacle);
    }
    public static void AddDecor(Decor decor)
    {
        if (Decors == null) Decors = new List<Decor>();
        Decors.Add(decor);
    }

    //======================================================
    //======================================================

    //TODO
    public static GameObject GetGameObjectOfDecor(Decor decor)
    {
        return Decors.Find(dec => dec == decor).gameObject;
    }
    //TODO

    public static GameObject GetGameObjectOfMonster(Monster monster)
    {
        return Monsters.Find(dec => dec == monster).gameObject;
    }
    //TODO
    public static GameObject GetGameObjectOfObstacles(Obstacle obstacle)
    {
        return Obstacles.Find(dec => dec == obstacle).gameObject;
    }
}
