using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public EnemyBehaviour enemyBehaviour;
    private int speed = 1;
    [SerializeField]
    private int health = 100;
    private int damageAmount = 50;
    GameObject mainCamera;
    Animator animator;
    bool speedChanged = false;
    AudioSource audioSource;
    public AudioClip damage, destroy, attack;


    public void Damage(WeaponBehaviour weaponBehavior)
    {
        switch (weaponBehavior.damage)
        {
            case RateWeapon.Low:
                health -= Mathf.RoundToInt(damageAmount * 0.5f); // Reduce health by half the damage amount
                break;
            case RateWeapon.Moderate:
                health -= damageAmount; // Reduce health by the full damage amount
                break;
            case RateWeapon.High:
                health -= Mathf.RoundToInt(damageAmount * 2f); // Increase damage by 100%
                break;
        }

        // Check if the enemy has been destroyed
        if (health <= 0)
        {

            audioSource.PlayOneShot(destroy);
            Destroy(gameObject);
        }
        else
        {

            audioSource.PlayOneShot(damage);
        }

    }
   
    public void Attack()
    {

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerController>().Damage(enemyBehaviour);
       

        switch (enemyBehaviour.damageToPlayer)
        {
            case RateEnemy.Low:
                animator.SetTrigger("shake1");
                break;
            case RateEnemy.Medium:
                animator.SetTrigger("shake2");
                break;

            case RateEnemy.High:
                animator.SetTrigger("shake3");
                break;
        }



        audioSource.clip = attack;
        audioSource.Play();
        Destroy(this.gameObject);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "PlayerWall")
        {

            Attack();
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GameObject.FindGameObjectWithTag("SFX").GetComponent<AudioSource>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        animator=mainCamera.GetComponent<Animator>();

        speed = (int)enemyBehaviour.Speed;
        //Reset the health according to the hp
        switch (enemyBehaviour.hp)
        {
            case RateEnemy.Low:
                health = Mathf.RoundToInt(health * 0.5f); // Reduce health by half 
                break;
            case RateEnemy.High:
                health = Mathf.RoundToInt(health * 2f); // Increase health by 100%
                break;
        }

    }
    public void SlowDown()
    {
        if (!speedChanged)
        {
            speedChanged = !speedChanged;
            speed /= 2;
        }
       // Invoke("BackToNormalSpeed", 2f);
    }
    public void BackToNormalSpeed()
    {

        speed *= 2;
    }

    void FixedUpdate()
    {
        transform.Translate(-1 * transform.forward * Time.deltaTime * speed);
    }


}
