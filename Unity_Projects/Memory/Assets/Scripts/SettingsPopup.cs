using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPopup : MonoBehaviour
{
    [SerializeField] private SliderJoint2D timeOutSlider;

    private void Start()
    {
        timeOutSlider.value = PlayerPrefs.GetFloat("timeout", 3f);
    }
    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void OnSubmitName(string name) 
    {
        Debug.Log(name);
    }

    public void OnTimeOutValue(float timeout)
    {
        Debug.Log("Timeout: " + timeout);
        PlayerPrefs.SetFloat("timeout", timeout);
    }
}
