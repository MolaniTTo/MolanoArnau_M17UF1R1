using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMov : MonoBehaviour
{
    public Transform[] waypoints;

    public float velocity = 2f;
    private int nextWaypoint = 0;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (waypoints.Length > 0)
        {
            Transform destination = waypoints[nextWaypoint];
            transform.position = Vector3.MoveTowards(transform.position, destination.position, velocity * Time.deltaTime); //mou l'enemic cap al waypoint
            if(destination.position.x < transform.position.x) //gira l'enemic segons la direccio
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }

            if(Vector3.Distance(transform.position, destination.position) < 0.1f) //quan arriba al waypoint, canvia de waypoint
            {
                nextWaypoint = (nextWaypoint + 1) % waypoints.Length; //canvia de waypoint
            } 
        }

    }
}
