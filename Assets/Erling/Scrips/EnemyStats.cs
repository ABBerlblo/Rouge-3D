using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public EnemyMove enemyMoveScript;
    PlayerStats playerStats;
    public int hitPoints;
    public int attackPoints;
    public float enemyDmg;
    void Start()
    {
        hitPoints = 100;
        attackPoints = 10;
    }
    void Update()
    {
        if (enemyMoveScript.lookRadius <= 2.5f)
        {
            InvokeRepeating("EnemyAttack",1f,1f);
        }
        if (hitPoints <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Destroy(this);
    }

    public void EnemyAttack()
    {
        playerStats.health -= enemyDmg;
    }
}
