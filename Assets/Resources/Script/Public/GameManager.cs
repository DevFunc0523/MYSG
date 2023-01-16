using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using My.SaveData;

public class GameManager : MonoBehaviour
{
    private static GameManager Instance = null;
    public My.SaveData.Data Data;
    public int SaveFile = -1;

    private void Awake()
    {
        Singleton();
        Data = new UserData(SaveFile).mData; // 게임 들어왔을때 입력해주기 
    }

    private void Singleton()
    { if (null == Instance) { Instance = this; DontDestroyOnLoad(this.gameObject); } else { Destroy(this.gameObject); } }
}

