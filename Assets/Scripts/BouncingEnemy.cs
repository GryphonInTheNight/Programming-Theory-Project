using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class BouncingEnemy : Enemy
{
    /// <summary>
    /// Max Vertical range to move up and down (before going down again).
    /// </summary>
    [SerializeField] private float yRange = 5.0f;
    /// <summary>
    /// Stores the current y position in the bounce.
    /// </summary>
    [SerializeField] private float currenty = 0.0f;
    /// <summary>
    /// Is the enemy bouncing up or down?
    /// </summary>
    [SerializeField] private bool goingUp = true;
    /// <summary>
    /// Speed at which the enemy bounces up and down.
    /// </summary>
    [SerializeField] private float bounceSpeed = 1.0f;

    // POLYMORPHISM
    protected override void MoveMeTowards(Vector3 movetarget)
    {
        //Moves in the x-z plane.
        base.MoveMeTowards(movetarget);

        //Checks whether we've reached the upper or lower limits of bouncing.
        if (currenty > yRange)
            goingUp = false;
        else if (currenty < 0)
            goingUp = true;

        //Moves in the y direction
        if (goingUp)
        {
            transform.Translate(Vector3.up * bounceSpeed * Time.deltaTime);
            currenty += bounceSpeed * Time.deltaTime;
        }
        else
        {
            transform.Translate(Vector3.down * bounceSpeed * Time.deltaTime);
            currenty -= bounceSpeed * Time.deltaTime;
        }
    }
    // POLYMORPHISM
    protected override void MoveMeTowards(GameObject movetarget)
    {
        MoveMeTowards(movetarget.transform.position);
    }
}
