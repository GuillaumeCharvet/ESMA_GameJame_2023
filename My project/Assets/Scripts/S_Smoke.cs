using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Smoke : MonoBehaviour
{
    public GameObject smokePlane;
    //public Camera cam;

    public float speed;
    public float smokeTime = 3.0f;
    public bool useSmoke = false;

    public List<GameObject> pafListe;
    public List<Transform> pafTransformList;
    public List<Vector3> basePos;

    public bool needToStart = false;
    // Start is called before the first frame update


    private void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            basePos.Add(pafTransformList[i].transform.position);
        }
    }

    private void Update()
    {
        if (smokePlane.activeInHierarchy && needToStart == false)
        {
            useSmoke = true;
            needToStart = true;
            StartCoroutine(smokeVFX(smokeTime));
        }
    }

    public IEnumerator smokeVFX(float time)
    {
        StartCoroutine(pafBoomPow());
        var timer = 0.0f;
        while (useSmoke)
        {
             var randRota = Random.Range(15.0f, 90.0f);
             smokePlane.transform.Rotate(new Vector3(0, 0, randRota));
            if (timer < time)
            {
                timer += speed;
                yield return new WaitForSeconds(speed);
            }
            else
            {
                needToStart = false;
                for (int i = 0; i < 3; i++)
                {
                    pafListe[i].SetActive(false);
                }
                useSmoke = false;
                smokePlane.SetActive(false);
            }
        }
    }

    private IEnumerator pafBoomPow()
    {
        while (useSmoke)
        {
            var random = Random.Range(0, 3);
            var randPos = Random.Range(0, 3);
            for (int i = 0; i < 3; i++)
            {
                if (i == random)
                {
                    Debug.Log("activate");
                    pafListe[i].transform.position = basePos[randPos];
                    pafListe[i].SetActive(true);
                }
                else pafListe[i].SetActive(false);
            }
            yield return new WaitForSeconds(speed * 5);
        }
    }
}
