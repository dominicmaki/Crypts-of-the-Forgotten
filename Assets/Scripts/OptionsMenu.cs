using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    public GameObject optionsMenu; // Reference to the options menu panel
    public Button settingsButton; // Settings button that opens the options menu
    public TMP_Dropdown resolutionDropdown; // TMP Dropdown for resolution (use TMP_Dropdown)
    public Slider masterVolumeSlider; // Slider for master volume
    public Slider sfxVolumeSlider; // Slider for SFX volume
    public Slider musicVolumeSlider; // Slider for music volume
    public Button applyButton; // Button to apply settings
    public Button closeButton; // Button to close the options menu

    private void Start()
{
    // Check for missing references and log errors if necessary
    if (optionsMenu == null)
    {
        Debug.LogError("Options Menu is not assigned in the Inspector!");
    }

    if (settingsButton == null)
    {
        Debug.LogError("Settings Button is not assigned in the Inspector!");
    }

    if (resolutionDropdown == null)
    {
        Debug.LogError("Resolution Dropdown is not assigned in the Inspector!");
    }

    if (masterVolumeSlider == null || sfxVolumeSlider == null || musicVolumeSlider == null)
    {
        Debug.LogError("One or more volume sliders are not assigned in the Inspector!");
    }

    if (applyButton == null || closeButton == null)
    {
        Debug.LogError("Apply or Close buttons are not assigned in the Inspector!");
    }

    // Hide the options menu initially
    if (optionsMenu != null)
    {
        optionsMenu.SetActive(false);
    }

    // Add listeners to buttons
    if (settingsButton != null)
    {
        settingsButton.onClick.AddListener(OpenOptionsMenu);
    }

    if (applyButton != null)
    {
        applyButton.onClick.AddListener(ApplySettings);
    }

    if (closeButton != null)
    {
        closeButton.onClick.AddListener(CloseOptionsMenu);
    }

    // Set initial volume levels
    if (masterVolumeSlider != null)
    {
        masterVolumeSlider.value = AudioListener.volume;
    }
    if (sfxVolumeSlider != null)
    {
        sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);
    }
    if (musicVolumeSlider != null)
    {
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
    }

    // Populate resolution dropdown
    PopulateResolutionDropdown();
}


    // Open the options menu
    private void OpenOptionsMenu()
    {
        optionsMenu.SetActive(true);
    }

    // Close the options menu
    private void CloseOptionsMenu()
    {
        optionsMenu.SetActive(false);
    }

    // Apply the settings from the UI
    private void ApplySettings()
{
    // Apply volume settings
    if (masterVolumeSlider != null)
    {
        AudioListener.volume = masterVolumeSlider.value;
    }
    else
    {
        Debug.LogError("Master volume slider is missing!");
    }

    if (sfxVolumeSlider != null)
    {
        PlayerPrefs.SetFloat("SFXVolume", sfxVolumeSlider.value);
    }
    else
    {
        Debug.LogError("SFX volume slider is missing!");
    }

    if (musicVolumeSlider != null)
    {
        PlayerPrefs.SetFloat("MusicVolume", musicVolumeSlider.value);
    }
    else
    {
        Debug.LogError("Music volume slider is missing!");
    }

    // Apply resolution settings
    if (resolutionDropdown != null)
    {
        int selectedResolutionIndex = resolutionDropdown.value;
        Resolution[] resolutions = Screen.resolutions;

        if (selectedResolutionIndex >= 0 && selectedResolutionIndex < resolutions.Length)
        {
            Resolution selectedResolution = resolutions[selectedResolutionIndex];
            Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);
        }
        else
        {
            Debug.LogError("Selected resolution is invalid.");
        }
    }
    else
    {
        Debug.LogError("Resolution dropdown is missing!");
    }

    // Save settings to PlayerPrefs
    PlayerPrefs.Save();
}


    // Populate the dropdown with available resolutions
    private void PopulateResolutionDropdown()
{
    if (resolutionDropdown == null)
    {
        Debug.LogError("Resolution dropdown is not assigned in the Inspector!");
        return;
    }

    Resolution[] resolutions = Screen.resolutions;
    if (resolutions.Length == 0)
    {
        Debug.LogError("No available screen resolutions found.");
        return;
    }

    resolutionDropdown.ClearOptions();

    // Add all available resolutions to the dropdown
    foreach (var resolution in resolutions)
    {
        string resolutionText = resolution.width + " x " + resolution.height;
        resolutionDropdown.options.Add(new TMP_Dropdown.OptionData(resolutionText)); // Use TMP_Dropdown.OptionData
    }

    // Set the current resolution as selected in the dropdown
    int currentResolutionIndex = GetCurrentResolutionIndex(resolutions);
    resolutionDropdown.value = currentResolutionIndex;
}


    // Get the index of the current screen resolution
    private int GetCurrentResolutionIndex(Resolution[] resolutions)
    {
        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                return i;
            }
        }
        return 0; // Default to the first resolution if not found
    }
}
