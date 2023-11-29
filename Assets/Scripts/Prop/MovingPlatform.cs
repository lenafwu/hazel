using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform platform;
    public Transform startPoint;
    public Transform endPoint;
    public float speed = 1.5f;

    private int direction = 1;

    private void OnDrawGizmos() {
        if(platform != null && startPoint != null && endPoint != null) {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(platform.transform.position, startPoint.position);
            Gizmos.DrawLine(platform.transform.position, endPoint.position);
        }
    }

    void Update(){
        Vector2 target = currentMovementTarget();
        print(target);
        platform.position = Vector2.MoveTowards(platform.position, target, speed * Time.deltaTime);

        float distance = Vector2.Distance(platform.position, target);

        if(distance < 0.1f) {
            direction *= -1;
        }
    }


   // <--- 1      platform     -1 --->
    private Vector2 currentMovementTarget(){
        if(direction == 1) {
            return startPoint.position;
        } else {
            return endPoint.position;
        }
    }

}
