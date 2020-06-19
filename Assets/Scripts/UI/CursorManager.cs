using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{

    // Update is called once per frame
    public bool cursorVisible = true;

    // Update is called once per frame
    void Update()
    {
        if (cursorVisible)
        {
            Cursor.visible = true;
        }
        else
        {
            Cursor.visible = false;
        }
    }
}
