using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ItemCollector : MonoBehaviour
{
    private int oranges = 0;

    [SerializeField] private TMP_Text orangesText;
    [SerializeField] private AudioSource CollectionSoundEffect;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Orange"))
        {
            CollectionSoundEffect.Play();
            Destroy(collision.gameObject);
            oranges++;
            orangesText.text = "Oranges: " + oranges;
        }
    }
}
