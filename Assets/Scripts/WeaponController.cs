using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{


    public WeaponBehaviour weaponBehaviour;

    public float speed = 4f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyController>() != null)
        {
            if (!weaponBehaviour.slowsEnemySpeed)
            {
                other.GetComponent<EnemyController>().Damage(weaponBehaviour);
            }
            
        }



        Destroy(this.gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(transform.forward * Time.deltaTime * speed);
    }
}
