using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// Controls the speed at which the player rotates.
    /// </summary>
    [SerializeField] private float rotationSpeed = 80.0f;

    /// <summary>
    /// The offset from the player position for the initial position of the projectile.
    /// </summary>
    [SerializeField] private Vector3 projectileOffset = new Vector3(0, 0, 0);
    /// <summary>
    /// Timer variable to prevent projectile spamming.
    /// </summary>
    private float projectileTimer = 0.2f;
    /// <summary>
    /// The time, in seconds, between successful projectile fires.
    /// </summary>
    [SerializeField] private float projectileReloadSpeed = 1.0f;
    /// <summary>
    /// The prefab for the projectile object the player fires.
    /// </summary>
    [SerializeField] private Projectile projectilePrefab;

    // Update is called once per frame
    void Update()
    {
        // ABSTRACTION
        HandleCameraRotation();
        HandleIfFiringProjectile();
    }
    /// <summary>
    /// Rotates the camera around the y-axis according to horizontal input.
    /// </summary>
    void HandleCameraRotation()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Input.GetAxis("Horizontal") * Time.deltaTime);
    }
    /// <summary>
    /// Fires a projectile when the space bar is pressed.
    /// </summary>
    void HandleIfFiringProjectile()
    {
        projectileTimer += Time.deltaTime;
        if (Input.GetKey(KeyCode.Space) && projectileTimer > projectileReloadSpeed && !GameManager.Instance.hasLost)
        {
            projectileTimer = 0;
            Instantiate(projectilePrefab, transform.position +
                transform.right * projectileOffset.x +
                transform.up * projectileOffset.y +
                transform.forward * projectileOffset.z,
                transform.rotation);
        }
    }
}
