
using UnityEngine;

public class MovingAtoBPlatform : MapObject
{
    public Vector3 pointA;          
    public Vector3 pointB;          
    public float speed = 2f;  
    private bool onOff = false;

    private Vector3 target;

    void Start()
    {
        target = pointB;
    }

    void Update()
    {
        if (onOff)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            
            if (Vector3.Distance(transform.position, target) < 0.01f)
            {
                target = (target == pointA) ? pointB : pointA;
            }
        }
        
    }

    public override void OnInteract()
    {
        onOff = !onOff;
    }
}
