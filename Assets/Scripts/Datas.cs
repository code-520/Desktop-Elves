using System.Collections.Generic;
using UnityEngine;
using System;

public class Datas : MonoBehaviour
{
    //����ֵ�������Сֵ
    public const int MAX_EMOTIONS_VALUE = 100;
    public const int MIN_EMOTIONS_VALUE = 0;
    
    //����ֵ��������
    public struct Emotions
    {
        public int value;
        public static Emotions Init()
        {
            return JsonHelper.Read<Emotions>("Emotions");
        }
        //�޸�����ֵ(����/����)
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
        //��ȡ��ǰ����״̬(3:���� 2:���� 1:������ 0:������)
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
        public Dictionary<string,string> ToDictionary()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            foreach(var str in words)
            {
                string[] keyAndValues = str.Split('-');
                if(keyAndValues[0].IndexOf('|')!=-1)
                {
                    string[] keys = keyAndValues[0].Split('|');
                    for(int i=0;i<keys.Length;i++)
                    {
                        dict.Add(keys[i], keyAndValues[1]);
                    }
                }
                else
                {
                    dict.Add(keyAndValues[0], keyAndValues[1]);
                }
            }
            return dict;
        }
    }
    public struct Today
    {
        public string date;
        public bool isToday;
        public bool morning;
        public bool afternoon;
        public bool evening;
        public void Init()
        {
            date = DateTime.Now.ToString();
            morning = false;
            afternoon = false;
            evening = false;
            isToday = true;
        }
        //ÿ����������һ����������
        //ע��,������ֵΪfalse,˵���ϴδ洢������������
        public bool UpdateDatas()
        {
            DateTime now = DateTime.Now;
            DateTime lastTime = Convert.ToDateTime(date);
            if(now.Year==lastTime.Year)
            {
                if(now.Month==lastTime.Month)
                {
                    if(now.Day==lastTime.Day)
                    {
                        date = now.ToString();
                    }
                    else if(now.Day>lastTime.Day)
                    {
                        Init();
                    }
                    else
                    {
                        return false;
                    }
                }
                else if(now.Month>lastTime.Month)
                {
                    Init();
                }
                else
                {
                    return false;
                }
            }
            else if(now.Year>lastTime.Year)
            {
                Init();
            }
            else
            {
                return false;
            }
            return true;
        }
        //�ж��Ƿ���к��Լ����к�������
        public bool IsGreeting(out int time)
        {
            time = -1;
            if (isToday)
            {
                if (!morning&&DateTime.Now.Hour>=6&&DateTime.Now.Hour<=10)
                {
                    morning = true;
                    time = 0;
                }
                else if (!afternoon&&DateTime.Now.Hour>=12&&DateTime.Now.Hour<=13)
                {
                    afternoon = true;
                    time = 1;
                }
                else if (!evening&&DateTime.Now.Hour>=19&&DateTime.Now.Hour<=22)
                {
                    evening = true;
                    time = 2;
                }
                else
                {
                    return false;
                }
                return true;
            }
            return false;
        }
    }
    //����ͷЧ�����ݽṹ
    public struct Welcome
    {
        public float size;
        public DateTime time;
        public bool isWelcomed;
        public void Init()
        {
            size = 0f;
            time = DateTime.Now;
            isWelcomed = false;
        }
    }
}
