
using UnityEngine;

public enum RateEnemy
{
    Low = 1,
    Medium = 2,
    High = 3
};
public enum RateSpeed
{
    Slow = 1,
    Medium = 2,
    Fast = 3
};
[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyManager", order = 1)]
public class EnemyBehaviour: ScriptableObject
{
    public RateSpeed Speed;
    public RateEnemy hp;
    public RateEnemy damageToPlayer;

}
