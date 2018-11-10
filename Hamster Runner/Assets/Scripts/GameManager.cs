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
    private float _levelDuration = 60f;
    public bool _paused = true;

    [Header("Score")]
    public int _score = 0;
    GameObject txt_score = null;

    private void Awake()
    {
        _paused = true;
        _gameTimer = 0f;

        if (txt_score == null)
            txt_score = GameObject.Find("Score");
    }

    private void Update()
    {
        if (!_paused)
        {
            _gameTimer += Time.deltaTime;
        }

        // Setting score, very hacky, I know :(
        txt_score.GetComponent<Text>().text = "Score: " + _score.ToString();
    }

    public void Restart()
    {
        _paused = false;
        _gameTimer = 0f;
    }

    public void Resume()
    {
        _paused = false;
    }

    public void Pause()
    {
        _paused = true;
    }
}
