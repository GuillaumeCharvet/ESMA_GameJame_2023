using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Observer : MonoBehaviour
{
    [SerializeField] private bool _CameraIsActive;
    [SerializeField] private bool _CameraOnConnection;
    [SerializeField] private bool _ActiveTimer;

    [SerializeField] private int _CDCamConnection;
    [SerializeField] private int _CDMinCamEnable;
    [SerializeField] private int _CDMaxCamEnable;
    [SerializeField] private int _CDMinCamDisable;
    [SerializeField] private int _CDMaxCamDisable;

    [Header("Animation Cam")]
    [SerializeField] private Animator _animCam;

    [Header("Info Debug")]
    [SerializeField] private float _TimerObservation;

    [SerializeField] private bool CamStop;

    private void Start()
    {
        _CameraIsActive = true;
        _ActiveTimer = false;
        _TimerObservation = 0f;

    }

    private void Update()
    {
        if (CamStop)
        {
            if (_TimerObservation > 0)
            {
                _TimerObservation = _TimerObservation - Time.deltaTime;
            }
       
            if (_ActiveTimer && _TimerObservation <= 0)
            {
                StartCoroutine(ConnetionCamera());
            }

            if(_CameraOnConnection)
            {
               //Animation cam
            }
        }

    }


    public void ActiveTimer()   //if(3 obj fabriqué){ _ActiveTimer = true }
    {
        _ActiveTimer = true;
    }

    IEnumerator ConnetionCamera()
    {
        _ActiveTimer = false;
        _CameraOnConnection = true;

        yield return new WaitForSeconds(_CDCamConnection);

        _CameraOnConnection = false;
        ActiveCam();
        if (_CameraIsActive)
            ResetTimerCamEnable();
        else
            ResetTimerCamDisable();
    }

    private void ActiveCam()
    {
        if(CamStop)
        {
            _CameraIsActive = !_CameraIsActive;
        }
    }

    private void ResetTimerCamEnable()
    {
        _TimerObservation = Random.Range(_CDMinCamEnable, _CDMaxCamEnable);
        _ActiveTimer = true;
    }
    private void ResetTimerCamDisable()
    {
        _TimerObservation = Random.Range(_CDMinCamDisable, _CDMaxCamDisable);
        _ActiveTimer = true;
    }


}
