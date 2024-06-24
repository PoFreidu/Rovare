using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenesManager : MonoBehaviour
{
    // Статическая переменная для доступа к Settings Panel из любого места в коде
    public static GameObject SettingsPanel;

    private void Awake()
    {
        // Находим Settings Panel по тегу и сохраняем ссылку на него
        SettingsPanel = GameObject.FindWithTag("SettingsPanel");
        // Убедитесь, что Settings Panel неактивен при старте, если это необходимо
        SettingsPanel.SetActive(false);
    }

    // Метод для активации Settings Panel
    public static void ActivateSettingsPanel()
    {
        if (SettingsPanel != null)
        {
            SettingsPanel.SetActive(true);
        }
    }
}
