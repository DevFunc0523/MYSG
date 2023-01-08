using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using My.BasicRoot;

public class BasicCanvas : MonoBehaviour
{
    private static BasicCanvas Instance = null;
    public GameManager Manager;

    private BasicRoot mRoot;
    public Transform mCurtain, mSelects;
    private GameObject mSelectObj;

    public Dictionary<string,Text> mStatistics = new Dictionary<string, Text>();

    private void Awake()
    {
        Singleton();
        GetObject();
        mRoot = BasicRoot.Get();
    }

    protected void Start()
    {
        SetStatistics();
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

    public void SetStatistics(string mode = null) // 
    {
        Transform root = GameObject.Find("Statistics").transform.Find("Text");
 
        if (mode == null)
        {
            // find statistics text to mStatistics
            string obj = "";
            for (int i =0; i < root.childCount; i++)
            {
                obj = root.GetChild(i).name;
                if (!mStatistics.ContainsKey(obj))
                {
                    mStatistics.Add(obj, root.Find(obj).GetComponent<Text>());
                }
                mStatistics[obj].text = ToText(obj);
            }
            return;
        }

        if(!mStatistics.ContainsKey(mode) ) { mStatistics.Add(mode, root.Find(mode).GetComponent<Text>()); }
        mStatistics[mode].text = ToText(mode);


        string ToText(string obj)
        {
            string result = "";
            switch(obj)
            {
                case "Location": result = Manager.Data.Get().Location.ToString(); break;
                case "Money": result = Manager.Data.Get().Money.ToString() + "円"; break;
                case "Hour": result = (Manager.Data.Get().Time["Hour"] < 10 ? "0" : "") + Manager.Data.Get().Time["Hour"] + ":00"; break;
                case "Health": result = "體力 40/60"; break; // dont
                case "Stamina": result = "射精 3/3"; break; // dont
                case "Day":
                    List<string> week = new List<string>() { "月", "火", "水", "木", "金", "土", "日" };
                    result = Manager.Data.Get().Time["Day"] + "日 ( " + week[Manager.Data.Get().Time["Week"]] + " )";
                    break;
            }
            return result;

            //여기다가 글자로 바꿔주는거 만들기 
        }
    }
}
