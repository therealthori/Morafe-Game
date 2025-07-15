using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoin : MonoBehaviour
{
    [SerializeField] AudioSource coinFX;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        coinFX.Play();
        MasterInfo.Instance.AddCoin();
        gameObject.SetActive(false);
    }
}
