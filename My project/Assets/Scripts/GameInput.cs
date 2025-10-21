using UnityEngine;

public class GameInput : MonoBehaviour
{

    public static GameInput Instance { get; private set; }

    private PlayerInputActions playerInputActions;
    // второй вариант создания движений перса (первый в тг)
    // (файлик с тем же названием лежит в ассетах, его можно посмотреть)

    private void Awake()
    {
        Instance = this;

        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();
    }

    public Vector2 GetMoveVector()
    {
        Vector2 Grid = playerInputActions.Player.Move.ReadValue<Vector2>();
        return Grid;
    }

}
