using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class S_Timer : MonoBehaviour
{

    [SerializeField] private S_ManagerGame S_ManagerGame;
    [SerializeField] private TMP_Text _timerTxt;
    [SerializeField] private float DefaultTime;

    [Header("Info Debug")]
    //[SerializeField] private bool _stopTimer = true;

    public bool _isTimerStopped;

    private float _minutes, _seconds;
    public float _time;
    /*
    private float _timeStop;
    private float _timeStart;

    private void Start()
    {
        _time = Time.time;
    }
    private void Update()
    {
        if (!_stopTimer)
        {
            //_timeIncrement = _timeStop + Time.time - _timeStart;

            _time = ( DefaultTime - Time.time );

            _minutes = (int)(_time / 60f) % 60;
            _seconds = (int)(_time % 60f);

            if (_seconds < 10)// ajoute un 0 devant les 10 premiere sec
            {
                _timerTxt.text = "0" + _minutes + ":" + "0" + _seconds;
            }
            else
            {
                _timerTxt.text = _minutes + ":" + _seconds;
            }
        }

        if (_stopTimer)
            Time.timeScale = 0f;
        else 
            Time.timeScale = 1f;

        if(_time < 0)
        {
            S_ManagerGame.EndGame();
        }

    }

    public void TimerStopStart()
    {
        _stopTimer = !_stopTimer;
    }
    
}*/

    private void Start()
    {
        _time = DefaultTime;
        StartCoroutine(TimerCoroutine());
    }
    private void Update()
    {
        if (_time < 0)
        {
            S_ManagerGame.EndGame();
            _isTimerStopped = false;
            _time = 360f;
        }
    }

    private IEnumerator TimerCoroutine()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitWhile(() => _isTimerStopped);
            _time = (_time - Time.deltaTime);
            

            _minutes = (int)(_time / 60f) % 60;
            _seconds = (int)(_time % 60f);

            if (_seconds < 10)
            {
                _timerTxt.text = "0" + _minutes + ":" + "0" + _seconds;
            }
            else
            {
                _timerTxt.text = _minutes + ":" + _seconds;
            }
        }
    }

    public void TimerStopStart()
    {
        _isTimerStopped = !_isTimerStopped;
    }
}