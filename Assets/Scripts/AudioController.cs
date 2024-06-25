using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AudioController : MonoBehaviour
{
	[SerializeField] private Slider sliderVolume; // Слайдер для уровня громкости
	[SerializeField] private Toggle toggleMute; // Тоггл для выключения звука
	[SerializeField] private AudioSource audioSource; // Источник звука

	private const string VolumePref = "VolumePreference";
	private const string MutePref = "MutePreference";

	private void Awake()
	{
		// Загружаем настройки при инициализации
		LoadSettings();
	}
	
	private void OnEnable()
	{
		// Попытка найти объект Music при активации AudioManager
		FindMusicObject();
	}
	
	private void FindMusicObject()
    {
        // Предполагаем, что у объекта Music есть тег "Music"
        GameObject musicObject = GameObject.FindWithTag("Music");
        if (musicObject != null)
        {
            // Если объект найден, получаем его AudioSource
            audioSource = musicObject.GetComponent<AudioSource>();
        }
        else
        {
            // Если объект не найден, можно вывести предупреждение или обработать эту ситуацию
            Debug.LogWarning("Объект Music не найден!");
        }
    }

	private void Start()
	{
		// Подписываемся на события слайдера и тоггла
		sliderVolume.onValueChanged.AddListener(delegate { OnVolumeChange(); });
		toggleMute.onValueChanged.AddListener(delegate { OnToggleMute(); });
	}


	public void OnVolumeChange()
	{
		// Обновляем громкость и сохраняем настройки
		if (audioSource != null)
		{
			audioSource.volume = sliderVolume.value;
		}
		SaveSettings();
	}

	public void OnToggleMute()
	{
		// Обновляем состояние mute и сохраняем настройки
		if (audioSource != null)
		{
			audioSource.mute = !toggleMute.isOn;
		}
		SaveSettings();
	}

	private void SaveSettings()
	{
		// Сохраняем текущие значения громкости и состояние тоггла
		PlayerPrefs.SetFloat(VolumePref, sliderVolume.value);
		PlayerPrefs.SetInt(MutePref, toggleMute.isOn ? 0 : 1);
		PlayerPrefs.Save();
	}

	private void LoadSettings()
	{
		// Загружаем сохраненное значение громкости или устанавливаем максимальную громкость, если его нет
		sliderVolume.value = PlayerPrefs.GetFloat(VolumePref, 1.0f);

		// Проверяем, существует ли сохранённое значение для тоггла
		if (PlayerPrefs.HasKey(MutePref))
		{
			// Загружаем сохранённое значение тоггла
			toggleMute.isOn = PlayerPrefs.GetInt(MutePref) == 0;
		}
		// Если сохранённого значения нет, не устанавливаем состояние тоггла

		// Применяем загруженные настройки к источнику звука
		if (audioSource != null)
		{
			audioSource.volume = sliderVolume.value;
			audioSource.mute = !toggleMute.isOn;
		}
	}

	private void OnApplicationQuit()
	{
		// Сохраняем настройки при выходе из приложения
		SaveSettings();
	}
}
