using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if(_currentWaypointIndex >= waypoints.Length)
        {
            _currentWaypointIndex = 0;
        }
        return waypoints[_currentWaypointIndex];
    }

}
