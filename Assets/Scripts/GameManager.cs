using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game variables")]
    [Tooltip("Time in seconds")]
    public float RoundTime;

    [Header("Game audioClips")]
    public AudioClip GameWinSound;
    public AudioClip GameLoseSound;

    [Header("Text boxes references")]
    public TextMeshProUGUI ScoreTextbox;
    public string ScoreTextPrefix;

    public TextMeshProUGUI AmmoTextbox;
    public string AmmoTextPrefix;

    public TextMeshProUGUI HealthTextbox;
    public string HealthTextPrefix;

    public TextMeshProUGUI TimeLeftTextbox;
    public string TimeLeftTextPrefix;

    public GameObject GameOverUI;
    public GameObject WinUI;

    [HideInInspector]
    public bool isGameOver;

    private int score;
    private AudioSource audioSource;

    public Image AmmoBar;

    public Image HealthBar;

    // Use this for initialization
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }


        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        
        WinUI.SetActive(false);
        GameOverUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver)
            return;

        UpdateTimeLeft();

        if (Input.GetKeyDown(KeyCode.F1))
        {
            SetGameOver(true);
        }

        if(Input.GetKey(KeyCode.R))
        {
            ResetGame();
        }
    }

    public void UpdateScore(int _score, AudioClip audioClip)
    {
        score += _score;
        ScoreTextbox.text = ScoreTextPrefix + score;

        audioSource.PlayOneShot(audioClip);
    }

    public void UpdateAmmo(int ammo)
    {
        AmmoTextbox.text = AmmoTextPrefix + ammo;
        AmmoBar.GetComponent<RectTransform>().localPosition = new Vector3(Mathf.Clamp((50f - (float)ammo) * -0.038f, -2, 0), AmmoBar.GetComponent<RectTransform>().localPosition.y, AmmoBar.GetComponent<RectTransform>().localPosition.z);
    }

    public void UpdateHealth(int health)
    {

        if (health <= 0 && !isGameOver)
        {
            health = 0;
            SetGameOver(false);
        }

        HealthTextbox.text = HealthTextPrefix + health;
        HealthBar.GetComponent<RectTransform>().localPosition = new Vector3(Mathf.Clamp((100f - (float)health) * -0.019f, -2, 0), HealthBar.GetComponent<RectTransform>().localPosition.y, HealthBar.GetComponent<RectTransform>().localPosition.z);
    }

    public void UpdateTimeLeft()
    {
        if (RoundTime <= 0)
        {
            RoundTime = 0;
            TimeLeftTextbox.text = TimeLeftTextPrefix + "\n00:00:00";
            SetGameOver(false);

            return;
        }

        RoundTime -= Time.deltaTime;

        int minutes = (int)RoundTime / 60;
        int seconds = (int)RoundTime - 60 * minutes;
        int milliseconds = (int)(100 * (RoundTime - minutes * 60 - seconds));

        TimeLeftTextbox.text = TimeLeftTextPrefix + '\n' + string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
    }

    public void SetGameOver(bool isWin)
    {

        isGameOver = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        GameObject.Find("FPSPlayer").GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false;

        if (isWin)
        {
            CameraScript.bgm.Stop();
            audioSource.PlayOneShot(GameWinSound);
            WinUI.SetActive(true);
        }
        else if (!isWin)
        {
            CameraScript.bgm.Stop();
            audioSource.PlayOneShot(GameLoseSound);
            GameOverUI.SetActive(true);
        }
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMain()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public int GetScore()
    {
        return score;
    }
}
