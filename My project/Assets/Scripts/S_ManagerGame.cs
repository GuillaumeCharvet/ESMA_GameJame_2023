using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_ManagerGame : MonoBehaviour
{
    [SerializeField] private CS_Observer CS_Observer;
    [SerializeField] private S_Timer S_Timer;


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


        if (CS_Observer.NbrVie == 0)
        {
            //Afficher fin par trop d'erreur;
        }
        if (S_Timer._time < 0)
        {
            //if(NbrBonCraft > NbrEvileCraft)
            //{
            // Afficher Fin par trop Exploiter;
            //}

            //if(NbrBonCraft < NbrEvileCraft)
            //{
                //if(Soft < (Explotion && Megazord)
                //{
                        //Afficher Fin soft;
                //}
                //if(Explotion < (Soft && Megazord)
                //{
                        //Afficher Fin  Incendie;
                //}
                //if(Megazord < (Soft && Explotion)
                //{
                        //Afficher Fin WTF;
                //}
            //}
        }
    }

}
