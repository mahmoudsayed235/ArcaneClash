using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    public float loadWeaponTime = 1f;
    [System.Serializable]
    public class WeaponType
    {
        public GameObject weaponPrefab;
        [HideInInspector]
        public float timeToFire;
        [HideInInspector]
        public bool isReady;
    }
    public List<WeaponType> weaponTypes;

    int currentWeapon = 1;

    private float nextAttackTime1 = 0f;
    private float nextAttackTime2 = 0f;
    private float nextAttackTime3 = 0f;
    UIController uiController;


    // Start is called before the first frame update
    void Start()
    {
        currentWeapon = 1;
        foreach (WeaponType weapon in weaponTypes)
        {
            switch (weapon.weaponPrefab.GetComponent<WeaponController>().weaponBehaviour.rateOfFire)
            {
                case RateWeapon.Low:
                    loadWeaponTime = 0.5f;
                    break;
                case RateWeapon.Moderate:
                    loadWeaponTime = 1f;
                    break;

                case RateWeapon.High:
                    loadWeaponTime = 2.0f;
                    break;
            }
            weapon.timeToFire = loadWeaponTime;
            weapon.isReady = true;
        }

        uiController = GameObject.FindObjectOfType<UIController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentWeapon = 1;
            print("1");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentWeapon = 2;
            print("2");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentWeapon = 3;
            print("3");
        }
        Attack();
    }
    void Attack()
    {
        if (currentWeapon == 1)
        {
            if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextAttackTime1)
            {
                Attack1();
                nextAttackTime1 = Time.time + (1f / weaponTypes[currentWeapon - 1].timeToFire);
                print("nextAttackTime1 : " + (nextAttackTime1 - Time.time));//.5
            }
        }
        else if (currentWeapon == 2)
        {
            if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextAttackTime2)
            {
                Attack2();
                nextAttackTime2 = Time.time + (1f / weaponTypes[currentWeapon - 1].timeToFire);
                print("nextAttackTime2 : " + (nextAttackTime2 - Time.time));//1
            }
        }
        else if (currentWeapon == 3)
        {
            if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextAttackTime3)
            {
                Attack3();
                nextAttackTime3 = Time.time + (1f / weaponTypes[currentWeapon - 1].timeToFire);
                print("nextAttackTime3 : " + (nextAttackTime3 - Time.time));//2
            }
        }
    }
    void Attack1()
    {
        uiController.Reload1(0.5f);
        Instantiate(weaponTypes[currentWeapon - 1].weaponPrefab, this.transform.position, Quaternion.identity);

    }
    void Attack2()
    {
        uiController.Reload2(2.0f);
        Instantiate(weaponTypes[currentWeapon - 1].weaponPrefab, this.transform.position, Quaternion.identity);
        EnemyController[] enemies = GameObject.FindObjectsOfType<EnemyController>();
        // Loop through each enemy
        foreach (EnemyController enemy in enemies)
        {
            enemy.SlowDown();
        }


    }

    void Attack3()
    {

        uiController.Reload3(1.0f);
        Instantiate(weaponTypes[currentWeapon - 1].weaponPrefab, this.transform.position, Quaternion.identity);
    }
}
