using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PacmanButler : MonoBehaviour
{
    public int health = 10;
    public int speed = 6;

    public Transform player;
    private ParticleSystem deathEffect;

    void Start()
    {
        deathEffect = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(player.position);
        if (health > 0)
        {
            agent.speed = speed;
        }
        else
        {
            agent.speed = 0;
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health == 0)
        {
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            agent.speed = 0;
            PlayerMovement pacmanSlayer = GameObject.Find("Player").GetComponent<PlayerMovement>();
            pacmanSlayer.pacmanKills += 1;
            pacmanSlayer.scoreCount += 30;
            pacmanSlayer.score.text = "Score: " + pacmanSlayer.scoreCount.ToString();
            if (pacmanSlayer.pacmanKills >= 9) {
                if (PlayerPrefs.GetInt("Maze") == 1)
                    pacmanSlayer.objectiveText.text = "Good.. Find the Switch to Unlock the Next Floor";
            }
            
            if (pacmanSlayer.pacmanKills >= 45)
            {
                if (PlayerPrefs.GetInt("Maze") == 2)
                    pacmanSlayer.objectiveText.text = "All Pac-man Defeated!!";
            }
            StartCoroutine(pacmanButlerDeath());
        }
    }
    IEnumerator pacmanButlerDeath()
    {
        deathEffect.Play();
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
