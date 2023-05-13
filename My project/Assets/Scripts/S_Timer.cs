using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class S_Timer : MonoBehaviour
{

    [SerializeField] private TMP_Text _timerTxt;
    [SerializeField] private float DefaultTime;

    [Header("Info Debug")]
    [SerializeField] private bool _stopTimer = true;
    
    private float _minutes, _seconds;
    private float _time;
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

            _time = DefaultTime - (Time.time - _timeStart);
            
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
       

    }

    public void TimerStop()
    {
        _stopTimer = !_stopTimer;
    }

}
