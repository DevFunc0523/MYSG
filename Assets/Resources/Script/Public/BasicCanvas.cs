using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using My.Singleton;
using My.BasicRoot;

public class BasicCanvas : Singleton
{
    public BasicRoot mRoot;
    private Transform mCurtain, mSelects;
    public GameObject mSelectObj;

    protected override void Awake()
    {
        base.Awake();
        mRoot = BasicRoot.Get();
        GetObject();
    }

    private void GetObject()
    {
        mSelects = GameObject.Find("Selects").transform;
        mSelects.gameObject.SetActive(false);

        mCurtain = GameObject.Find("Curtain").transform;
        mCurtain.gameObject.SetActive(false);
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
}
