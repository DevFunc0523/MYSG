using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using My.Component;

public class IndoorMap : BasicComponent
{
    private void Start()
    {
        LocationBtnGet();
        MapStart();
    }

    private void LocationBtnGet()
    {
        Transform temp = transform.Find("Location").transform.GetComponent<Transform>();
        for (int i = 0; i < temp.childCount; i++) {
            switch (temp.GetChild(i).name) {
                case "Outdoor": { OutdoorBtn(SetButton(i)); } break;
            }
        }

        Button SetButton(int i) { return temp.GetChild(i).GetComponent<Button>();  }
    }

    private void OutdoorBtn(Button set)
    {
        Dictionary<string, Action> temp = new Dictionary<string, Action>();

        void Out()
        {
            SceneManager.LoadScene("Outdoor_Map");
            mMainUi.PopUp_SelectOut();
        }

        void Return() 
        {
            mMainUi.PopUp_SelectOut();
        }

        temp.Add("でる", Out);
        temp.Add("かえる", Return); 
        set.onClick.RemoveAllListeners();
        set.onClick.AddListener(()=>mMainUi.PopUp_Select(temp));
    }

    // tast
    private void BadgeBtn(Button set)
    {
        Dictionary<string, Action> temp = new Dictionary<string, Action>();

        void Out()
        {
            SceneManager.LoadScene("Indoor_Sofa");
            mMainUi.PopUp_SelectOut();
        }

        void Return()
        {
            mMainUi.PopUp_SelectOut();
        }

        temp.Add("ショファへ行く", Out);
        temp.Add("かえる", Return);
        set.onClick.RemoveAllListeners();
        set.onClick.AddListener(() => mMainUi.PopUp_Select(temp));
    }
}
