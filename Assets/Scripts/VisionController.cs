//Leah Zahn
//ID: 2341427
//zahn @chapman.edu
//CPSC236-03
//Avoider
//This is my own work, and I did not cheat on this assignment.

/*
 * VisionController.cs
 * This class controls a field of vision of a patroller, and modifies the boolean 
 * spottedPatroller of its patroller if it encounters the player.
 * 
 */

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
