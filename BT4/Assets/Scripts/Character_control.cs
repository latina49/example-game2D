using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// vi du ve delegate
public class Character_control 

{
    public delegate void LifeCallBack(float value);
    public LifeCallBack callBack;
    GameObject model;
    float life = 5;
    public Character_control ()
    {
        model = new GameObject("Character");
    }

    public void Update()
    {
        if (life > 0)
        {
            life -= Time.deltaTime * 2;
            if (life <= 0)
            {
                //Debug.Log("Death");

                callBack(0);
            }
        }
    }
}
