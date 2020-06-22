using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] allMenus;
    private GameObject newMenuObject;
    private Menu newMenu;
    [SerializeField]
    private GameObject mainMenuCanvas;
    [SerializeField]
    private GameObject mainMenuPanel;
    [SerializeField]
    private GameObject mainMenuButton;


    void Awake()
    {
        for (int i = 0; i < allMenus.Length; i++)
        {
            newMenuObject = allMenus[i];
            newMenu = newMenuObject.GetComponent<Menu>();
            newMenu.gameObject.SetActive(false);
            newMenu.info.isOn = false;
        }
    }

    public void ActivateMenu(GameObject menu)
    {
        newMenu = menu.GetComponent<Menu>();
        newMenu.info.isOn = newMenu.gameObject.activeInHierarchy;

        if (!newMenu.info.isOn)
        {
            mainMenuCanvas.SetActive(false);
            menu.SetActive(true);
            newMenu.info.isOn = true;
            
        }
        
    }


    public void CloseSubMenu()
    {

        for (int i = 0; i < allMenus.Length; i++)
        {
            newMenuObject = allMenus[i];
            newMenu = newMenuObject.GetComponent<Menu>();
            newMenu.gameObject.SetActive(false);
            newMenu.info.isOn = false;
        }
        mainMenuCanvas.SetActive(true);

    }

    public void CloseMainMenu()
    {
        mainMenuPanel.SetActive(false);
        mainMenuButton.SetActive(true);
    }

    public void OpenMainMenu()
    {
        mainMenuButton.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
}
