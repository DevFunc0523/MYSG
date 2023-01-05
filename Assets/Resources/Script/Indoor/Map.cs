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
    }

    private void LocationBtnGet()
    {
        Transform Mather = transform.GetComponent<Transform>();
        for (int i = 0; i < Mather.childCount; i++) {
            switch(Mather.GetChild(i).name) {
                case "Outdoor":{ OutdoorBtn(Mather.GetChild(i).GetComponent<Button>()); } break;
            }
        }
    }

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

}
