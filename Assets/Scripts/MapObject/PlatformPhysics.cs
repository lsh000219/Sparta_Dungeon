using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPhysics : MonoBehaviour  //움직이는 플랫폼 위에있을 때 같이 움직이게 함
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(transform);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }
}
