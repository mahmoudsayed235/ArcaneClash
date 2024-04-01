using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float health = 100f;
    public float damageAmount = 2f;

    public void Damage(EnemyBehaviour enemyBehavior)
    {
        switch (enemyBehavior.damageToPlayer)
        {
            case RateEnemy.Low:
                health -= Mathf.RoundToInt(damageAmount * 0.5f); // Reduce health by half the damage amount
                break;
            case RateEnemy.Medium:
                health -= damageAmount; // Reduce health by the full damage amount
                break;
            case RateEnemy.High:
                health -= Mathf.RoundToInt(damageAmount * 2f); // Increase damage by 100%
                break;
        }

        // Check if the enemy has been destroyed
        if (health <= 0)
        {
            //GameOver
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
