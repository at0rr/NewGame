using UnityEngine;
using System;

public class EnemyEntity : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private GameObject experiencePickupPrefab; // префаб кружка xp

    private int _currHealth;
    private bool isDead = false; // чтобы не наносить урон мёртвому

    private void Start()
    {
        _currHealth = _maxHealth;
    }

    public void TakeDamage(int damage) // враг получает урон
    {
        if (isDead) return; // если враг мёртв, то ничего не делаем
        _currHealth -= damage;

        if (_currHealth <= 0)
        {
            isDead = true;
            DetectDeath();
        }
    }

    private void DetectDeath()
    {
        Vector3 deathPosition = transform.position;

        if (experiencePickupPrefab != null)
        {
            Instantiate(experiencePickupPrefab, deathPosition, Quaternion.identity); // спавн кружка опыта
        }

        Destroy(gameObject);
    }
}
