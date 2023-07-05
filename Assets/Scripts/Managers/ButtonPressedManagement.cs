using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressedManagement : MonoBehaviour
{
    void PressedManagement()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            //InventoryPanel
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            //SkillPanel 
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            //CraftPanel
        }
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            //QuestPanel
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            //SettingsPanel
        }
    }
}
