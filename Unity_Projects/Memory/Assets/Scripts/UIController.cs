using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreLabel;
    [SerializeField] private RawImage barImg;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private SettingsPopup settingsPopup;


    void Start()
    {
        settingsPopup.Close();
    }
    public void SetScore(int score)
    {
        scoreLabel.SetText(score.ToString());
    }

    public void OnOpenSettings()
    {
        settingsPopup.Open();
    }

    public void SetTimeOutValue(float timeout)
    {   
        barImg.transform.localScale = new Vector3(timeout, 1.0f);
    }

    public void EnableTimeOutBar(bool enable)
    {
        barImg.gameObject.SetActive(enable);
    }

    public void ShowPauseMenu(bool isPaused)
    {
        pauseMenu.SetActive(isPaused);
    }

    
}
