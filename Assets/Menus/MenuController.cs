using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private Menu[] allMenus;
    private Menu newMenu;
    [SerializeField]
    private GameObject mainMenuPanel;


    public void ActivateInventory()
    {
        newMenu = allMenus[0];

        newMenu.info.isOn = newMenu.gameObject.activeInHierarchy;
        if (!newMenu.info.isOn)
        {
            mainMenuPanel.SetActive(false);
            newMenu.gameObject.SetActive(true);
            newMenu.info.isOn = true;
        }
        
    }

    public void ActivateMenu()
    {
        string newMenuTag = GetComponent<Menu>().info.tag;
        Debug.Log(newMenuTag);
        string tempMenuTag = "";
        for (int i = 0; i < allMenus.Length; i++)
        {
            tempMenuTag = allMenus[i].info.tag;
            if (tempMenuTag == newMenuTag)
            {
                newMenu.info.isOn = newMenu.gameObject.activeInHierarchy;
                if (!newMenu.info.isOn)
                {
                    mainMenuPanel.SetActive(false);
                    newMenu.gameObject.SetActive(true);
                    newMenu.info.isOn = true;
                }
                break;
            }
        }

    }






    public void CloseSubMenu()
    {

        for (int i = 0; i < allMenus.Length; i++)
        {
            newMenu = allMenus[i];
            newMenu.gameObject.SetActive(false);
            newMenu.info.isOn = false;
        }
        mainMenuPanel.SetActive(true);

    }
    void Awake()
    {
        for (int i = 0; i < allMenus.Length; i++)
        {
            newMenu = allMenus[i];
            newMenu.gameObject.SetActive(false);
            newMenu.info.isOn = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
