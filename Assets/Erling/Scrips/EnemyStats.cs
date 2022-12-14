using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int hitPoints;
    public int attackPoints;
    void Start()
    {
        hitPoints = 100;
        attackPoints = 10;
    }
    void Update()
    {
        if (hitPoints <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Destroy(this);
    }
}
