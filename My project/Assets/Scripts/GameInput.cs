using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{

    public static GameInput Instance { get; private set; }

    private PlayerInputActions playerInputActions;
    // второй вариант создания движений перса
    // (файлик с тем же названием лежит в ассетах, его можно посмотреть)

    public event EventHandler OnPlayerAttack; // создаём событие

    private void Awake()
    {
        Instance = this;
        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();

        playerInputActions.Combat.Attack.started += PlayerAttack_started; // атака персонажа
    }

    private void PlayerAttack_started(InputAction.CallbackContext obj)
    {
        if (OnPlayerAttack != null) OnPlayerAttack.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMoveVector()
    {
        Vector2 Grid = playerInputActions.Player.Move.ReadValue<Vector2>();
        return Grid;
    }

    // public Vector3 GetMousePosition() // для сравнения мыши с персонажем, чтобы он смотрел в сторону мыши
    // {
    //     // я это используя для оружия
    //     Vector3 mousePos = Mouse.current.position.ReadValue();
    //     return mousePos;
    // }
}
