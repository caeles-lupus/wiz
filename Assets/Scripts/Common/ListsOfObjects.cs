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

    //===============================================
    //===============================================

    /// <summary>
    /// Удаляет объект из списка.
    /// </summary>
    /// <param name="gameObject"></param>
    public static void RemoveObj(GameObject gameObject)
    {
    int index;
        if (Monsters != null && (index = Monsters.FindIndex(m => m.gameObject == gameObject)) != -1)
        {
            Monsters.RemoveAt(index);

        }
        else if (Obstacles != null && (index = Obstacles.FindIndex(m => m.gameObject == gameObject)) != -1)
        {
            Obstacles.RemoveAt(index);
        }
        else if (Decors != null && (index = Decors.FindIndex(m => m.gameObject == gameObject)) != -1)
        {
            Decors.RemoveAt(index);
        }
        else
        {

        }
    }


}
