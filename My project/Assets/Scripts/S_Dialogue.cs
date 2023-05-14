using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class S_Dialogue : MonoBehaviour
{

    [SerializeField] private Animator _Hautparleur;

    [SerializeField] private GameObject _dialogue1;
    [SerializeField] private GameObject _dialogue2;
    [SerializeField] private GameObject _dialogue3;
    [SerializeField] private GameObject _degat;





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



    IEnumerator TimeAfficheDialogue(GameObject imageDialogue)
    {
        imageDialogue.SetActive(true);
        _Hautparleur.SetBool("IsTaking",true);


        yield return new WaitForSeconds(2f);

        _Hautparleur.SetBool("IsTaking",false);
        imageDialogue.SetActive(false);
    }


}
