using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class UIController : MonoBehaviour
{
    public Image healthBar;
    public TextMeshProUGUI waveNumber;
    public Image weapon1Icon;
    public Image weapon2Icon;
    public Image weapon3Icon;
    public GameObject pausePanel;
    public GameObject gameOverPanel;
    public AudioSource sfxAudioSource;
    public AudioClip gameOverClip;


    private bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void Update()
    {
        // Check if the player presses the ESC key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Toggle between pause and unpause
            if (isPaused)
                ResumeGame();
            else
                Pause();
        }
    }
    public void Exit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
    private void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        Debug.Log("Game Paused");
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Debug.Log("Game Resumed");

    }
    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);

    }
    public void GameOver()
    {
        sfxAudioSource.PlayOneShot(gameOverClip);
        healthBar.fillAmount = 0f;
        Time.timeScale = 0f;


        gameOverPanel.SetActive(true);
    }
    private IEnumerator Reload(Image weaponIcon, float fillDuration)
    {
        float timer = 0f;

        while (timer < fillDuration)
        {
            timer += Time.deltaTime;

            float fillAmount = Mathf.Clamp01(timer / fillDuration);

            weaponIcon.fillAmount = 1.0f - fillAmount;

            yield return null;
        }

        weaponIcon.fillAmount = 0f;
    }
    
    public void Reload1(float fillDuration)
    {
        StartCoroutine( Reload(weapon1Icon, fillDuration));
    }
    public void Reload2(float fillDuration)
    {
        StartCoroutine(Reload(weapon2Icon, fillDuration));

    }
    public void Reload3(float fillDuration)
    {

        StartCoroutine(Reload(weapon3Icon, fillDuration));
    }

   
    public void UpdateWaveNumber(int number)
    {
        waveNumber.text = "#" + (number-1).ToString();
    }
    public void UpdateHealthBar(float value)
    {
        print(value);
        healthBar.fillAmount = value;
    }
}
