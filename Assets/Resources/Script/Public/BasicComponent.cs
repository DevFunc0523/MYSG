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

        private void Awake()
        {
            GameObject obj = GameObject.Find("BasicCanvas");
            if (obj == null)
            {
                mRoot = BasicRoot.Get();
                GameObject creativeObj = Resources.Load(mRoot.get[Root.CANVAS]) as GameObject;
                obj = Instantiate(creativeObj, Vector3.zero, Quaternion.identity);
            }

            mMainUi = obj.GetComponent<BasicCanvas>();
        }
    }
}
