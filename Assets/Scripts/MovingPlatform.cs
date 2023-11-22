using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform platform;
    public Transform startPoint;
    public Transform endPoint;
    public float speed = 1.5f;

    int direction = 1;

    private void OnDrawGizmos() {
        if(platform != null && startPoint != null && endPoint != null) {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(platform.transform.position, startPoint.position);
            Gizmos.DrawLine(platform.transform.position, endPoint.position);
        }
    }

    void update(){
        Vector2 target = currentMovementTarget();
        platform.position = Vector2.Lerp(platform.position, target, speed * Time.deltaTime);
    }


   // <--- 1      platform     -1 --->
    Vector2 currentMovementTarget(){
        if(direction == 1) {
            return startPoint.position;
        } else {
            return endPoint.position;
        }
    }
}
