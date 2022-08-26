using UnityEngine;
public class Main : MonoBehaviour
{
    public int circle_count = 2;
    private Circle[] circles;
    public Line2[] lines;
    private int selected_circle = -1;
    private Vector2 m_position;
    void Start()
    {
        circles = new Circle[circle_count];
        for (int i = 0; i < circles.Length; i++)
        {
            Vector3 position = new Vector3(Random.Range(-15, 15), Random.Range(-5, 5), 0);
            float radius = 2 + i * 0.1f;
            float mass = i == 5 ? -1 : radius; 
            circles[i] = new Circle(position, radius, mass);
        }
    }
    void Update()
    {
        GetInput();
        for (int i = 0; i < circles.Length; i++)
        {
            circles[i].ResetDeltaPosition();
            if (selected_circle == i)
            {
                Vector3 direction = m_position - circles[selected_circle].Position;
                Vector3 delta = direction.normalized * Mathf.Clamp(direction.magnitude, 0, 0.5f);
                circles[selected_circle].UpdateDeltaPosition(delta);
            }
        }
        for (int i = 0; i < circles.Length; i++)
        {
            for (int ii = 0; ii < circles.Length; ii++)
            {
                if(i != ii) ProcessCirclesOverlap(circles[i], circles[ii]);
            }
            for(int ii = 0; ii < lines.Length; ii++)
            {
                ProcessCircleOnLine(lines[ii], circles[i], -1);
            }
            circles[i].UpdatePosition();
        }
    }
    private void GetInput ()
    {
        m_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(Input.GetMouseButtonDown (0))
        {
            for (int i = 0; i < circles.Length; i++)
            {
                if (PointInsideCircle(m_position, i) && circles[i].Mass != -1)
                {
                    selected_circle = i;
                    circles[selected_circle].OnSelected();
                    break;
                }
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
    private bool PointInsideCircle (Vector2 point, int index)
    {
        float distance = Vector2.Distance(point, circles[index].Position);
        return distance < circles[index].radius;
    }
    private void ProcessCirclesOverlap(Circle a, Circle b)
    {
        Vector2 direction = a.Position + a.delta_position - b.Position - b.delta_position;
        float distance = direction.magnitude;
        if (distance < a.radius + b.radius)
        {
            float delta = a.radius + b.radius - distance;
            if(a.Mass == -1)
            {
                b.UpdateDeltaPosition(-delta * direction.normalized);
            }
            else if(b.Mass == -1)
            {
                a.UpdateDeltaPosition(delta * direction.normalized);
            }
            else
            {
                a.UpdateDeltaPosition(delta * a.radius / (a.radius + b.radius) * direction.normalized);
                b.UpdateDeltaPosition(-delta * b.radius / (a.radius + b.radius) * direction.normalized);
            }
        }
    }
    private void ProcessCircleOnLine (Line2 line, Circle circle, int direction)
    {
        Vector2 c_position = circle.Position + circle.delta_position;
        Vector3 local = line.line.transform.InverseTransformPoint(c_position);
        if(Mathf.Abs(local.y) < line.Length / 2 && Mathf.Abs(local.x) < circle.radius + line.Width)
        {
            Vector3 world = line.line.transform.TransformPoint(new Vector3((circle.radius + line.Width) * direction, local.y, 0));
            circle.UpdateDeltaPosition(new Vector2(world.x, world.y) - c_position);
        }
    }
}