using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    /// <summary>
    /// The speed of the projectile.
    /// </summary>
    [SerializeField] private float projectileSpeed = 10.0f;
    /// <summary>
    /// +/- x and z ranges beyond which to destroy the projectile.
    /// </summary>
    [SerializeField] private float outOfBounds = 100;
    /// <summary>
    /// The damage done by the projectile.
    /// </summary>
    [SerializeField] private int damage = 1;

    // Update is called once per frame
    void Update()
    {
        //ABSTRACTION
        MoveProjectile();
        CheckOutOfBounds();
    }
    /// <summary>
    /// Moves the projectile.
    /// </summary>
    void MoveProjectile()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * projectileSpeed);
    }
    /// <summary>
    /// Checks whether the projectile is out of bounds.  If so, destroys it.
    /// </summary>
    void CheckOutOfBounds()
    {
        if (transform.position.x > outOfBounds
            || transform.position.x < -outOfBounds
            || transform.position.z > outOfBounds
            || transform.position.z < -outOfBounds)
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Checks whether the projectile collided with an enemy.
        //If so, damages it.
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
            enemy.HitMe(damage);
        //Then destroys the projectile.  (This is here not nested in case non-enemy rigid objects get added.)
        Destroy(gameObject);
    }
}
