using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public float maxGameTime;
    public bool isBotEnabled = false, isBuffsEnabled;
    public Text textMaxGameTime;
    public GameObject minusButton, plusButton;

    private void Start()
    {
        maxGameTime = SettingsScript.maxGameTime;
        isBotEnabled = SettingsScript.isBotEnabled;
        isBuffsEnabled = SettingsScript.isBuffsEnabled;
    }
    void Update()
    {
        SettingsScript.maxGameTime = maxGameTime;
        SettingsScript.isBotEnabled = isBotEnabled;
        SettingsScript.isBuffsEnabled = isBuffsEnabled;

        textMaxGameTime.text = maxGameTime.ToString();

        if (maxGameTime <= 30)
        {
            maxGameTime = 30;
            minusButton.SetActive(false);
        }
        else
            minusButton.SetActive(true);

        if (maxGameTime >= 360)
        {
            maxGameTime = 360;
            plusButton.SetActive(false);
        }
        else
            plusButton.SetActive(true);

        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene(0);
    }

    public void AddTime()
    {
        maxGameTime += 30;
    }

    public void RemoveTime()
    {
        maxGameTime -= 30;
    }

    public void LoadScene(int SceneIndex)
    {
        SceneManager.LoadScene(SceneIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void IsBotEnabled()
    {
        isBotEnabled = !isBotEnabled;
    }

    public void IsBuffsEnabled()
    {
        isBuffsEnabled = !isBuffsEnabled;
    }

    public void PlayButton()
    {
        
    }
}
