using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public EnemyStats Stats;
    public GameObject Drop;
    public NavMeshAgent Agent;
    public CircleCollider2D Sight;
    public Slider slider;

    private string state = "patrol";
    private float currentHealth;
    private bool ableToAttack = true;
    private Collider2D target;
    private Vector3 destination;
    private bool isTurnedRight = false;

    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = Stats.maxHealth;
        slider.value = Stats.maxHealth;
        slider.gameObject.SetActive(false);
        currentHealth = Stats.maxHealth;
        Agent.updateRotation = false;
        Agent.updateUpAxis = false;
        Sight.radius = Stats.agroRadius;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            target = other;
            destination = other.transform.position;
            Agent.destination = destination;
            state = "chasing";
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if ((Agent.velocity.x > 0 && !isTurnedRight) || (Agent.velocity.x < 0 && isTurnedRight)) EnemyFlip();
        switch (state)
        {
            case "patrol":

                break;
            case "chasing":
                if (Vector3.Distance(destination, target.transform.position) > 1.0f)
                {
                    destination = target.transform.position;
                    Agent.destination = destination;
                }
                if (Agent.remainingDistance < Stats.attackRange)
                {
                    state = "attacking";
                }
                break;
            case "attacking":
                if (ableToAttack) Attack();
                break;
            default:
                break;
        }
    }

    private void Attack()
    {
        ableToAttack = false;
        Collider2D player = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y), Stats.attackRange, LayerMask.GetMask("Player"));
        if (player != null)
        {
            player.GetComponent<PlayerController>().TakeDamage(Stats.damage);
        }
        state = "chasing";
        Invoke("ReadyAttack", Stats.attackCooldown);
    }

    private void ReadyAttack()
    {
        ableToAttack = true;
    }

    private void Death()
    {
        Instantiate(Drop, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Death();
        }
        else
        {
            slider.gameObject.SetActive(true);
            slider.value = currentHealth;
        }
    }

    public void EnemyFlip()
    {
        transform.localScale = new Vector3(transform.localScale.x * (-1), transform.localScale.y, 1);
        isTurnedRight = !isTurnedRight;
    }
}
