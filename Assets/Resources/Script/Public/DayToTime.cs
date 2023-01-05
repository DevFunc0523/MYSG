using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

using My.Look.UserData;

namespace My.Look.UserData
{
    public class JsonData
    {
        public List<string> TimeKey = new List<string>() { "Day", "Week", "Hour" };
        public List<int> TimeVulume = new List<int>() { 0, 0, 0 };
    }
}

namespace My.SaveData
{
    public class Data
    {
        public Dictionary<string, int> Time = new Dictionary<string, int>();
    }

    public class UserData
    {
        private Data mData;

        public UserData()
        {
            string path = File.ReadAllText(Application.dataPath + "/Resources/Json/init/UserData.json");
            JsonData data = JsonUtility.FromJson<JsonData>(path);
            mData = ChangeData(data);
        }

        public Data Get() { return mData; }

        // Converter ( jsonData -> Data )
        private Data ChangeData(JsonData json)
        {
            Data Result = new Data();

            // time dictionary make
            for (int i =0; i < json.TimeKey.Count; i++) { Result.Time.Add( json.TimeKey[i], json.TimeVulume[i]); }
            return Result;
        }

    }
}
