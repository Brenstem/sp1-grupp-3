using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerCutscene : MonoBehaviour
{
    [SerializeField] GameObject[] images;

    private Animator[] imageAnimators = new Animator[2];
    private Timer animationTimer;
    private bool animationPlaying;
    private bool cutSceneActivated;
    private int x = 0;

    private void Start()
    {
        imageAnimators = new Animator[images.Length];
        animationTimer = new Timer();
        
        for (int i = 0; i < images.Length; i++)
        {
            imageAnimators[i] = images[i].GetComponent<Animator>();
        }
    }

    private void Update()
    {
        animationTimer.UpdateTimer();

        if (cutSceneActivated)
        {
            PlayCutscene();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.gameObject.CompareTag("Player"))
        {
            cutSceneActivated = true;
            this.GetComponent<Collider2D>().enabled = false;
        }
    }

    private void PlayCutscene()
    {
        for (; x < images.Length;)
        {
            if (!animationPlaying)
            {
                Debug.Log("play " + x + " animation");
                images[x].SetActive(true);
                animationTimer.StartTimer(imageAnimators[x].GetCurrentAnimatorStateInfo(0).length);
                animationPlaying = true;
            }
            break;
        }

        if (animationTimer.TimerFinished)
        {
            Debug.Log(x + " animation finished");
            images[x].SetActive(false);
            animationPlaying = false;
            x++;
        }
    }
}
