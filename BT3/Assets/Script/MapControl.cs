using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapControl : MonoBehaviour
{
    public dmgBallControl bomd;
    public Enemy_Control[] enemyControl;
    public GameObject main_character;
    // Start is called before the first frame update
    void Start()
    {
        bomd.Init();
        for (int i = 0; i < enemyControl.Length; i++)
            enemyControl[i].Init();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = 0; i < enemyControl.Length; i++)
            enemyControl[i].UpdateMovement(main_character.transform.position);
    }
}
