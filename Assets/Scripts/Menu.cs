using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Animator animator;    
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject bubbles;
    private InputSystem_Actions actions;
    private bool paused = false;

    private void OnEnable()
    {
        actions = new InputSystem_Actions();
        actions.Enable();
    }

    private void OnDisable()
    {
        actions.Disable();
    }

    // On start if something is found in playerprefs load it
    // otherwise, set the volume for all.
    void Start()
    {
        animator.SetBool("Start", true);
        animator.SetBool("End", true);
        StartCoroutine(AnimationStart());
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMusicVolume();
            SetSFXVolume();
        }

        actions.Player.Echap.performed += EchapOnperformed;
    }

    // Pause game during the gameplay
    private void EchapOnperformed(InputAction.CallbackContext obj)
    {
        if (paused == true)
        {
            CloseMenu();
        }
        else
        {
            paused = true;
            bubbles.SetActive(true);
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void CloseMenu()
    {
        paused = false;
        bubbles.SetActive(false);
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    // Wait for ESCAPE pressed for openning pause menu
    void Update()
    {
        
    }

    // Set the volume of the music and save in a playerpref
    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        audioMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    // Set the volume of the SFX and save in a playerpref
    public void SetSFXVolume()
    {
        float volume = sfxSlider.value;
        audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    // Load the volume of the music and SFX
    private void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        SetMusicVolume();
        SetSFXVolume();
    }

    // Resume game from the menu pause
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
    }
    
    // Quit game from the menu pause
    public void QuitGame()
    {
        Application.Quit();
    }


    public void Play()
    {
        animator.SetBool("Start", false);
        animator.SetBool("End", false);
        animator.SetBool("Start", true);
        StartCoroutine(ChangeScene());
    }

    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(1.3f);
        SceneManager.LoadScene(1);
    }

    IEnumerator AnimationStart()
    {
        yield return new WaitForSeconds(1.3f);
        animator.SetBool("Start", true);
        animator.SetBool("End", true);
    }
}
