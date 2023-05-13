using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Observer : MonoBehaviour
{
    [SerializeField] private bool _CameraIsActive;
    [SerializeField] private bool _CameraOnConnection;
    [SerializeField] private bool _ActiveTimerCam;

    [SerializeField] private int _CDCamConnection;
    [SerializeField] private int _CDMinCamEnable;
    [SerializeField] private int _CDMaxCamEnable;
    [SerializeField] private int _CDMinCamDisable;
    [SerializeField] private int _CDMaxCamDisable;

    [Header("Animation Cam")]
    [SerializeField] private Animator _animCam;
    [SerializeField] private GameObject _led;
    [SerializeField] private Material _ledON;
    [SerializeField] private Material _ledOFF;

    [Header("Info Debug")]
    [SerializeField] private float _TimerObservation;

    [SerializeField] private bool CamStop;

    private void Start()
    {
        _CameraIsActive = true;
        _ActiveTimerCam = false;
        _TimerObservation = 0f;

    }

    private void Update()
    {

        if (_TimerObservation > 0)
        {
            _TimerObservation = _TimerObservation - Time.deltaTime;
        }

        if (_ActiveTimerCam && _TimerObservation <= 0)
        {
            StartCoroutine(ConnetionCamera());
        }

        if (_CameraOnConnection)
            _animCam.SetBool("IsConnect", true);
        else
            _animCam.SetBool("IsConnect", false);
        if (_CameraIsActive)
            _animCam.SetBool("IsActive", true);
        else
            _animCam.SetBool("IsActive", false);

    }
        public void ActiveTimer()   //if(3 obj fabriqué){ _ActiveTimer = true }
    {
        _ActiveTimerCam = true;
    }

    IEnumerator ConnetionCamera()
    {
        _ActiveTimerCam = false;
        _CameraOnConnection = true;
        //_animCam.SetBool("IsConnect", true);
        yield return new WaitForSeconds(_CDCamConnection);
        _CameraOnConnection = false;
        //_animCam.SetBool("IsConnect", false);

        ActiveCam();

        if (_CameraIsActive)
        {
            ResetTimerCamEnable();
            //_animCam.SetBool("IsActive", true);
            
        }
        else
        {
            ResetTimerCamDisable();
            //_animCam.SetBool("IsActive", false);
           
        }
    }



    private void ActiveCam()
    {
            _CameraIsActive = !_CameraIsActive;
    }

    private void ResetTimerCamEnable()
    {
        _TimerObservation = Random.Range(_CDMinCamEnable, _CDMaxCamEnable);
        _ActiveTimerCam = true;
    }
    private void ResetTimerCamDisable()
    {
        _TimerObservation = Random.Range(_CDMinCamDisable, _CDMaxCamDisable);
        _ActiveTimerCam = true;
    }


}
