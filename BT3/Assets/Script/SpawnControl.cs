using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnControl : MonoBehaviour
{
    public GameObject ball_prefab;
    public float min = 4;
    public float max = 9;
    public void Init()
    {
        InvokeRepeating(nameof(CreateBall), 0, Random.Range(min, max));
    }
    public void CreateBall()
    {
        GameObject b = Instantiate(ball_prefab, null);
        b.transform.position = transform.position;
    }

}
