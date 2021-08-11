using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Target
    [SerializeField] private GameObject player;

    //Enemy Stats
    [SerializeField] private float speed = 0.05f;
    public float health = 50.0f;
    private float damage = 20.0f;
    public Animator animator;
    public string type;
    public bool move = false;

    //Attacking
    private bool reachPlayer = false;
    private float ticks = 0.0f;
    private const float INTERVAL = 5.0f;
    bool damage_received = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z > player.transform.position.z + 1.5)
        {
            if (move)
            {
                transform.LookAt(player.transform);
                transform.position += transform.forward * speed * Time.deltaTime;
            }           
        }

        else
        {
            reachPlayer = true;
        }

        if (move)
        {
            Attack();
        }       
    }

    private void Attack()
    {
        if (reachPlayer)
        {
            if(this.type != "WASP")
            {
                animator.SetBool("Reached", true);
            }
            
            ticks += Time.deltaTime;
            if(ticks >= INTERVAL)
            {
                animator.SetBool("Attack", true);               
                ticks = 0.0f;
            }

            else
            {
                animator.SetBool("Attack", false);
            }
        }
    }

    public void DamagePlayer()
    {
        GameSystem.health -= damage;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        animator.SetFloat("Health", health);

        if(health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(this.gameObject, 1);
        if (!damage_received)
        {
            GameSystem.score += 50;
            damage_received = true;
        }      
    }
}
