using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExBall : MonoBehaviour
{
    private bool isScore;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Ground")
        {
            if (!isScore) Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Up")
        {
            isScore = true;
        }

    }


}
