using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainScreen : MonoBehaviour
{
    [SerializeField] GameObject panel;


    public void CloseCredits()
    {
        panel.SetActive(!panel.activeInHierarchy);
    }
}
