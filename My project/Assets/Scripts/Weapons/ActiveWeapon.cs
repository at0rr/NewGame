using UnityEngine;
using NUnit.Framework.Internal;

public class ActiveWeapon : MonoBehaviour
{
    public static ActiveWeapon Instance {get; private set;} // для просмотра, какое оружие сейчас выбрано

    [SerializeField] private Sword sword;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        FollowSidePosition();
    }

    public Sword GetActiveWeapon() => sword;

    private void FollowSidePosition() // поворот оружия в нужную сторону
    {
        // Vector3 mousePos = GameInput.Instance.GetMousePosition();
        // Vector3 playerPos = Player.Instance.GetPlayerPosition();

        if (Player.Instance.GetGrid().x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0); // Quaternion - штука для поворота объектов
        }
        else if(Player.Instance.GetGrid().x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
