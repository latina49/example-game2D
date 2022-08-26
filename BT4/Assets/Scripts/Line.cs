using UnityEngine;

public class Line : MonoBehaviour
{
    [HideInInspector]
    public GameObject line;
    private Vector2 center;
    private float length;
    public float width;
    void Start()
    {
        line = gameObject;
        center = (line.transform.GetChild(0).position + line.transform.GetChild(1).position) / 2;
        length = Vector3.Distance(line.transform.GetChild(0).position, line.transform.GetChild(1).position);
    }
    public Vector2 Center { get { return new Vector2 (center.x, center.y) ; } }
    public float Length { get { return length; } }
    public float Width { get { return width; } }

    private void OnDrawGizmos()
    {
        for(int i = 0; i<transform.childCount - 1; i++)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(i + 1).position);
        }
    }
}
