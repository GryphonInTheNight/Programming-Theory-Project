using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class TeleportingEnemy : Enemy
{
    /// <summary>
    /// Private timer before teleporting again.
    /// </summary>
    [SerializeField] private float portTimer = 0.0f;
    /// <summary>
    /// Cooldown for teleporting.
    /// </summary>
    [SerializeField] private float teleportCooldown = 5.0f;
    /// <summary>
    /// Range for teleporting
    /// </summary>
    [SerializeField] private float portRange = 5.0f;

    // POLYMORPHISM
    protected override void MoveMeTowards(Vector3 target)
    {
        //Moves towards the target like normal
        base.MoveMeTowards(target);

        //When the teleport timer runs down, teleports to a random location within the radius of the range.
        portTimer += Time.deltaTime;
        if (portTimer > teleportCooldown)
        {
            portTimer = 0;
            float angle = Random.Range(0, 2 * Mathf.PI);
            float radius = Random.Range(0, portRange);
            transform.Translate(new Vector3(radius * Mathf.Sin(angle), 0, radius * Mathf.Cos(angle)));
        }
    }
    // POLYMORPHISM
    protected override void MoveMeTowards(GameObject mytarget)
    {
        MoveMeTowards(mytarget.transform.position);
    }

}
