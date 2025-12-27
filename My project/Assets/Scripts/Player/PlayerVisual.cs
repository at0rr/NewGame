using NUnit.Framework.Internal;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    private Animator animator; // Получили доступ к аниматору
    private SpriteRenderer spriteRenderer;
    private const string IS_RUNNING = "IsRunning"; //с помощью неё будем обновлять аниматор

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        animator.SetBool(IS_RUNNING, Player.Instance.IsRunning()); //SetBool обновляет значение аниматора, bool потому что у на isRunning тоже bool
        PlayerViewDirection();
    }

    private void PlayerViewDirection()
    {
        // Vector3 mousePos = GameInput.Instance.GetMousePosition(); // чтобы перс смотрел в сторону мыши
        // Vector3 playerPos = Player.Instance.GetPlayerPosition();

        if (Player.Instance.GetGrid().x < 0) // чтобы перс смотрел в сторону движения
        {
            spriteRenderer.flipX = true;
        }
        else if (Player.Instance.GetGrid().x > 0)
        {
            spriteRenderer.flipX = false;
        }
    }
}
