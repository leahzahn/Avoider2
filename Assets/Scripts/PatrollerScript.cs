using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollerScript : MonoBehaviour
{
    public List<WaypointScript> Waypoints = new List<WaypointScript>();
    public float Speed = 1.0f;
    public int DestinationWaypoint = 1;
    //public GameObject upPOV;
    //public GameObject rightPOV;
    //public GameObject leftPOV;
    //public GameObject downPOV;
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

        if (CheckforPlayer())
        {
            //this.Destination = player.transform.position;
            StopAllCoroutines();
            StartCoroutine(MoveToPlayer());
        }

        //if ((transform.position - this.Destination).sqrMagnitude > 1.30f)
    }

    private bool CheckforPlayer()
    {
        return ((this.transform.position - player.transform.position).sqrMagnitude < 7.0f);
    }

    IEnumerator MoveToPlayer()
    {
        while ((transform.position - player.transform.position).sqrMagnitude > 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position,
                player.transform.position, this.Speed * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator MoveTo()
    {
        while ((transform.position - this.Destination).sqrMagnitude >  0.01f)
        {
            if (spottedPlayer)
            {
                this.Destination = player.transform.position;
            }
            transform.position = Vector2.MoveTowards(transform.position,
                this.Destination, this.Speed * Time.deltaTime);

            //float xDifference = this.Destination.x - transform.position.x;
            //float yDifference = this.Destination.y - transform.position.y;

            //if (Math.Abs(xDifference) > Math.Abs(yDifference))
            //{
            //    if (xDifference > 0)
            //    {
            //        rightPOV.gameObject.transform.position = new Vector3(this.transform.position.x + 0.75f, this.transform.position.y, 0);
            //        rightPOV.gameObject.SetActive(true);
            //        leftPOV.gameObject.SetActive(false);
            //        upPOV.gameObject.SetActive(false);
            //        downPOV.gameObject.SetActive(false);
            //    }
            //    else
            //    {
            //        leftPOV.gameObject.transform.position = new Vector3(this.transform.position.x - 0.75f, this.transform.position.y, 0);
            //        leftPOV.gameObject.SetActive(true);
            //        rightPOV.gameObject.SetActive(false);
            //        upPOV.gameObject.SetActive(false);
            //        downPOV.gameObject.SetActive(false);
            //    }
            //}
            //else
            //{
            //    if (yDifference > 0)
            //    {
            //        upPOV.gameObject.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 1.25f, 0);
            //        upPOV.gameObject.SetActive(true);
            //        leftPOV.gameObject.SetActive(false);
            //        rightPOV.gameObject.SetActive(false);
            //        downPOV.gameObject.SetActive(false);
            //    }
            //    else
            //    {
            //        downPOV.gameObject.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 1.25f, 0);
            //        downPOV.gameObject.SetActive(true);
            //        leftPOV.gameObject.SetActive(false);
            //        upPOV.gameObject.SetActive(false);
            //        rightPOV.gameObject.SetActive(false);
            //    }
            //}

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
        //else if (collision.gameObject.layer == 10)
        //{
        //    StopAllCoroutines();
        //    this.transform.position = this.Waypoints[DestinationWaypoint].transform.position;
        //    spottedPlayer = false;
        //}
    }
}
