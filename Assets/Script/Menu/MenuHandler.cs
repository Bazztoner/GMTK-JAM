using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    Animator _an;
    bool _firstSelection = true;
    public bool inCredits;

    void Start()
    {
        _an = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            if (!inCredits)
            {
                if (_firstSelection)
                {
                    _an.Play("2");
                }
                else
                {
                    _an.Play("1");
                }
                _firstSelection = !_firstSelection;

                SoundMenu.Instance.PlayClip(SoundMenu.Audios.MENUBUTTON);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return))
        {
            if (inCredits)
            {
                ToMenu();
            }
            else
            {
                if (_firstSelection) ToGame();
                else ToCredits();
            }
        }
    }

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
