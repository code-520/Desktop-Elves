using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Legs : MonoBehaviour
{
    public static bool isWalking;

    private static bool isClicked;
    private static string[] audioPath = { "Audio\\Keli\\Excessively", "Audio\\Keli\\Lovely and pitiful" };
    private void Awake()
    {
        isClicked = false;
        isWalking = false;
    }
    //Õ»≤ø¥•∑¢
    public static void SayInLegs()
    {
        if(isClicked)
        {
            Actioning(5);
            isClicked = false;
        }
    }
    
    private void OnMouseDown()
    {
        isClicked = true;
    }
    public static void Actioning(int value)
    {
        int cnt = Random.Range(0, 2);
        if(RoleManger.emotions.GetStatus()==0)
        {
            RoleManger.animator.SetTrigger("hit");
            cnt = 0;
        }
        else
        {
            isWalking = true;
        }
        RoleManger.saying.Stop();
        RoleManger.emotions.SetValue(false, value);
        RoleManger.saying.clip = Resources.Load<AudioClip>(audioPath[cnt]);
        RoleManger.saying.Play();
    }
}
