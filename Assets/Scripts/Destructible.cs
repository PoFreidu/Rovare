using System.Collections;
using System.Collections.Generic;
using NavMeshPlus.Components;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField] private GameObject destroyVFX;
    [SerializeField] private NavMeshSurface navMeshSurface; // Ссылка на NavMeshSurface

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Проверяем, является ли объект снарядом или мечом
        if (other.gameObject.GetComponent<Projectile>() || other.gameObject.GetComponent<Sword>()) {
            DestroyObject();
        }
    }
    
    public void DestroyObject()
    {
        GetComponent<PickUpSpawner>().DropItems();
        Instantiate(destroyVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
        UpdateNavMeshSurface(); // Вызов обновления NavMesh после уничтожения объекта
    }

    private void UpdateNavMeshSurface()
    {
        if (navMeshSurface)
        {
            navMeshSurface.BuildNavMesh();
            Debug.Log("NavMeshSurface обновлён."); // Сообщение в консоль
        }
    }
}