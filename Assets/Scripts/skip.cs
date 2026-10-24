using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class skip : MonoBehaviour
{
    public void MouseClick(string buttonType)
    {
        if (buttonType == "Skip")
        {
            SceneManager.LoadScene("Gameplay");
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
