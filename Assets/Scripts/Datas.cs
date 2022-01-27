using System.Collections.Generic;
using UnityEngine;
using System;

public class Datas : MonoBehaviour
{
    //情绪值的最大最小值
    public const int MAX_EMOTIONS_VALUE = 100;
    public const int MIN_EMOTIONS_VALUE = 0;
    
    //情绪值数据类型
    public struct Emotions
    {
        public int value;
        public static Emotions Init()
        {
            return JsonHelper.Read<Emotions>("Emotions");
        }
        //修改情绪值(增加/减少)
        public void SetValue(bool add,int val)
        {
            if(add)
            {
                value = value + val > MAX_EMOTIONS_VALUE ? MAX_EMOTIONS_VALUE : value + val;
            }
            else
            {
                value = value - val < MIN_EMOTIONS_VALUE ? MIN_EMOTIONS_VALUE : value - val;
            }
        }
        //获取当前情绪状态(3:开心 2:正常 1:不开心 0:很伤心)
        public int GetStatus()
        {
            if(value>=70)
            {
                return 3;
            }
            else if(value>=60)
            {
                return 2;
            }
            else if(value>=40)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
    
    public struct Grammars
    {
        public List<string> words;
        public void Init()
        {
            words = new List<string>();
        }
        public string[] ToArray()
        {
            List<string> list = new List<string>();
            for(int i=0;i<words.Count;i++)
            {
                string str = words[i].Split('-')[0];
                if(str.IndexOf('|')!=-1)
                {
                    string[] st = str.Split('|');
                    for(int j=0;j<st.Length;j++)
                    {
                        list.Add(st[j]);
                    }
                }
                else
                {
                    list.Add(str);
                }
            }
            return list.ToArray();
        }
        public Dictionary<string,Actions> ToDictionary()
        {
            Dictionary<string, Actions> dict = new Dictionary<string, Actions>();
            foreach(var str in words)
            {
                string[] keyAndValues = str.Split('-');
                if(keyAndValues[0].IndexOf('|')!=-1)
                {
                    string[] keys = keyAndValues[0].Split('|');
                    for(int i=0;i<keys.Length;i++)
                    {
                        Actions a = new Actions();
                        a.Init(keyAndValues[1]);
                        dict.Add(keys[i], a);
                    }
                }
                else
                {
                    Actions a = new Actions();
                    a.Init(keyAndValues[1]);
                    dict.Add(keyAndValues[0], a);
                }
            }
            return dict;
        }
    }
    public struct Actions
    {
        public string value;
        public ActionType type;
        public void Init(string text)
        {
            value = text;
            InitType();
        }
        public void InitType()
        {
            if(value=="AddScale")
            {
                type = ActionType.GetBigger;
            }
            else if(value=="SubScale")
            {
                type = ActionType.GetSmaller;
            }
            else if(value.IndexOf("http")!=-1)
            {
                type = ActionType.OpenWebsite;
            }
            else if(value.IndexOf(".exe")!=-1)
            {
                type = ActionType.OpenProcess;
            }
            else if(value.IndexOf(".bat")!=-1)
            {
                type = ActionType.OpenScript;
            }
            else
            {
                type = ActionType.Say;
            }
        }
    }
    public enum ActionType
    {
        GetBigger=0,
        GetSmaller,
        OpenWebsite,
        OpenProcess,
        OpenScript,
        Say
    }
}
