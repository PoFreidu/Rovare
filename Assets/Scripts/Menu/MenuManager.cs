using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
	public GameObject settingsPanel;
	public GameObject loadingScreen;
	public TMP_Dropdown resolutionDropdown;
	public TMP_Dropdown qualityDropdown;
	public Toggle fullScreenToggle;
	public Slider progressLoading;
	
	// Список поддерживаемых разрешений экрана
	private List<Resolution> supportedResolutions;
	
	void Start()
	{
		supportedResolutions = new List<Resolution>
		{
			CreateResolution(1280, 720, 60),
			CreateResolution(1920, 1080, 60),
			CreateResolution(2560, 1440, 60),
			CreateResolution(3840, 2160, 60),
			// Добавьте другие желаемые разрешения здесь
		};
		
		if (SceneManager.GetActiveScene().name == "Menu")
		{
			Destroy(GameObject.Find("Player"));
			Destroy(GameObject.Find("UICanvas"));
			Destroy(GameObject.Find("Managers"));
		}
		
		PopulateResolutionDropdown();
		LoadSettings();
	}
	
	private Resolution CreateResolution(int width, int height, int refreshRate)
	{
		var numerator = (uint)refreshRate * 1000000;
		var denominator = (uint)1000000; // Явное приведение типа int к uint
		var refreshRateRatio = new RefreshRate() { numerator = numerator, denominator = denominator };
		return new Resolution() { width = width, height = height, refreshRateRatio = refreshRateRatio };
	}


	void PopulateResolutionDropdown()
	{
		resolutionDropdown.ClearOptions();
		List<string> options = new List<string>();

		foreach (var res in supportedResolutions)
		{
			string option = $"{res.width}x{res.height} {res.refreshRateRatio}Hz";
			options.Add(option);
		}

		resolutionDropdown.AddOptions(options);
		resolutionDropdown.RefreshShownValue();
	}

	public void SetFullScreen(bool isFullScreen)
	{
		Screen.fullScreen = isFullScreen;
		SaveSettings();
	}
	
	public void SetResolution(int resolutionIndex)
	{
		Resolution resolution = supportedResolutions[resolutionIndex];
		Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
		SaveSettings();
	}
	
	public void SetQuality(int qualityIndex)
	{
		QualitySettings.SetQualityLevel(qualityIndex);
		SaveSettings();
	}
	
	public void PlayGame()
	{
		/* SceneManager.LoadScene("SampleScene"); */
		Loading();
	}
	
	public void ExitGame()
	{
		Application.Quit();
	}
	
	public void Settings()
	{
		settingsPanel.SetActive(true);
	}
	
	public void Exit()
	{
		settingsPanel.SetActive(false);
	}
	
 	public void Loading()
	{
		loadingScreen.SetActive(true);
		
		StartCoroutine(LoadAsync());
	}
	
IEnumerator LoadAsync()
{
    AsyncOperation loadAsync = SceneManager.LoadSceneAsync("SampleScene");
    loadAsync.allowSceneActivation = false;

    float artificialProgress = 0f;
    float fillSpeed = 10f; // Скорость заполнения слайдера

    while (!loadAsync.isDone)
    {
        // Искусственно увеличиваем прогресс
        artificialProgress += fillSpeed * Time.deltaTime;
        artificialProgress = Mathf.Clamp(artificialProgress, 0f, 100f);
        progressLoading.value = artificialProgress;
        Debug.Log($"Загрузка: {artificialProgress}%"); // Отладочное сообщение

        // Проверяем, достиг ли искусственный прогресс 100% и реальный прогресс загрузки 0.9
        if (artificialProgress >= 100f && loadAsync.progress >= 0.9f)
        {
            loadAsync.allowSceneActivation = true;
        }
        yield return null;
    }
}
	
	public void SaveSettings()
	{
		int qualityIndex = qualityDropdown.value;
		int resolutionIndex = resolutionDropdown.value;
		bool isFullScreen = fullScreenToggle.isOn;
		
		QualitySettings.SetQualityLevel(qualityIndex);
		Resolution resolution = supportedResolutions[resolutionIndex];
		Screen.SetResolution(resolution.width, resolution.height, isFullScreen);
		Screen.fullScreen = isFullScreen;
		
		PlayerPrefs.SetInt("QualitySettingPreference", qualityIndex);
		PlayerPrefs.SetInt("ResolutionPreference", resolutionIndex);
		PlayerPrefs.SetInt("FullScreenPreference", isFullScreen ? 1 : 0);
		PlayerPrefs.Save();
	}

	public void LoadSettings()
	{
		int qualityIndex = PlayerPrefs.GetInt("QualitySettingPreference", 3);
		int resolutionIndex = PlayerPrefs.GetInt("ResolutionPreference", 0);
		bool isFullScreen = PlayerPrefs.GetInt("FullScreenPreference", 1) == 1;
		
		resolutionDropdown.value = resolutionIndex;
		qualityDropdown.value = qualityIndex;
		fullScreenToggle.isOn = isFullScreen;
		
		SetQuality(qualityIndex);
		SetResolution(resolutionIndex);
		SetFullScreen(isFullScreen);
	}
}
