using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumbersOfInventory : MonoBehaviour
{
    public Image[] selectBox;

    private void Update()
    {
        PressedNumbers();
    }
    void PressedNumbers()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectBox[0].gameObject.SetActive(false);
            selectBox[1].gameObject.SetActive(true);
            selectBox[2].gameObject.SetActive(true);
            selectBox[3].gameObject.SetActive(true);
            selectBox[4].gameObject.SetActive(true);
            selectBox[5].gameObject.SetActive(true);
            selectBox[6].gameObject.SetActive(true);
            selectBox[7].gameObject.SetActive(true);
            print("1 E BASILDI");

        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectBox[0].gameObject.SetActive(true);
            selectBox[1].gameObject.SetActive(false);
            selectBox[2].gameObject.SetActive(true);
            selectBox[3].gameObject.SetActive(true);
            selectBox[4].gameObject.SetActive(true);
            selectBox[5].gameObject.SetActive(true);
            selectBox[6].gameObject.SetActive(true);
            selectBox[7].gameObject.SetActive(true);
            print("2 E BASILDI");

        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectBox[0].gameObject.SetActive(true);
            selectBox[1].gameObject.SetActive(true);
            selectBox[2].gameObject.SetActive(false);
            selectBox[3].gameObject.SetActive(true);
            selectBox[4].gameObject.SetActive(true);
            selectBox[5].gameObject.SetActive(true);
            selectBox[6].gameObject.SetActive(true);
            selectBox[7].gameObject.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            selectBox[0].gameObject.SetActive(true);
            selectBox[1].gameObject.SetActive(true);
            selectBox[2].gameObject.SetActive(true);
            selectBox[3].gameObject.SetActive(false);
            selectBox[4].gameObject.SetActive(true);
            selectBox[5].gameObject.SetActive(true);
            selectBox[6].gameObject.SetActive(true);
            selectBox[7].gameObject.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            selectBox[0].gameObject.SetActive(true);
            selectBox[1].gameObject.SetActive(true);
            selectBox[2].gameObject.SetActive(true);
            selectBox[3].gameObject.SetActive(true);
            selectBox[4].gameObject.SetActive(false);
            selectBox[5].gameObject.SetActive(true);
            selectBox[6].gameObject.SetActive(true);
            selectBox[7].gameObject.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            selectBox[0].gameObject.SetActive(true);
            selectBox[1].gameObject.SetActive(true);
            selectBox[2].gameObject.SetActive(true);
            selectBox[3].gameObject.SetActive(true);
            selectBox[4].gameObject.SetActive(true);
            selectBox[5].gameObject.SetActive(false);
            selectBox[6].gameObject.SetActive(true);
            selectBox[7].gameObject.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            selectBox[0].gameObject.SetActive(true);
            selectBox[1].gameObject.SetActive(true);
            selectBox[2].gameObject.SetActive(true);
            selectBox[3].gameObject.SetActive(true);
            selectBox[4].gameObject.SetActive(true);
            selectBox[5].gameObject.SetActive(true);
            selectBox[6].gameObject.SetActive(false);
            selectBox[7].gameObject.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            selectBox[0].gameObject.SetActive(true);
            selectBox[1].gameObject.SetActive(true);
            selectBox[2].gameObject.SetActive(true);
            selectBox[3].gameObject.SetActive(true);
            selectBox[4].gameObject.SetActive(true);
            selectBox[5].gameObject.SetActive(true);
            selectBox[6].gameObject.SetActive(true);
            selectBox[7].gameObject.SetActive(false);
        }
    }


}
