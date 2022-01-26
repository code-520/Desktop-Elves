using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    private static bool isClicked;
    private static string[] animationName = { "happy", "jump", "jump1", "jump2" };
    private static string[] audioName = { "Bambs", "A1", "A2", "A3", "A4", "A5", "A6", "A7", "A8" };
    private void Awake()
    {
        isClicked = false;
    }
    private void OnMouseDown()
    {
        isClicked = true;
    }
    public static void SayInHead()
    {
        if(isClicked)
        {
            isClicked = false;
            RoleManger.emotions.SetValue(true, 10);
            RoleManger.animator.SetTrigger(animationName[Random.Range(0,4)]);
            RoleManger.saying.Stop();
            RoleManger.saying.clip = Resources.Load<AudioClip>("Audio\\Keli\\" + audioName[Random.Range(0, 9)]);
            RoleManger.saying.Play();
        }
    }
}
