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
        public int Location = 0;
        public int Money = 0;
    }
}

namespace My.SaveData
{
    public enum LocationEnum
    {
        居室, 外部
    }

    public class Data
    {
        public Dictionary<string, int> Time = new Dictionary<string, int>();
        public LocationEnum Location = LocationEnum.居室;
        public int Money = 0;
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

        public void SetLocation(LocationEnum set) { mData.Location = set; }

        // Converter ( jsonData -> Data )
        private Data ChangeData(JsonData json)
        {
            Data Result = new Data();

            // time dictionary make
            for (int i =0; i < json.TimeKey.Count; i++) { Result.Time.Add( json.TimeKey[i], json.TimeVulume[i]); }
            // location enum make
            Result.Location = (LocationEnum)json.Location;
            // money make
            Result.Money = json.Money;

            return Result;
        }

    }
}
