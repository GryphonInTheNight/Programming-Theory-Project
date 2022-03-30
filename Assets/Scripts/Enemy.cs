using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // ENCAPSULATION
    /// <summary>
    /// The target this enemy is approaching/pursuing, if any.
    /// </summary>
    public GameObject target { get; set; }
    /// <summary>
    /// The current health of this enemy.
    /// </summary>
    [SerializeField] protected int myHealth;
    /// <summary>
    /// The maximum health of this enemy.
    /// </summary>
    [SerializeField] protected int maxHealth;
    /// <summary>
    /// The speed of this enemy.
    /// </summary>
    [SerializeField] protected float speed;
    /// <summary>
    /// Point value for destroying this enemy.
    /// </summary>
    [SerializeField] protected int pointValue;

    private void Update()
    {
        if (target != null)
            MoveMeTowards(target);
    }
    private void Awake()
    {
        myHealth = maxHealth;
    }
    
    /// <summary>
    /// Lowers health by the damage taken, then if health is less than zero destroys the object.
    /// </summary>
    /// <param name="damage">The damage to health taken.</param>
    public void HitMe(int damage)
    {
        myHealth -= damage;
        if (myHealth <= 0)
        {
            GameManager.Instance.UpdateScore(pointValue);
            GameManager.Instance.CheckIfReadyToSpawn();
            Destroy(gameObject);
        }
    }

    // POLYMORPHISM
    /// <summary>
    /// Moves this enemy towards the target position based off its speed.
    /// </summary>
    /// <param name="movetarget">The target position.</param>
    protected virtual void MoveMeTowards(Vector3 movetarget)
    {
        transform.Translate((movetarget - transform.position).normalized * speed * Time.deltaTime);
    }
    //POLYMORPHISM
    /// <summary>
    /// Moves this enemy towards the target GameObject based off its speed.
    /// </summary>
    /// <param name="movetarget">The gameobject to move towards.</param>
    protected virtual void MoveMeTowards(GameObject movetarget)
    {
        MoveMeTowards(movetarget.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        //!!!The player should be the only trigger for this!!!
        GameManager.Instance.LoseGame();
    }
}
