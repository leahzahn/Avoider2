//Leah Zahn
//ID: 2341427
//zahn @chapman.edu
//CPSC236-03
//Avoider
//This is my own work, and I did not cheat on this assignment.

/*
 * PlayerController.cs
 * This class controls the player. The player moves to the location of the last mouse click, 
 * and can dash for 1.5 seconds upon double click. If the player collides with a patroller, 
 * they are returned to their starting point. They can get the key and win the game by colliding 
 * with the door if they have the key. 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    public Animator animator;
    public GameObject startPoint;
    public GameObject KeyText;
    public ButtonController buttonController;

    bool doubleClick = false;
    float timer = 0.0f;
    float doubleClickTimer = 0.0f;
    bool hasKey = false;
    bool speedDoubled = false;
    Vector3 mouseInSpace;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        mouseInSpace = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            StopAllCoroutines();
            if (doubleClick)
            {
                StartCoroutine(OnDoubleClick());
            }

            mouseInSpace = new Vector3(mouseInSpace.x, mouseInSpace.y, 0);

            if ((timer > 0.0f) && !doubleClick && !speedDoubled)
            {
                Debug.Log("dashing");
                doubleClick = true;
                speed *= 2;
                speedDoubled = true;
                StartCoroutine(OnDoubleClick());

            }
            else
                doubleClick = false;


            StartCoroutine(Click());

            StartCoroutine(MoveTo(transform.position, mouseInSpace, speed));

        }
    }

    IEnumerator OnDoubleClick()
    {
        while (doubleClickTimer < 1.5f)
        {
            doubleClickTimer += Time.deltaTime;
            doubleClick = true;
            speedDoubled = true;
            yield return null;
        }

        if (doubleClickTimer >= 1.5f)
        {
            Debug.Log("in reset speed");
            speed /= 2;
            speedDoubled = false;
            doubleClickTimer = 0.0f;
            doubleClick = false;
        }
    }

    IEnumerator Click()
    {
        while (timer < 0.2f)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        if (timer >= 0.2f)
        {
            timer = 0.0f;
        }
    }

    IEnumerator MoveTo(Vector3 start, Vector3 destination, float speed)
    {
        while ((transform.position - destination).sqrMagnitude > 0.05f)
        {
            animator.SetBool("isRunning", true);
            transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            yield return true;
        }

        if ((transform.position - destination).sqrMagnitude <= 0.05f)
            animator.SetBool("isRunning", false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 10) //wall
        {
            StopAllCoroutines();
            if (speedDoubled)
            {
                speed /= 2;
                doubleClick = false;
                speedDoubled = false;
                doubleClickTimer = 0.0f;
            }
            animator.SetBool("isRunning", false);
        }
        else if (collision.gameObject.layer == 11) //door
        {
            if (hasKey)
            {
                buttonController.OnGameWin();

                StopAllCoroutines();
            }
        }
        else if (collision.gameObject.layer == 9) //enemy
        {
            this.transform.position = startPoint.transform.position;
            StopAllCoroutines();
            if (speedDoubled)
            {
                speed /= 2;
                doubleClick = false;
                speedDoubled = false;
                doubleClickTimer = 0.0f;
            }
            animator.SetBool("isRunning", false);
        }
        else if (collision.gameObject.layer == 12) // key
        {
            collision.gameObject.SetActive(false);
            KeyText.GetComponent<Text>().text = "Key: Acquired";
            hasKey = true;
        }
    }
}
