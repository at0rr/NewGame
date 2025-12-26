using UnityEngine;

public class EnemyEntity : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    private int _currHealth;

    private void Start()
    {
        _currHealth = _maxHealth;
    }

    public void TakeDamage(int damage) // враг получает урон
    {
        _currHealth -= damage;

        DetectDeath();
    }

    private void DetectDeath()
    {
        if (_currHealth <= 0) Destroy(gameObject);
    }
}
