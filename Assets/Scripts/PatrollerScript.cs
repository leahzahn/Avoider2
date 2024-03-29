﻿//Leah Zahn
//ID: 2341427
//zahn @chapman.edu
//CPSC236-03
//Avoider
//This is my own work, and I did not cheat on this assignment.

/*
 * PatrollerScript.cs
 * This class controls a patroller. The patroller moves through its waypoints, and pauses 
 * at any that are marked as sentry points. If it sees the player, it will chase the player, 
 * and if it hits the player, will return the player to its starting point and the patroller 
 * will move to its next waypoint.
 * 
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollerScript : MonoBehaviour
{
    public List<WaypointScript> Waypoints = new List<WaypointScript>();
    public float Speed = 1.0f;
    public int DestinationWaypoint = 1;
    public GameObject upPOV;
    public GameObject rightPOV;
    public GameObject leftPOV;
    public GameObject downPOV;
    public bool spottedPlayer = false;

    private Vector3 Destination;
    private bool Forwards = true;
    private float TimePassed = 0f;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        this.Destination = this.Waypoints[DestinationWaypoint].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        StopAllCoroutines();
        StartCoroutine(MoveTo());
    }

    IEnumerator MoveTo()
    {
        while ((transform.position - this.Destination).sqrMagnitude >  0.01f)
        {
            if (spottedPlayer)
            {
                this.Destination = player.transform.position;
                rightPOV.gameObject.SetActive(false);
                leftPOV.gameObject.SetActive(false);
                upPOV.gameObject.SetActive(false);
                downPOV.gameObject.SetActive(false);
            }
            transform.position = Vector2.MoveTowards(transform.position,
                this.Destination, this.Speed * Time.deltaTime);

            float xDifference = this.Destination.x - transform.position.x;
            float yDifference = this.Destination.y - transform.position.y;

            if ((Math.Abs(xDifference) > Math.Abs(yDifference)) && !spottedPlayer) 
            {
                if (xDifference > 0)
                {
                    rightPOV.gameObject.transform.position = new Vector3(this.transform.position.x + 1.25f, this.transform.position.y, 0);
                    rightPOV.gameObject.SetActive(true);
                    leftPOV.gameObject.SetActive(false);
                    upPOV.gameObject.SetActive(false);
                    downPOV.gameObject.SetActive(false);
                }
                else
                {
                    leftPOV.gameObject.transform.position = new Vector3(this.transform.position.x - 1.25f, this.transform.position.y, 0);
                    leftPOV.gameObject.SetActive(true);
                    rightPOV.gameObject.SetActive(false);
                    upPOV.gameObject.SetActive(false);
                    downPOV.gameObject.SetActive(false);
                }
            }
            else if ((Math.Abs(xDifference) <= Math.Abs(yDifference)) && !spottedPlayer)
            {
                if (yDifference > 0)
                {
                    upPOV.gameObject.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 1.25f, 0);
                    upPOV.gameObject.SetActive(true);
                    leftPOV.gameObject.SetActive(false);
                    rightPOV.gameObject.SetActive(false);
                    downPOV.gameObject.SetActive(false);
                }
                else
                {
                    downPOV.gameObject.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 1.25f, 0);
                    downPOV.gameObject.SetActive(true);
                    leftPOV.gameObject.SetActive(false);
                    upPOV.gameObject.SetActive(false);
                    rightPOV.gameObject.SetActive(false);
                }
            }

            yield return null;
        }

        if ((transform.position - this.Destination).sqrMagnitude <= 0.01f)
        {
            if (this.Waypoints[DestinationWaypoint].IsSentry)
            {
                while (this.TimePassed < this.Waypoints[DestinationWaypoint].PauseTime)
                {
                    this.TimePassed += Time.deltaTime;
                    yield return null;
                }

                this.TimePassed = 0;
            }
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint()
    {
        if (this.Waypoints[DestinationWaypoint].isEndPoint)
        {
            if (this.Forwards)
            {
                this.Forwards = false;
            }
            else
            {
                this.Forwards = true;
            }
        }

        if (this.Forwards)
        {
            ++DestinationWaypoint;
        }
        else
        {
            --DestinationWaypoint;
        }

        if (DestinationWaypoint >= this.Waypoints.Count)
            DestinationWaypoint = 0;

        this.Destination = this.Waypoints[DestinationWaypoint].transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            StopAllCoroutines();
            this.transform.position = this.Waypoints[DestinationWaypoint].transform.position;
            spottedPlayer = false;
        }
    }
}
