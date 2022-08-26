using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dmgBallControl : MonoBehaviour
{
    
    private SpawnControl[] SpawnControls;
    
    public void Init()
    {
        SpawnControls = new SpawnControl[transform.childCount];
        for (int i=0; i < transform.childCount; i++)
        {
            SpawnControls[i] = transform.GetChild(i).GetComponent<SpawnControl>();
            SpawnControls[i].Init();
        }
        
    }
    
}
