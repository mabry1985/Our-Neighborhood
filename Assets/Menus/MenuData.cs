using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newMenuData", menuName = "NewMenu", order = 51)]
public class MenuData : ScriptableObject
{
    public Menu menuCanvas;
    public string tag;
    public string buttonTitle;
    public bool isOn;


}
