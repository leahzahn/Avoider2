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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 mouseInSpace = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            //StopAllCoroutines();
            StopCoroutine(Click());
            StopCoroutine(MoveTo(transform.position, mouseInSpace, speed));

            mouseInSpace = new Vector3(mouseInSpace.x, mouseInSpace.y, 0);

            if ((timer > 0.0f) && !doubleClick)
            {
                Debug.Log("dash");
                doubleClick = true;
                speed *= 2;
                //if (!speedDoubled)
                //{
                //    speed *= 2;
                //    speedDoubled = true;
                //}
                StartCoroutine(OnDoubleClick());

            }
            else
                doubleClick = false;

            
            StartCoroutine(Click());

            StartCoroutine(MoveTo(transform.position, mouseInSpace, speed));

        }

        //if (speedDoubled)
        //    speedDoubled = false;
    }

    IEnumerator OnDoubleClick()
    {
        //speed *= 2;
        while (doubleClickTimer < 5.0f)
        {
            doubleClickTimer += Time.deltaTime;
            Debug.Log(doubleClickTimer + ", " + doubleClick);
            doubleClick = true;
            yield return null;
        }

        if (doubleClickTimer >= 5.0f)
        {
            Debug.Log("in reset speed if");
            speed /= 2;
            //speedDoubled = false;
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

            //if (doubleClick)
            //    transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime * 2);
            //else
            transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            yield return true;
        }

        if ((transform.position - destination).sqrMagnitude <= 0.05f)
            animator.SetBool("isRunning", false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StopAllCoroutines();

        //Debug.Log("collision w/ " + collision.gameObject.layer);

        if (collision.gameObject.layer == 10) //wall
        {
            StopAllCoroutines();
        }
        else if (collision.gameObject.layer == 11) //door
        {
            if (hasKey)
            {
                buttonController.OnGameQuitButtonClick();
                StopAllCoroutines();
            }
            
            Debug.Log("Door hit!");
            
        }
        else if (collision.gameObject.layer == 9) //enemy
        {
            this.transform.position = startPoint.transform.position;
        }
        else if (collision.gameObject.layer == 12) // key
        {
            collision.gameObject.SetActive(false);
            KeyText.GetComponent<Text>().text = "Key: Acquired";
            hasKey = true;
        }
    }
}
