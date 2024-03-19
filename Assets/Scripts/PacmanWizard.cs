using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class PacmanWizard : MonoBehaviour
{
    public int health = 120;
    public Transform player;
    private ParticleSystem bossDeathEffect;
    public TextMeshProUGUI bossHealthText;

    void Start()
    {
        bossHealthText.text = "";
    }

    void Update()
    {
        bossDeathEffect = GetComponent<ParticleSystem>();
        if (health > 0)
        {
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            agent.SetDestination(player.position);
        }
        else if (health <= 0)
        {
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            agent.speed = 0;
        }
    }

    public void BossTakeDamage(int amount)
    {
        health -= amount;
        bossHealthText.text = "Master Pac-Man's Health: " + health.ToString();
        if (health <= 0)
        {
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            agent.speed = 0;
            bossHealthText.text = "";
            PlayerMovement pacmanSlayer = GameObject.Find("Player").GetComponent<PlayerMovement>();
            pacmanSlayer.levelComplete = true;
            pacmanSlayer.objectiveText.text = "Mission Success!!";
            StartCoroutine(bossDeath());
        }
    }
    IEnumerator bossDeath()
    {
        bossDeathEffect.Play();
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
