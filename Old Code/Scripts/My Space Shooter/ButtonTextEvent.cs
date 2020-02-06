using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTextEvent : MonoBehaviour
{
    public Text innerText;
    public void MouseEnter()
    {
        innerText.gameObject.GetComponent<RectTransform>().localScale = new Vector3(1.05f, 1.05f, 1.05f);
        Debug.Log("Mouse in");
    }

    public void MouseExit()
    {
        innerText.gameObject.GetComponent<RectTransform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
        Debug.Log("Mouse out");
    }
}
