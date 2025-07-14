using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionDetect : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerAnim;
    [SerializeField] private AudioSource collisionFX;
    [SerializeField] private GameObject fadeOut;
    
    void OnTriggerEnter(Collider other)
    {
        collisionFX.Play();
        player.GetComponent<PlayerMovement>().enabled = false;
        playerAnim.GetComponent<Animator>().Play("Stuble Backwards");
    }

    IEnumerator CollisionEnd()
    {
        
        yield return new WaitForSeconds(3);
        fadeOut.SetActive(true);
    }
}
