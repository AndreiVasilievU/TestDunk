using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class MainCanvas : MonoBehaviour
{
    public static MainCanvas instance;
    
    [SerializeField] private GameObject[] screens;
    [SerializeField] private Rigidbody2D ball;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI starCountText;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private Button settingsButton;

    private Camera mainCamera;
    private bool isGray;

    public void Start()
    {
        instance = this;
        ball.bodyType = RigidbodyType2D.Static;
        mainCamera = Camera.main;

        if (PlayerPrefs.HasKey("SaveLoadBackground"))
        {
            var intColor = PlayerPrefs.GetInt("SaveLoadBackground");
            if (intColor == 1)
            {
                mainCamera.backgroundColor = new Color(0.690032f, 0.735849f, 0.733857f);
                isGray = true;
            }
            else
            {
                mainCamera.backgroundColor = Color.black;
                isGray = false;
            }
        }
        else
        {
            PlayerPrefs.SetInt("SaveLoadBackground", 1);
            mainCamera.backgroundColor = new Color(0.690032f, 0.735849f, 0.733857f);
            isGray = true;
        }

        foreach (var screen in screens)
        {
            screen.SetActive(false);
        }

        screens[0].SetActive(true);
    }

    public void UpdateScore()
    {
        scoreText.text = (CounterManager.instance.ringsCounter + CounterManager.instance.bounceCounter).ToString();
    }

    public void UpdateStarScore()
    {
        CounterManager.instance.SetStarCounter();
        starCountText.text = CounterManager.instance.starCounter.ToString();
    }
    public void OnPlayButton()
    {
        AudioManager.instance.PlayOneShot(Clips.Button);

        screens[0].SetActive(false);
        screens[1].SetActive(true);
        ball.bodyType = RigidbodyType2D.Dynamic;
    }

    public void OnRetryButton()
    {
        SceneManager.LoadScene(0);
    }

    public void OnGameOver()
    {
        screens[2].SetActive(true);
        settingsButton.gameObject.SetActive(false);
    }

    public void GoToPlayFromSettings()
    {
        Time.timeScale = 1;

        AudioManager.instance.PlayOneShot(Clips.Button);

        StartCoroutine(HidePanel(0.3f));
        settingsButton.interactable = true;
    }

    public void OnSettings()
    {
        settingsPanel.SetActive(true);
        settingsPanel.transform.localScale = Vector3.one;
        settingsButton.interactable = false;

        AudioManager.instance.PlayOneShot(Clips.Button);
        Time.timeScale = 0;
    }

    public void OnChangeColor()
    {
        AudioManager.instance.PlayOneShot(Clips.Button);

        if (!isGray)
        {
            mainCamera.backgroundColor = new Color(0.690032f, 0.735849f, 0.733857f);
            isGray = true;
            PlayerPrefs.SetInt("SaveLoadBackground", 1);
        }
        else
        {
            mainCamera.backgroundColor = Color.black;
            isGray = false;
            PlayerPrefs.SetInt("SaveLoadBackground", 0);
        }
    }
    
    IEnumerator HidePanel(float time)
    {
        settingsPanel.transform.DOScale(0, time);
        yield return new WaitForSeconds(time);
        settingsPanel.SetActive(false);
    }
}