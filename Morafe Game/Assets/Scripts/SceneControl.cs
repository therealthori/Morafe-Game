using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    public void StartGame()
    {
        StartCoroutine(StartButton());
    }

    IEnumerator StartButton()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(1);
    }
}
