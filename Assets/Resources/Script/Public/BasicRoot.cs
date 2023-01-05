using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My.BasicRoot
{
    public enum Root
    { CANVAS, SELECT }

    public class BasicRoot : ScriptableObject
    {
        public Dictionary<Root, string> get = new Dictionary<Root, string>()
        {
            { Root.CANVAS, "Ui/Canvas/BasicCanvas" },
            { Root.SELECT, "Ui/Canvas/Select" }
        };

        public static BasicRoot Get() { return Resources.Load("Script/BasicRoot") as BasicRoot; }
    }
}
