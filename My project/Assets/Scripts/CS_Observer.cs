using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Observer : MonoBehaviour
{
    [SerializeField] private S_Television S_Television;
    [SerializeField] private ItemsManager ItemsManager;
    [SerializeField] private S_ManagerGame S_ManagerGame;

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
    [Header("TV")]
    [SerializeField] private Material _ledON;
    [SerializeField] private Material _ledOFF;
    [SerializeField] private GameObject _led1;
    [SerializeField] private GameObject _led2;
    [SerializeField] private GameObject _led3;


    public bool StopGame;
    [Header("Info Debug")]
    [SerializeField] private float _TimerObservation;
    [SerializeField] private bool CamStop;
    public int NbrVie = 3;

    private void Start()
    {
        _CameraIsActive = true;
        _ActiveTimerCam = false;
        _TimerObservation = 0f;

        StopGame = false;

    }

    private void Update()
    {
        if (!StopGame)
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


            if (_CameraIsActive)
            {
                if (ItemsManager.itemInHand != null)
                {
                    if (!ItemsManager.itemInHand.SO_Item.authorized)
                    {
                        Debug.Log("Noooooooooooo !!");
                        TakeDmg();
                    }
                }
                if (ItemsManager.itemInEtabliG != null)
                {
                    if (!ItemsManager.itemInEtabliG.SO_Item.authorized)
                    {
                        Debug.Log("Naaaaaaaaaaaa!!");
                        TakeDmg();
                    }
                }
                if (ItemsManager.itemInEtabliG != null)
                {
                    if (!ItemsManager.itemInEtabliD.SO_Item.authorized)
                    {
                        Debug.Log("Niiiiiiiiiiiii !!");
                        TakeDmg();
                    }
                }
            }
        }
    }
    public void ActiveTimer()   //if(3 obj fabriqué){ _ActiveTimer = true }
    {
        _ActiveTimerCam = true;
    }

    IEnumerator ConnetionCamera()
    {
        _ActiveTimerCam = false;
        _CameraOnConnection = true;

        yield return new WaitForSeconds(_CDCamConnection);
        _CameraOnConnection = false;


        ActiveCam();

        if (_CameraIsActive)
        {
            ResetTimerCamEnable();

            
        }
        else
        {
            ResetTimerCamDisable();

           
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

    public void TakeDmg()
    {
        NbrVie -= 1;

        if(NbrVie > 2)
        {
            _led1.GetComponent<Renderer>().material = _ledON;
            _led2.GetComponent<Renderer>().material = _ledON;
            _led3.GetComponent<Renderer>().material = _ledON;
        }
        else if (NbrVie == 2)
        {
            _led1.GetComponent<Renderer>().material = _ledON;
            _led2.GetComponent<Renderer>().material = _ledON;
            _led3.GetComponent<Renderer>().material = _ledOFF;
        }
        else if (NbrVie == 1)
        {
            _led1.GetComponent<Renderer>().material = _ledON;
            _led2.GetComponent<Renderer>().material = _ledOFF;
            _led3.GetComponent<Renderer>().material = _ledOFF;
        }
        else if (NbrVie < 1)
        {
            _led1.GetComponent<Renderer>().material = _ledOFF;
            _led2.GetComponent<Renderer>().material = _ledOFF;
            _led3.GetComponent<Renderer>().material = _ledOFF;

            StopGame = true;
            S_ManagerGame.EndGame();
        }
    }


}
