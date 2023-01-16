using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

namespace My.Character
{
    public class Data
    {
        //
    }

    public class Reding
    {
        public string root = null;
        
        public Dictionary<int, List<KeyValuePair<string,string>>> mEvent = new Dictionary<int, List<KeyValuePair<string, string>>>();
        public Dictionary<string, Dictionary<string, string>> mObject = new Dictionary<string, Dictionary<string, string>>();
        public List<string> nameList = new List<string>() { "SotaAya",  };

        public Reding(string stream) // 생성자
        {
            root = stream;
            for(int i = 0; i < nameList.Count; i++) { GetData(nameList[i]); }
        }

        private void GetData(string name)
        {
            Hashtable data, copy = new Hashtable();
            ArrayList temp, tempo = new ArrayList();
            List<KeyValuePair<string, string>> vulme = new List<KeyValuePair<string, string>>();

            data = (Hashtable)easy.JSON.JsonDecode(File.ReadAllText(Application.dataPath + "/Resources/Json/" + "Const" + "/" + name + ".json"));

            if (data.ContainsKey(root + "_Object"))
            {
                copy = (Hashtable)data[root + "_Object"];
                Dictionary<string, string> obj = new Dictionary<string, string>();
                foreach (string key in copy.Keys)
                {
                    obj.Add(key, copy[key].ToString());
                }
                mObject.Add(name, obj);
            }

            if (data.ContainsKey(root+ "_Event"))
            {
                copy = (Hashtable)data[root+"_Event"];
                foreach (string key in copy.Keys)
                {
                    vulme.Clear();
                    temp = (ArrayList)copy[key];

                    for (int i = 0; i < temp.Count; i++)
                    {
                        tempo = (ArrayList)temp[i];
                        vulme.Add(new KeyValuePair<string, string>(tempo[0].ToString(), tempo[1].ToString()));
                    }
                    mEvent.Add(int.Parse(key), vulme);
                }
            }
        }
    }
}
