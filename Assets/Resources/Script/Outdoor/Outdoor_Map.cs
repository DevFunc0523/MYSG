using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using My.Component;
using My.SaveData;

public class Outdoor_Map : BasicComponent
{

    public void Start()
    {
        Manager.Data.SetLocation(LocationEnum.外部);
        mMainUi.SetStatistics();
    }

}
