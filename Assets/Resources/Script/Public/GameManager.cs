using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using My.SaveData;

namespace My.GameManager
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager Instance = null;
        public UserData Data;

        private void Awake()
        {
            Singleton();
            Data = new UserData();
        }

        private void Singleton()
        { if (null == Instance) { Instance = this; DontDestroyOnLoad(this.gameObject); } else { Destroy(this.gameObject); } }
    }
}
