using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionController : MonoBehaviour
{
    public PatrollerScript patroller;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            Debug.Log("spotted player");
            patroller.spottedPlayer = true;
        }
    }
}
