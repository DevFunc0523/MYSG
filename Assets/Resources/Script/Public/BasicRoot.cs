using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My.BasicRoot
{
    public enum Root
    { CANVAS, SELECT, GAMEMANAGER }

    public class BasicRoot : ScriptableObject
    {
        public Dictionary<Root, string> get = new Dictionary<Root, string>()
        {
            { Root.CANVAS, "Ui/Prefab/BasicCanvas" },
            { Root.SELECT, "Ui/Prefab/Select" },
            { Root.GAMEMANAGER, "Ui/Prefab/GameManager" }
        };

        public static BasicRoot Get() { return Resources.Load("Script/BasicRoot") as BasicRoot; }
    }
}
