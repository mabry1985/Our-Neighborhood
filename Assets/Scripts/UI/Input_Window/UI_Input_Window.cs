
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
using TMPro;

public class UI_Input_Window : MonoBehaviour
{

    private Button_UI okBtn;
    private Button_UI cancelButton;
    private TMP_InputField inputText;

    private static UI_Input_Window instance;

    #region Runtime Events
    private void Awake()
    {
        instance = this;

        if (okBtn == null)
        {
            okBtn = transform.Find("SubmitButton").GetComponent<Button_UI>();
        }

        if (cancelButton == null)
        {
            cancelButton = transform.Find("CancelButton").GetComponent<Button_UI>();
        }

        if (inputText == null)
        {

            inputText = transform.Find("InputField").GetComponent<TMP_InputField>();
        }
        
        HideInputWindow();
    }
    #endregion


    #region Private Methods
    private void ShowInputWindow(string inputString, Action onCancel, Action<string> onOk)
    {
        gameObject.SetActive(true);
        inputText.text = inputString;

        cancelButton.ClickFunc = () =>
        {
            onCancel();
        };

        okBtn.ClickFunc = () =>
        {
            onOk(inputText.text);
            Debug.Log(inputText.text);
            inputText.text = null;
        };


    }
    private void HideInputWindow()
    {
        gameObject.SetActive(false);
    }

    #endregion


    #region Static Methods - Singletons
    public static void Static_ShowInputWindow(string inputString, Action onCancel, Action<string> onOk)
    {
        instance.ShowInputWindow(inputString, onCancel, onOk);

    }

    public static void Static_HideInputWindow()
    {
        instance.HideInputWindow();
    }
    #endregion
}
