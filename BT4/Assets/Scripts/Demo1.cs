using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo1 : MonoBehaviour
{
    public int circle_count = 10;
    public Circle[] circles;
    public Line[] lines;
    private int selected_circle = -1;
    // Start is called before the first frame update
    void Start()
    {
        circles = new Circle[circle_count];
        for (int i = 0; i < circles.Length; i++)
        {
            float radius = 1;
            float width = 16 / 9 * 5 - radius;
            float height = 5 - radius;
            Vector3 position = new Vector3(Random.Range(-width, width), Random.Range(-height, height), 0);
            float mass = 1;
            circles[i] = new Circle(position, radius, mass);
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Process();
    }
    Vector2 m_position;
    void Process ()
    {
        int c = 0;
        for (int i = 0; i < circles.Length; i++)
        {
            circles[i].ResetDeltaPosition();
        }
        for (int i = 0; i < circles.Length; i++)
        {
            for(int ii = c; ii < circles.Length; ii++)
            {
                if(i != ii)
                    ProcessCirclesOverlap(circles[i], circles[ii]);
            }
            c++;
        }
        for (int i = 0; i < circles.Length; i++)
        {
            for(int ii = 0; ii < lines.Length; ii++)
            {
                ProcessCircleOnLine(lines[ii], circles[i], 1);
            }
            circles[i].UpdatePosition();
        }
    }
    void GetInput ()
    {
        m_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            for (int i = 0; i < circles.Length; i++)
            {
                if(PointInsideCircle(m_position, i))
                {
                    selected_circle = i;
                    circles[i].OnSelected();
                    break;
                }
            }
        }
        else if(Input.GetMouseButton(0))
        {
            if (selected_circle != -1)
            {
                circles[selected_circle].Position = m_position;
            }   
        }
        else if(Input.GetMouseButtonUp(0))
        {
            if(selected_circle != -1)
            {
                circles[selected_circle].OnDeselected();
                selected_circle = -1;
            }
        }
    }
    private bool PointInsideCircle(Vector2 point, int index)
    {
        float distance = Vector2.Distance(point, circles[index].Position);
        return distance < circles[index].radius;
    }
    private void ProcessCirclesOverlap(Circle a, Circle b)
    {
        Vector2 direction = a.Position - b.Position;
        float distance = direction.magnitude;
        if (distance < a.radius + b.radius)
        {
            Vector2 delta = direction.normalized * (a.radius + b.radius - distance);
            a.UpdateDeltaPosition(delta / 2);
            b.UpdateDeltaPosition(-delta / 2);
        }
    }
    private void ProcessCircleOnLine(Line line, Circle circle, int direction)
    {
        Vector2 position = circle.Position + circle.delta_position;
        Vector3 local = line.line.transform.InverseTransformPoint(position);
        if (Mathf.Abs(local.x) < line.Length / 2 && local.y < circle.radius + line.Width)
        {
            Vector2 local_process = new Vector2(local.x, circle.radius + line.width);
            Vector2 world = line.line.transform.TransformPoint(local_process);
            circle.UpdateDeltaPosition(world - position);
        }
    }
}
