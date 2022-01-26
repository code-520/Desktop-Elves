using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class body : MonoBehaviour
{
    private static bool isClicked;
    private void Awake()
    {
        isClicked = false;
    }

    public static void SayInBody()
    {
        if(isClicked)
        {
            Legs.Actioning(10);
            isClicked = false;
        }
    }
    private void OnMouseDown()
    {
        isClicked = true;
    }
}
