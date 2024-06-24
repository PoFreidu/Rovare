using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCounter : MonoBehaviour
{
    [SerializeField] private int enemyCounter; // Счётчик врагов

    public GameObject objectToRemove; // Объект для удаления после уничтожения всех врагов

    void Start()
    {
        // Подсчитываем количество врагов на сцене по тегу
        enemyCounter = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    void Update()
    {
        // Пересчитываем количество врагов на сцене
        enemyCounter = GameObject.FindGameObjectsWithTag("Enemy").Length;

        // Если врагов не осталось, удаляем объект
        if (enemyCounter == 0)
        {
            Destroy(objectToRemove);
        }
    }
}
