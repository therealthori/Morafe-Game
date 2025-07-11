using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetect : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerAnim;

    void OnTriggerEnter(Collider other)
    {
        player.GetComponent<PlayerMovement>().enabled = false;
        playerAnim.GetComponent<Animator>().Play("Stumble Backwards");
    }
}
