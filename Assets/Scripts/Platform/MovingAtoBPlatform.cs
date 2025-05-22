using UnityEngine;

public class MovingAtoBPlatform : MonoBehaviour
{
    public Vector3 pointA;          // 시작 위치
    public Vector3 pointB;          // 도착 위치
    public float speed = 2f;        // 이동 속도

    private Vector3 target;

    void Start()
    {
        target = pointB;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < 0.01f)
        {
            target = (target == pointA) ? pointB : pointA;
        }
    }
}
