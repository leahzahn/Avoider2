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


        //if ((transform.position - this.Destination).sqrMagnitude > 1.30f)
    }

    IEnumerator MoveTo()
    {
        while ((transform.position - this.Destination).sqrMagnitude >  0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position,
                this.Destination, this.Speed * Time.deltaTime);

            if (this.Destination.x - transform.position.x > 0)
            {
                rightPOV.gameObject.transform.position = this.transform.position;
                rightPOV.gameObject.SetActive(true);
                leftPOV.gameObject.SetActive(false);
                upPOV.gameObject.SetActive(false);
                downPOV.gameObject.SetActive(false);
            }
            else if (this.Destination.x - transform.position.x <= 0)
            {
                leftPOV.gameObject.transform.position = this.transform.position;
                leftPOV.gameObject.SetActive(true);
                rightPOV.gameObject.SetActive(false);
                upPOV.gameObject.SetActive(false);
                downPOV.gameObject.SetActive(false);
            }
            //else if (this.Destination.y - transform.position.y > 0)
            //{
            //    upPOV.gameObject.transform.position = this.transform.position;
            //    upPOV.gameObject.SetActive(true);
            //    leftPOV.gameObject.SetActive(false);
            //    rightPOV.gameObject.SetActive(false);
            //    downPOV.gameObject.SetActive(false);
            //}
            //else if (this.Destination.y - transform.position.y <= 0)
            //{
            //    downPOV.gameObject.transform.position = this.transform.position;
            //    downPOV.gameObject.SetActive(true);
            //    leftPOV.gameObject.SetActive(false);
            //    upPOV.gameObject.SetActive(false);
            //    rightPOV.gameObject.SetActive(false);
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
}
