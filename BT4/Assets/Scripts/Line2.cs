using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line2 : MonoBehaviour
{
    public GameObject line;
    void Start()
    {
        line = gameObject;
    }
    public Vector2 Center { get { return new Vector2(line.transform.position.x, line.transform.position.y); } }
    public float Length { get { return line.transform.GetChild(0).transform.localScale.y; } }
    public float Width { get { return line.transform.GetChild(0).transform.localScale.x / 2; } }
}
