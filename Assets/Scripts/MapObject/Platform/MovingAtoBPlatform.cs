
using UnityEngine;

public class MovingAtoBPlatform : MapObject // 두 지점을 왕복하는 노란색 플랫폼
{
    public Vector3 pointA;          
    public Vector3 pointB;          
    public float speed = 2f;  
    public bool onOff;

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

    public override void OnInteract()  // 상호작용시 이동을 멈출수도, 다시 움직일수도 있음
    {
        onOff = !onOff;
    }
}
