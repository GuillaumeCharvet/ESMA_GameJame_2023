using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class S_Dialogue : MonoBehaviour
{

    [SerializeField] private Animator _Hautparleur;

    [SerializeField] private GameObject[] _dialogueStart;
    [SerializeField] private int[] _TimeAfficheTxtStart;

    [SerializeField] private GameObject _dialogue1;
    [SerializeField] private GameObject _dialogue2;
    [SerializeField] private GameObject _dialogue3;
    [SerializeField] private GameObject _degat;
    [SerializeField] private int _TimeAfficheTxt;


    private void Update()
    {
        
    }

    public void Dialoguestart()
    {
        StartCoroutine(TimeAfficheDialogueStart());
    }
    
    public void Dialogue1()
    {
        StartCoroutine(TimeAfficheDialogue(_dialogue1));
    }

    public void Dialogue2()
    {
        StartCoroutine(TimeAfficheDialogue(_dialogue2));
    }
    public void Dialogue3()
    {
        StartCoroutine(TimeAfficheDialogue(_dialogue3));
    }



    public void dialogueTakeIt()
    {
        StartCoroutine(TimeAfficheDialogue(_degat));
    }

    IEnumerator TimeAfficheDialogueStart()
    {
        for (int i = 0; i < _dialogueStart.Length; i++)
        {
            _dialogueStart[i].SetActive(true);
            _Hautparleur.SetBool("IsTalking", true);

            yield return new WaitForSeconds(_TimeAfficheTxtStart[i]);

            _Hautparleur.SetBool("IsTalking", false);
            _dialogueStart[i].SetActive(false);

            yield return new WaitForSeconds(1f);

        }
    }

    IEnumerator TimeAfficheDialogue(GameObject imageDialogue)
    {
        imageDialogue.SetActive(true);
        _Hautparleur.SetBool("IsTalking", true);


        yield return new WaitForSeconds(_TimeAfficheTxt);

        _Hautparleur.SetBool("IsTalking", false);
        imageDialogue.SetActive(false);
    }

}
