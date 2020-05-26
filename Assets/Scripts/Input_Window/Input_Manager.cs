using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CodeMonkey.Utils;

//Input Manager contains 

public class Input_Manager : MonoBehaviour
{
    private UI_Input_Window commandInputWindow;
    private TMP_InputField inputField;

    private List<string> input_Logs = new List<string>();

    private bool windowBool = true;

    #region RunTimeEvents
    private void Awake()
    {
       if (commandInputWindow == null)
        {
            commandInputWindow = transform.Find("UI_Input_Window").GetComponent<UI_Input_Window>();
        }

       if (inputField == null)
        {
            inputField = transform.GetComponentInChildren<TMP_InputField>();
        }
    }

    private void Start()
    {
        input_Logs = new List<string>();
        
    }
    void Update()
    {
        OpenCommandWindow();
        PrintCommandItems();
        PrintCommandList();

    }

    #endregion

    #region Private Methods
    private void OpenCommandWindow()
    {
        if (Input.GetButtonDown("Input"))
        {
            if (windowBool)
            {
                UI_Input_Window.Static_ShowInputWindow("Type your commands here.", 
                    //onCancel
                    () => { UI_Input_Window.Static_HideInputWindow(); },
                    //onOk
                    (string inputText) => {input_Logs.Add(inputText);});

                windowBool = !windowBool;
            }
            else
            {
                UI_Input_Window.Static_HideInputWindow();
                windowBool = !windowBool;
            }

        }
    }
    #endregion

    #region Public Methods
    public void PrintCommandItems()
    {
        string temp = "";
        foreach (string a in input_Logs)
        {
            temp += a + " ";
            Debug.Log(temp);
        }
    }

    public void PrintCommandByIndex(int i)
    {
        print(input_Logs[i]);
    }

    public List<string> PrintCommandList()
    {
        return input_Logs;
    }

    #endregion
}
