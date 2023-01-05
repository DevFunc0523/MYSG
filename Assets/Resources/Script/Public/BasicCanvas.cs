using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using My.BasicRoot;
using My.GameManager;

public class BasicCanvas : MonoBehaviour
{
    private static BasicCanvas Instance = null;
    public GameManager Manager;

    private BasicRoot mRoot;
    private Transform mCurtain, mSelects;
    private GameObject mSelectObj;

    public Dictionary<string,Text> mStatistics = new Dictionary<string, Text>();

    private void Awake()
    {
        Singleton();
        mRoot = BasicRoot.Get();
    }

    private void Start()
    {
        GetObject();
        setStatistics();
    }

    private void Singleton()
    { if (null == Instance) { Instance = this; DontDestroyOnLoad(this.gameObject); } else { Destroy(this.gameObject); } }

    private void GetObject()
    {
        mSelects = GameObject.Find("Selects").transform;
        mSelects.gameObject.SetActive(false);

        mCurtain = GameObject.Find("Curtain").transform;
        mCurtain.gameObject.SetActive(false);

        Manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private Transform SelectObjectGet(int num)
    {
        if(mSelectObj == null) { mSelectObj = Resources.Load(mRoot.get[Root.SELECT]) as GameObject; }

        if (mSelects.GetChild(0).childCount == num) { Instantiate(mSelectObj, mSelects.GetChild(0)); }
        return mSelects.GetChild(0).GetChild(num).GetComponent<Transform>();
    }

    public void PopUp_SelectOut()
    {
        mSelects.gameObject.SetActive(false);
    }

    public void PopUp_Select(Dictionary<string, Action> popUp)
    {
        int i = 0;
        Transform temp;
        mSelects.gameObject.SetActive(true);
        mSelects.GetChild(0).transform.localPosition = new Vector3(0, 0, 0);
        foreach (KeyValuePair<string, Action> obj in popUp)
        {
            temp = SelectObjectGet(i);
            temp.GetComponent<Button>().onClick.RemoveAllListeners();
            temp.GetComponent<Button>().onClick.AddListener(() => obj.Value());
            temp.GetChild(0).GetComponent<Text>().text = obj.Key;
            temp.localPosition = new Vector3(0, -50 * i, 0);
            i++;
        }
        mSelects.GetChild(0).transform.localPosition = new Vector3(0, (i * 50) / 3, 0);
    }

    private void setStatistics(string mode = null)
    {
        // 초기화 
        if (mode == null)
        {
            Transform root = GameObject.Find("Statistics").transform.Find("Text");
            mStatistics.Add("Day", root.Find("Day").GetComponent<Text>());
            mStatistics.Add("Hour", root.Find("Hour").GetComponent<Text>());

            // 모든 void 사용 
            SetUi_Day();
            SetUi_Time();
        }
        else
        {
            switch (mode)
            {
                case "Day": SetUi_Day(); break;
                case "Time": SetUi_Time(); break;
            }
        }

        void SetUi_Day()
        {
            List<string> week = new List<string>() { "月", "火", "水", "木", "金", "土", "日" };
            mStatistics["Day"].text = Manager.Data.Get().Time["Day"] + "日 ( " + week[Manager.Data.Get().Time["Week"]] + " )";
        }

        void SetUi_Time()
        {
            string temp = (Manager.Data.Get().Time["Hour"] < 10 ? "0" : "") + Manager.Data.Get().Time["Hour"] + ":00";
            mStatistics["Hour"].text = temp;
        }
    }
}
