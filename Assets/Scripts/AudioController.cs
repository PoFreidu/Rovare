using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/* public class AudioController : MonoBehaviour
{
	public Toggle toggleMusic;
	public Slider sliderVolumeMusic;
	public AudioSource audioSource;
	public float volume;
	private float lastVolume = 1.0f;
	private bool isUpdatingUI = false;

	void Start()
	{
		// Убедитесь, что тег "Music" назначен вашему источнику музыки в Unity Editor.
		audioSource = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
		Load();
		UpdateUI(); // Обновление UI согласно загруженным настройкам
	}

	public void SliderMusic()
	{
		isUpdatingUI = true;
		volume = sliderVolumeMusic.value;
		if (toggleMusic.isOn)
		{
			lastVolume = volume;
		}
		Save();
		UpdateUI();
		isUpdatingUI = false;
	}

	public void ToggleMusic()
	{
		isUpdatingUI = true;
		if (toggleMusic.isOn)
		{
			volume = lastVolume;
		}
		else
		{
			lastVolume = volume;
			volume = 0;
		}
		Save();
		UpdateUI();
		isUpdatingUI = false;
	}

	private void UpdateUI()
	{
		audioSource.volume = volume;
		sliderVolumeMusic.value = volume;
		toggleMusic.isOn = volume > 0;
	}

	public void Save()
	{
		PlayerPrefs.SetFloat("volume", volume);
		PlayerPrefs.SetFloat("lastVolume", lastVolume);
		PlayerPrefs.SetInt("musicOn", toggleMusic.isOn ? 1 : 0);
		PlayerPrefs.Save();
	}

	private void Load()
	{
		volume = PlayerPrefs.GetFloat("volume", 0.5f);
		lastVolume = PlayerPrefs.GetFloat("lastVolume", 1.0f);
		toggleMusic.isOn = PlayerPrefs.GetInt("musicOn", 1) == 1;
		// Обновление слайдера должно происходить после загрузки настроек
		sliderVolumeMusic.value = volume > 0 ? volume : lastVolume;
	}
} */

/* public class AudioController : MonoBehaviour
{
	public Toggle toggleMusic;
	public Slider sliderVolumeMusic;
	public AudioSource audioSource;
	private float lastVolume = 0.5f; // Устанавливаем начальное значение громкости
	private bool isMusicOn = true; // По умолчанию музыка включена
	private bool isUpdatingUI = false; // Флаг для предотвращения рекурсии

	void Start()
	{
		audioSource = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
		Load();
		UpdateUI();
	}

	public void SliderMusic()
	{
		lastVolume = sliderVolumeMusic.value;
		if (isMusicOn)
		{
			audioSource.volume = lastVolume;
		}
		Save();
	}
	
	private void UpdateUI()
	{
		isUpdatingUI = true; // Устанавливаем флаг перед обновлением UI

		sliderVolumeMusic.value = isMusicOn ? lastVolume : 0;
		toggleMusic.isOn = isMusicOn;
		audioSource.volume = isMusicOn ? lastVolume : 0;

		isUpdatingUI = false; // Сбрасываем флаг после обновления UI
	}
	
public void ToggleMusic()
{
	if (isUpdatingUI) return; // Проверяем флаг и выходим, если UI обновляется

	isMusicOn = !isMusicOn;
	Debug.Log("ToggleMusic called. isMusicOn: " + isMusicOn); // Логируем состояние музыки

	if (isMusicOn)
	{
		audioSource.volume = lastVolume; // Сначала устанавливаем громкость
		Debug.Log("Music turned on. Volume set to: " + lastVolume); // Логируем установленную громкость
	}
	else
	{
		lastVolume = audioSource.volume; // Сохраняем текущую громкость
		audioSource.volume = 0; // Затем выключаем звук
		Debug.Log("Music turned off. Last volume saved: " + lastVolume); // Логируем сохраненную громкость
	}

	UpdateUI();
	Save(); // Вызываем Save() после обновления громкости
}
	public void Save()
	{
		PlayerPrefs.SetFloat("lastVolume", lastVolume);
		PlayerPrefs.SetInt("isMusicOn", isMusicOn ? 1 : 0);
		PlayerPrefs.Save();
		Debug.Log("Settings saved. Volume: " + lastVolume + ", Music On: " + isMusicOn); // Логируем сохраненные настройки
	}

private void Load()
{
	// Загружаем сохраненное значение громкости
	lastVolume = PlayerPrefs.GetFloat("lastVolume", 0.5f);
	Debug.Log("Loaded volume from PlayerPrefs: " + lastVolume); // Добавляем лог загруженной громкости

	if (lastVolume <= 0)
	{
		Debug.LogWarning("Loaded volume is 0. Using default value.");
		lastVolume = 0.5f; // Устанавливаем значение по умолчанию, если загруженное значение равно 0
	}

	isMusicOn = PlayerPrefs.GetInt("isMusicOn", 1) == 1;
	Debug.Log("Loaded music state from PlayerPrefs: " + isMusicOn); // Добавляем лог загруженного состояния музыки

	audioSource.volume = isMusicOn ? lastVolume : 0;
	Debug.Log("Settings loaded. Volume: " + lastVolume + ", Music On: " + isMusicOn);
}
}
 */


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









/* public class AudioController : MonoBehaviour
{
	public Toggle toggleMusic;
	public Slider sliderVolumeMusic;
	public AudioSource audioSource;
	public float volume;
	
	void Start()
	{
		audioSource = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
		Load();
		ValueMusic();
	}
	
	public void SliderMusic()
	{
		volume = sliderVolumeMusic.value;
		Save();
		ValueMusic();
	}
	
	public void ToggleMusic()
	{
		if(toggleMusic.isOn == true)
		{
			volume = 1;
		}
		else
		{
			volume = 0;
		}
		Save();
		ValueMusic();
	}
	
	private void ValueMusic()
	{
		audioSource.volume = volume;
		sliderVolumeMusic.value = volume;
		if (volume == 0)
		{
			toggleMusic.isOn = false;
		}
		else
		{
			toggleMusic.isOn = true;
		}
	}
	
	public void Save()
	{
		PlayerPrefs.SetFloat("volume", volume);
	}
	
	private void Load()
	{
		volume = PlayerPrefs.GetFloat("volume", volume);
	}
} */
