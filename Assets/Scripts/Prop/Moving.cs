using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Moving : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed;
    public float checkDistance = 0.05f;

    private Transform _targetWaypoint;
    private int _currentWaypointIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        _targetWaypoint = waypoints[0];
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, _targetWaypoint.position, speed * Time.deltaTime);

        if(Vector2.Distance(transform.position, _targetWaypoint.position) < checkDistance)
        {
            _targetWaypoint = GetNextWaypoint();
        }
    }

    private Transform GetNextWaypoint()
    {
        _currentWaypointIndex++;

        // Flip the sprite
        // FIXME: hardcoded scale
        // TODO: way points should be in different directions
        if(this.tag == "Monster"){
             transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
        }

        if(_currentWaypointIndex >= waypoints.Length)
        {
            _currentWaypointIndex = 0;
        }
        return waypoints[_currentWaypointIndex];
    }
}
