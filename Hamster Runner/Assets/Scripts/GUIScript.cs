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
    public GameState _gameState = GameState.Menu;
    public NextState _nextState = NextState.None;
    public GameManager _gameManager = null;

    // References
    [Header("Main Menu")]
    public GameObject pnl_MM;       // Main menu panel
    public Button btn_MM_start;     // Main menu start button
    public Button btn_MM_quit;      // Main menu quit button
    [Header("Pause Menu")]
    public GameObject pnl_PM;       // Pause menu panel
    public Button btn_PM_yes;       // Pause menu yes button
    public Button btn_PM_no;        // Pause menu no button
    [Header("Misc.")]
    public GameObject txt_score;    // Score text
    public GameObject txt_timer;    // Timer text
    public GameObject txt_finalScore;   // Final score text

    private void Awake()
    {
        // Finding all button references if not set
        if (_gameManager == null)
            _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        if (btn_MM_start == null)
            btn_MM_start = GameObject.Find("btn_start").GetComponent<Button>();
        if (btn_MM_quit == null)
            btn_MM_quit = GameObject.Find("btn_quit").GetComponent<Button>();
        if (btn_PM_yes == null)
            btn_PM_yes = GameObject.Find("btn_yes").GetComponent<Button>();
        if (btn_PM_no == null)
            btn_PM_no = GameObject.Find("btn_no").GetComponent<Button>();

        if (txt_score == null)
            txt_score = GameObject.Find("Score");
        if (txt_timer == null)
            txt_timer = GameObject.Find("Timer");
        if (txt_finalScore == null)
            txt_finalScore = GameObject.Find("Final Score");

        // Disables pause menu upon awake
        pnl_PM.SetActive(false);
        txt_score.SetActive(false);
        txt_timer.SetActive(false);
        txt_finalScore.SetActive(false);
    }

    private void Update()
    {
        // GUI input
        if (Input.GetButtonDown("Cancel") && _gameState != GameState.Menu)
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
                _gameState = GameState.Paused;
                _gameManager.Pause();
                _nextState = NextState.PauseMenu;
            }
            else if (_gameState == GameState.Paused)
            {
                // Resumes game
                Time.timeScale = 1f;
                _nextState = NextState.Resume;
            }
        }

        // Transition timer handling
        if (_timer > 0f)
        {
            // Transition functionalities
            switch (_nextState)
            {
                // If starting game, fade out main menu background
                case NextState.Start:
                    {
                        pnl_MM.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f - _timer / _transitionDuration);
                        break;
                    }
                // If pausing game, fade in pause menu (for now, just instantly pop up)
                case NextState.PauseMenu:
                    {
                        pnl_PM.SetActive(true);
                        pnl_PM.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                        break;
                    }
                // If resuming game, fade out pause menu
                case NextState.Resume:
                    {
                        pnl_PM.SetActive(true);
                        pnl_PM.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f - _timer / _transitionDuration);
                        break;
                    }
            }

            _timer += Time.deltaTime;
            if (_timer >= _transitionDuration)
            {
                _timer = 0f;
                ChooseAction();
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

                    // Setting game state
                    _gameState = GameState.Playing;

                    // Restoring color
                    pnl_MM.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);

                    // Disabling panel
                    pnl_MM.SetActive(false);

                    // Enabling gui text
                    txt_score.SetActive(true);
                    txt_timer.SetActive(true);

                    // Signalling Game Manager
                    _gameManager.Restart();

                    break;
                }
            case NextState.Resume:
                {
                    Debug.Log("Choose Action: Resume");

                    // Setting game state
                    _gameState = GameState.Playing;

                    // Restoring color
                    pnl_PM.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                    pnl_MM.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);

                    // Disabling panel
                    pnl_PM.SetActive(false);
                    pnl_MM.SetActive(false);

                    // Resumes time
                    Time.timeScale = 1f;

                    // Signalling Game Manager
                    _gameManager.Resume();

                    break;
                }
            case NextState.QuitToDesktop:
                {
                    Debug.Log("Choose Action: QuitToDesktop");

                    // Quitting application
                    Application.Quit();

                    break;
                }
            case NextState.QuitToMainMenu:
                {
                    Debug.Log("Choose Action: QuitToMainMenu");

                    // Setting game state
                    _gameState = GameState.Menu;

                    // Restoring color
                    pnl_PM.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                    pnl_MM.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);

                    // Disabling panel
                    pnl_PM.SetActive(false);
                    pnl_MM.SetActive(true);

                    break;
                }
            case NextState.PauseMenu:
                {
                    Debug.Log("Choose Action: PauseMenu");

                    // Setting game state
                    _gameState = GameState.Paused;

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
        Time.timeScale = 1f;
        StartTimer();
    }

    public void ButtonQuit()
    {
        _nextState = NextState.QuitToDesktop;
        Time.timeScale = 1f;
        _timer = _transitionDuration - Time.deltaTime;
    }

    public void ButtonYes()
    {
        _nextState = NextState.QuitToMainMenu;
        txt_score.SetActive(false);
        txt_timer.SetActive(false);
        txt_finalScore.SetActive(false);
        Time.timeScale = 1f;
        StartTimer();
    }

    public void ButtonNo()
    {
        _nextState = NextState.Resume;
        Time.timeScale = 1f;
        StartTimer();
    }
}
