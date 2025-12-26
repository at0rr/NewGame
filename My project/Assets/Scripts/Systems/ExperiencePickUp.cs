using UnityEngine;

public class ExperiencePickUp : MonoBehaviour
{
    [SerializeField] private int xpAmount = 10;

    private bool isPickedUp = false;

    private void OnTriggerEnter2D(Collider2D collision) // срабатывания триггера при подходе к шарику xp
    {
        if (isPickedUp) return; // проверяем, что мы ещё не подобрали кружочек

        if (collision.CompareTag("Player")|| collision.GetComponent<Player>() != null)
        {
            if (ExperienceSystem.Instance != null)
            {
                ExperienceSystem.Instance.AddXp(xpAmount);
            }
            isPickedUp = true; // кружочек подобран
            Destroy(gameObject);
        }
    }
}
