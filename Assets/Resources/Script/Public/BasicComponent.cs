using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My.Component
{
    using My.BasicRoot;

    public class BasicComponent : MonoBehaviour
    {
        public BasicRoot mRoot;
        public BasicCanvas mMainUi = null;
        public GameManager Manager = null;

        protected virtual void Awake()
        {
            AwakeRoot();
            InitGameManager();
            AwakeBasicCanvas(); 
        }

        private void AwakeRoot() { if(mRoot == null) { mRoot = BasicRoot.Get(); } }

        protected void AwakeBasicCanvas()
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
    }
}
