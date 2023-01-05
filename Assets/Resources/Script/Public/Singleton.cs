using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My.Singleton
{
    public class Singleton : MonoBehaviour
    {
        private static Singleton instance = null;

        protected virtual void Awake()
        {
            if (null == instance)
            {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else { Destroy(this.gameObject); }
        }
    }
}
