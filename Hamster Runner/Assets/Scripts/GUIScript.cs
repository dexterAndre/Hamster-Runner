using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIScript : MonoBehaviour
{
    // Transitions
    private float _timer = 0f;
    public float _transitionDuration = 1f;

    // Menu logic
    [System.Serializable]
    public enum NextState
    {
        None,
        Start,
        Resume,
        QuitToDesktop,
        QuitToMainMenu
    };
    [Header("Menu Logic")]
    public NextState _nextState = NextState.None;

    // References
    [Header("Main Menu")]
    public GameObject pnl_MM;       // Main menu panel
    public Button btn_MM_start;     // Main menu start button
    public Button btn_MM_quit;      // Main menu quit button
    [Header("Pause Menu")]
    public GameObject pnl_PM;       // Pause menu panel
    public Button btn_PM_yes;       // Pause menu yes button
    public Button btn_PM_no;        // Pause menu no button

    private void Awake()
    {
        // Finding all button references if not set
        if (btn_MM_start == null)
            btn_MM_start = GameObject.Find("btn_start").GetComponent<Button>();
        if (btn_MM_quit == null)
            btn_MM_quit = GameObject.Find("btn_quit").GetComponent<Button>();
        if (btn_PM_yes == null)
            btn_PM_yes = GameObject.Find("btn_yes").GetComponent<Button>();
        if (btn_PM_no == null)
            btn_PM_no = GameObject.Find("btn_no").GetComponent<Button>();
    }

    private void Update()
    {
        if (_timer > 0f)
        {
            _timer += Time.deltaTime;
            if (_timer >= _transitionDuration)
            {
                _timer = 0f;
            }
        }
    }

    private void ChooseAction()
    {
        switch (_nextState)
        {
            case NextState.None:
            {
                
                break;
            }
            case NextState.Start:
            {
                break;
            }
            case NextState.Resume:
            {
                break;
            }
            case NextState.QuitToDesktop:
            {
                break;
            }
            case NextState.QuitToMainMenu:
            {
                break;
            }
        }
    }

    public void ButtonStart()
    {

    }

    public void ButtonQuit()
    {

    }

    public void ButtonYes()
    {

    }

    public void ButtonNo()
    {

    }
}
