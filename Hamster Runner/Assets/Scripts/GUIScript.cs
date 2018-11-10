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
        QuitToMainMenu,
        PauseMenu
    };
    [System.Serializable]
    public enum GameState
    {
        Playing, 
        Paused,
        Menu
    };
    [Header("Menu Logic")]
    public GameState _gameState = GameState.Playing;
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

        // Disables pause menu upon awake
        pnl_PM.SetActive(false);
    }

    private void Update()
    {
        // GUI input
        if (Input.GetButtonDown("Cancel"))
        {
            if (_timer > 0f)
            {
                // Complete any ongoing transitions
                ChooseAction();
                _timer = Time.deltaTime;
            }
            StartTimer();

            if (_gameState == GameState.Playing)
            {
                // Queues up for pause menu transition
                Time.timeScale = 0f;
                _nextState = NextState.PauseMenu;
            }
            else if (_gameState == GameState.Paused)
            {
                // Resumes game
                _nextState = NextState.Resume;
            }
        }

        // Transition timer handling
        if (_timer > 0f)
        {
            _timer += Time.deltaTime;
            if (_timer >= _transitionDuration)
            {
                _timer = 0f;
                ChooseAction();
            }

            // Transition functionalities
            switch (_nextState)
            {
                // If starting game, fade out main menu background
                case NextState.Start:
                    {
                        pnl_MM.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f - _timer / _transitionDuration);
                        break;
                    }
            }
        }
    }

    private void StartTimer()
    {
        _timer += Time.deltaTime;
    }

    private void ChooseAction()
    {
        switch (_nextState)
        {
            case NextState.None:
                {
                    Debug.Log("Choose Action: None");



                    break;
                }
            case NextState.Start:
                {
                    Debug.Log("Choose Action: Start");

                    // Restoring color
                    pnl_MM.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);

                    // Disabling panel
                    pnl_MM.SetActive(false);

                    break;
                }
            case NextState.Resume:
                {
                    Debug.Log("Choose Action: Resume");

                    // Resumes time
                    Time.timeScale = 1f;

                    break;
                }
            case NextState.QuitToDesktop:
                {
                    Debug.Log("Choose Action: QuitToDesktop");



                    break;
                }
            case NextState.QuitToMainMenu:
                {
                    Debug.Log("Choose Action: QuitToMainMenu");



                    break;
                }
            case NextState.PauseMenu:
                {
                    Debug.Log("Choose Action: PauseMenu");



                    break;
                }
            default:
                {
                    Debug.Log("Choose Action: Default");



                    break;
                }
        }
    }

    public void ButtonStart()
    {
        _nextState = NextState.Start;
        _timer = Time.deltaTime;
    }

    public void ButtonQuit()
    {
        _nextState = NextState.QuitToDesktop;
        _timer = Time.deltaTime;
    }

    public void ButtonYes()
    {
        _nextState = NextState.QuitToMainMenu;
        _timer = Time.deltaTime;
    }

    public void ButtonNo()
    {
        _nextState = NextState.Resume;
        _timer = Time.deltaTime;
    }
}
