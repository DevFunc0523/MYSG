using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using My.SaveData;

namespace My.Component
{
    using My.BasicRoot;
    using My.Character;

    public class BasicComponent : MonoBehaviour
    {
        public BasicRoot mRoot;
        public BasicCanvas mMainUi = null;
        public GameManager Manager = null;
        public Reding Character = null;

        protected virtual void Awake()
        {
            InitRoot();
            InitGameManager();
            InitBasicCanvas();
            InitCharacter();
        }

        private void InitRoot() { if(mRoot == null) { mRoot = BasicRoot.Get(); } }

        protected void InitBasicCanvas()
        {
            GameObject obj = GameObject.Find("BasicCanvas");
            if (obj == null)
            {
                GameObject creativeObj = Resources.Load(mRoot.get[Root.CANVAS]) as GameObject;
                obj = Instantiate(creativeObj, Vector3.zero, Quaternion.identity);
                obj.name = "BasicCanvas";
            }
            mMainUi = obj.GetComponent<BasicCanvas>();
        }

        private void InitGameManager()
        {
            GameObject obj = GameObject.Find("GameManager");
            if (obj == null)
            {
                GameObject creativeObj = Resources.Load(mRoot.get[Root.GAMEMANAGER]) as GameObject;
                obj = Instantiate(creativeObj, Vector3.zero, Quaternion.identity);
                obj.name = "GameManager";
            }
            Manager = obj.GetComponent<GameManager>();
        }

        private void InitCharacter()
        {
            Character = null;
            Character = new Reding(Manager.Data.Time["Day"] + "_" + Manager.Data.Time["Hour"]);
        }

        protected void MapStart()
        {
            for(int i =0; i < Character.nameList.Count; i++)
            {
                foreach(string key in Character.mObject[Character.nameList[i]].Keys)
                {
                    switch (key)
                    {
                        case "IndoorMap": Indoor_Location(Character.mObject[Character.nameList[i]]); break;
                    }
                }
            }

            void Indoor_Location(Dictionary<string,string> data)
            {
                GameObject copy = Instantiate(Resources.Load<GameObject>("Ui/Prefab/Badge"));
                copy.transform.SetParent(transform.Find("Location").transform.Find(data["IndoorMap"]).transform);
                copy.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/" + data["name"] + "/Badge");
                copy.transform.localPosition = Vector3.zero;
                copy.transform.localScale = Vector3.one;

                Button eventBtn = copy.GetComponent<Button>();
                Dictionary<string, Action> action = new Dictionary<string, Action>();

                List<string> text = new List<string>(data["btnText"].Split('/'));
                List<string> texts = new List<string>();

                eventBtn.onClick.RemoveAllListeners();

                for (int i =0; i < text.Count; i++)
                {
                    texts = new List<string>(text[i].Split('%'));
                    switch (texts[0])
                    {
                        case "In": action.Add(texts[1], In); break;
                        case "Return": action.Add(texts[1], Return); break;
                    }
                }

                eventBtn.onClick.RemoveAllListeners();
                eventBtn.onClick.AddListener(() => mMainUi.PopUp_Select(action));
                if (data.ContainsKey("btnEvent")) { eventBtn.onClick.AddListener(() => mMainUi.PopUp_Text(data["btnEvent"], Manager.Data.Name)); }

                void In()
                {
                    SceneManager.LoadScene("Indoor_" + data["IndoorMap"]);
                    mMainUi.PopUp_SelectOut();
                }

                void Return() { mMainUi.PopUp_SelectOut(); }
            }
        }
    }
}
