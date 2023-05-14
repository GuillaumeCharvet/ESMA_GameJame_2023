using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class S_ManagerGame : MonoBehaviour
{
    [SerializeField] private CS_Observer CS_Observer;
    [SerializeField] private S_Timer S_Timer;
    [SerializeField] private CS_FinalResult CS_FinalResult;
    [SerializeField] private ItemsManager itemsManager;

    [SerializeField] private List<Sprite> _journalImage;
    [SerializeField] private Sprite _journalImageLoose;
    [SerializeField] private Image _UIJournal;
    [SerializeField] private GameObject _boutonReload;
    private int currentJournalIndex = 0;



    private GameObject _EndMenu;
    private GameObject _homeBouton;

    private void Start()
    {
     
    }
    private void Update()
    {
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {

    }

    public void ReloadGame()
    {
        Scene _scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(_scene.name);
    }

    public void EndGame()
    {
        CS_Observer.StopGame = true;
        S_Timer._isTimerStopped = false;

        _EndMenu.SetActive(true);



        if (CS_Observer.NbrVie == 0)
        {
            _journalImage.Add(_journalImageLoose);
        }


        var listItems = CS_FinalResult.numberOfItemSent;
        for (int i = 0; i < listItems.Length; i++)
        {
            if (listItems[i] > 0 && !itemsManager.items[i].authorized)
            {
                _journalImage.Add(itemsManager.items[i].journal);
            }
        }

        AfficheImage();
    }

    public void AfficheImage()
    {
        if (currentJournalIndex < _journalImage.Count - 1)
        {
            _UIJournal.sprite = _journalImage[currentJournalIndex];
            currentJournalIndex += 1;
        }
        else
        {
            _UIJournal.sprite = null;
            _boutonReload.SetActive(true);
        }
    }
}
