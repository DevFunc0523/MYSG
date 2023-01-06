using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using My.Component;

public class IndoorMap : BasicComponent
{
    protected override void Awake()
    {
        base.Awake();
        AwakeBasicCanvas();
    }

    private void Start()
    {
        LocationBtnGet();
    }

    private void LocationBtnGet()
    {
        Transform Mather = GameObject.Find("Location").transform.GetComponent<Transform>();
        for (int i = 0; i < Mather.childCount; i++) {
            switch(Mather.GetChild(i).name) {
                case "Outdoor":{ OutdoorBtn(Mather.GetChild(i).GetComponent<Button>()); } break;
                case "Brother": { BrotherBtn(Mather.GetChild(i).GetComponent<Button>()); } break;
            }
        }
        
        
    }

    // 방밖으로 나가기 버튼
    private void OutdoorBtn(Button set)
    {
        Dictionary<string, Action> temp = new Dictionary<string, Action>();

        void Event_A() {
            SceneManager.LoadScene("Outdoor_Map");
            mMainUi.PopUp_SelectOut();
        }
        temp.Add("밖으로", Event_A);

        void Event_B() {
            mMainUi.PopUp_SelectOut();
        }
        temp.Add("방으로", Event_B);
        set.onClick.AddListener(() => mMainUi.PopUp_Select(temp));
    }

    private void BrotherBtn(Button set)
    {
        Dictionary<string, Action> temp = new Dictionary<string, Action>();

        void Event_A()
        {
            mMainUi.PopUp_SelectOut();
        }
        temp.Add("미구현", Event_A);

        void Event_B()
        {
            mMainUi.PopUp_SelectOut();
        }
        temp.Add("돌아가기", Event_B);
        set.onClick.AddListener(() => mMainUi.PopUp_Select(temp));
    }

}
