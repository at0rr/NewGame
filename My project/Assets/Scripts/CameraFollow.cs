using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player; // создаём переменную для будущего хранения ссылки на местоположение игрока
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10); // координаты камеры (она ровно над игроком)
    [SerializeField] private float smoothSpeed = 0.125f; // плавность полёта камеры

    private void LateUpdate() // LateUpdate выполняется после всех Update, т.е. после того, как персонаж двинулся
    {
        Vector3 newPos = player.position + offset;
        Vector3 smoothPos = Vector3.Lerp(transform.position, newPos, smoothSpeed); // новая позиция камеры (откуда,
        // куда, насколько плавно)
        transform.position = smoothPos; // перемещение камеры
    }
}
