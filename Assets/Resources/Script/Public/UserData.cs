using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;


namespace My.SaveData
{
    public enum LocationEnum
    {
        傢, 外部, 居室,
    }

    public class Data
    {
        public Dictionary<string, int> Time = new Dictionary<string, int>();
        public LocationEnum Location = LocationEnum.居室;
        public int Money = 0;
        public string Name;
    }

    public class UserData
    {
        public Data mData;

        public UserData(int save)
        {
            string saveFile = null;
            Hashtable data = null;
            if (save == -1) // not save -> initfile
            {
                saveFile = "null"; 
                /*
                path = File.ReadAllText(Application.dataPath + "/Resources/Json/Init/UserData.json");
                File.WriteAllText(Application.dataPath + "/Resources/Json/" + saveFile + "/UserData.json", JsonUtility.ToJson(data));
                */
            }
            else { saveFile = save.ToString(); } // savefile

            // data open
            data = (Hashtable)easy.JSON.JsonDecode(File.ReadAllText(Application.dataPath + "/Resources/Json/" + saveFile + "/UserData.json"));
            mData = OpenJson(data);
        }

        public Data Get() { return mData; }

        public void SetLocation(LocationEnum set) { mData.Location = set; }

        // Converter ( Hashtable -> Data )
        private Data OpenJson(Hashtable data)
        {
            Data result = new Data();
            Hashtable copy = (Hashtable)data["Time"];

            foreach (object key in copy.Keys) { result.Time.Add(key.ToString(), (int)copy[key]); }

            result.Location = (LocationEnum)((int)data["Location"]);
            result.Money = (int)data["Money"];
            result.Name = data["Name"].ToString();

            return result;
        }

    }
}
