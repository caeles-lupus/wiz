using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TagsSets
{

    /// <summary>
    /// Враги препятствий. Т.е. теги тех, кого может атаковать ловушка.
    /// </summary>
    public static string[] EnemiesOfObstacle = new string[] { "Player", "Monsters" };

    /// <summary>
    /// Список тегов, которыми помечены объекты, на которых гг может стоять.
    /// </summary>
    public static List<string> tagsOfRealObjects = new List<string> { "Monsters", "Decor", "Ground", "Traps", "Platform" };

}
