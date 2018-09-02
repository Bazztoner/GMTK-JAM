using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    public void ToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ToCredits()
    {
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// TODO Load ASync
    /// </summary>
    public void ToGame()
    {
        SceneManager.LoadScene(2);
    }
}
