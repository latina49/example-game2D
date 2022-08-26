using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDie : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer==10)
        {
            Destroy(col.gameObject);
        }

    }
    
}
