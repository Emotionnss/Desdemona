using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject Title;
    [SerializeField] private GameObject Help;
    [SerializeField] private GameObject Options;
    public GameObject Bleed;
    public Animator Eye;

    public void Start()
    {
        Bleed.SetActive(false);
    }

    public void LevelStart()
    {
        StartCoroutine(NewGame());
    }

    public void Instruction()
    {
        Eye.SetBool("Opening", true);
        StartCoroutine(Wait(2, 1));
    }

    public void Option()
    {
        Eye.SetBool("Opening", true);
        StartCoroutine(Wait(2, 2));
    }

    public void LevelChange(int LevelIndex)
    {
        SceneManager.LoadScene(LevelIndex);
    }

    public void Back()
    {
        Eye.SetBool("Opening", true);
        StartCoroutine(Wait(2, 3));
    }

    public void quit()
    {
        Application.Quit();
    }

    public IEnumerator NewGame()
    {
        Bleed.SetActive(true);
        yield return new WaitForSeconds(2);
        LevelChange(1);
    }

    public IEnumerator Wait(float SecondsToWait, int ID)
    {
        yield return new WaitForSeconds(SecondsToWait);
        switch (ID)
        {
            case 1:
                Title.SetActive(false);
                Help.SetActive(true);
                Eye.SetBool("Opening", false);
                break;
            case 2:
                Title.SetActive(false);
                Options.SetActive(true);
                Eye.SetBool("Opening", false);
                break;
            case 3:
                Title.SetActive(true);
                Options.SetActive(false);
                Help.SetActive(false);
                Eye.SetBool("Opening", false);
                break;
        }
    }
}
