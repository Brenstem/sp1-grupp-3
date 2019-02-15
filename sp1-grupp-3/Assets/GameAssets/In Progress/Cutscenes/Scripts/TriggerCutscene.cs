using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerCutscene : MonoBehaviour
{
    public GameObject[] images;

    private Animator[] imageAnimators;
    private Timer animationTimer;
    private bool animationPlaying;

    private void Start()
    {
        animationTimer = new Timer();
        
        for (int i = 0; i < images.Length; i++)
        {
            imageAnimators[i] = images[i].GetComponent<Animator>();
        }
    }

    private void Update()
    {
        animationTimer.UpdateTimer();

        if (animationTimer.TimerFinished)
        {
            animationPlaying = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag("Player"))
        {
            PlayCutscene();
        }
    }

    private void PlayCutscene()
    {
        for (int i = 0; i < images.Length; i++)
        {
            animationPlaying = true;
            images[i].SetActive(true);
            animationTimer.StartTimer(imageAnimators[i].GetCurrentAnimatorStateInfo(0).length);
        }
    }
}
