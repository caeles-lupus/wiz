using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TagsSets
{

    /// <summary>
    /// Враги препятствий. Т.е. теги тех, кого может атаковать ловушка.
    /// </summary>
    public static List<string> EnemiesOfObstacle = new List<string> { "Player", "Monsters" };

    /// <summary>
    /// Список тегов, которыми помечены объекты, на которых гг может стоять.
    /// </summary>
    public static List<string> tagsOfRealObjects = new List<string> { /*"Monsters", */"Decor", "Ground", "Traps", "Platform" };

    /// <summary>
    /// Список тегов, которыми помечены объекты, от которых объект со скриптом AI не должен уходить.
    /// </summary>
    public static List<string> tagsForAI = new List<string> { "Player", "Coin", "Crystal", "Flowers" };
    /// <summary>
    /// Список тегов, которыми помечены объекты, которые не могут быть целями атаки.
    /// </summary>
    public static List<string> tagsNonTarget = new List<string> { "Ground", "Platform", "" };

}
