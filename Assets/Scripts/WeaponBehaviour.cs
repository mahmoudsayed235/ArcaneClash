
using UnityEngine;
public enum RateWeapon
{
    Low = 1,
    Moderate = 2,
    High = 3
};
public enum AreaOfEffect
{
    Single = 1,
    Area = 2
};

[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/WeaponManager", order = 1)]
public class WeaponBehaviour : ScriptableObject
{
    public RateWeapon damage;
    public AreaOfEffect areaOfEffect;
    public bool slowsEnemySpeed;
    public RateWeapon rateOfFire;

}
