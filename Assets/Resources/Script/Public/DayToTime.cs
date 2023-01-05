using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My.DayToTime
{
    public class Data
    {
        public string Day, Week = null;
        public int Time = 0;
    }

    public class DayToTime : MonoBehaviour
    {
        private Queue<string> mWeekList = new Queue<string>();

        private void Awake()
        {
                // json 불러오기  ( Basic json 

        }

        private void Roller()
        {
            // 여기다가 요일회전넣기 
        }
    }
}
