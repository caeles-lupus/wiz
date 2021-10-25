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

    public static Decor GetDecorOfGameObject(GameObject gameObject)
    {
        return Decors.Find(dec => dec.gameObject == gameObject);
    }
    public static Monster GetMonsterOfGameObject(GameObject gameObject)
    {
        return Monsters.Find(monster => monster.gameObject == gameObject);
    }
    public static Obstacle GetObstacleOfGameObject(GameObject gameObject)
    {
        return Obstacles.Find(obstacle => obstacle.gameObject == gameObject);
    }
}
