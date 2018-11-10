using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Time")]
    [SerializeField]
    private float _gameTimer = 0f;
    [SerializeField]
    private float _levelDuration = 5f;
    public bool _paused = true;

    // Final result
    private float _resultTimer = 0f;
    private float _resultDuration = 5f;

    [Header("Score")]
    public int _score = 0;
    public GameObject txt_score = null;
    public GameObject txt_timer = null;
    public GameObject txt_finalScore = null;

    [Header("References")]
    public GUIScript guiScript = null;
    public AudioSource _backgroundMusic = null;

    private void Awake()
    {
        _paused = true;
        _gameTimer = 0f;

        if (txt_score == null)
            txt_score = GameObject.Find("Score");
        if (txt_timer == null)
            txt_timer = GameObject.Find("Timer");
        if (txt_finalScore == null)
            txt_finalScore = GameObject.Find("Final Score");

        if (guiScript == null)
            guiScript = GameObject.Find("GUI").GetComponent<GUIScript>();
    }

    private void Update()
    {
        if (!_paused)
        {
            if (_resultTimer <= 0f)
            {
                _gameTimer += Time.deltaTime;
                if (_gameTimer >= _levelDuration)
                {
                    // End of round
                    _resultTimer = Time.deltaTime;
                    txt_finalScore.GetComponent<Text>().text = "Final Score: " + _score.ToString();
                    txt_finalScore.SetActive(true);
                }
            }
        }

        if (_resultTimer > 0f)
        {
            _resultTimer += Time.deltaTime;
            if (_resultTimer >= _resultDuration)
            {
                // Back to main menu
                guiScript.ButtonYes();
            }
        }

        // Setting score, very hacky, I know :(
        txt_score.GetComponent<Text>().text = "Score: " + _score.ToString();
        txt_timer.GetComponent<Text>().text = "Timer: " + ((int)((_levelDuration - _gameTimer))).ToString();
    }

    public void Restart()
    {
        _paused = false;
        _gameTimer = 0f;

        if (_backgroundMusic != null)
            _backgroundMusic.Play();
    }

    public void Resume()
    {
        _paused = false;
    }

    public void Pause()
    {
        _paused = true;
    }

    public void EndRound()
    {
        _paused = false;
        _gameTimer = _levelDuration;
        _resultTimer = Time.deltaTime;
    }
}
