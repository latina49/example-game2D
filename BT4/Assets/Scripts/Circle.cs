using UnityEngine;
public class Circle
{
    private readonly GameObject circle;
    private readonly SpriteRenderer spriteRenderer;
    private float mass;
    public Vector2 delta_position;
    public float radius;
    public Circle (Vector2 position, float radius, float mass)
    {
        this.radius = radius / 2;
        this.mass = mass;
        circle = GameObject.Instantiate(Resources.Load("Circle") as GameObject);
        spriteRenderer = circle.GetComponent<SpriteRenderer>();
        circle.transform.position = position;
        circle.transform.localScale = Vector3.one * radius;
        if(mass == -1)
        {
            spriteRenderer.color = Color.yellow;
        }
    }
    public Vector2 Position {  get { return new Vector2(circle.transform.position.x, circle.transform.position.y); }
        set { circle.transform.position = new Vector3(value.x, value.y, 0); }
    }
    public string Name { get { return circle.name; } }

    public void OnSelected()
    {
        spriteRenderer.color = Color.green;
    }
    public void OnDeselected()
    {
        spriteRenderer.color = Color.white;
    }
    public float Mass { get { return mass; }}
    public void ResetDeltaPosition()
    {
        delta_position = Vector2.zero;

        //code trong luc
        //delta_position = new Vector2(0, delta_position.y);
        //delta_position = new Vector3(0, mass != -1? -0.001f : 0, 0);

       
    }
    public void UpdateDeltaPosition(Vector2 delta)
    {
        delta_position += delta;
    }
    public void UpdatePosition()
    {
        Position += delta_position;
    }
}
