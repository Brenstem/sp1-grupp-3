﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoCutscene : MonoBehaviour
{
    public bool isPlaying = false;
    public bool hasPlayed = false;
    public GameObject player;

    private VideoPlayer videoPlayer;
    private PlayerMovement playerMovement;
    private Animator playerAnimator;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        playerMovement = player.GetComponent<PlayerMovement>();
        playerAnimator = player.GetComponent<Animator>();
    }

    void Update()
    {
        if (videoPlayer.isPlaying)
        {
            FMODUnity.RuntimeManager.MuteAllEvents(true); 
        }

        if (isPlaying && !videoPlayer.isPlaying) {
            videoPlayer.Play();
            playerMovement.enabled = false;
        }

        else if (!isPlaying) {
            videoPlayer.Stop();            
        }

        videoPlayer.loopPointReached += StopVideo;        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasPlayed) {
            isPlaying = true;
            hasPlayed = true;
        }
        
    }

    private void StopVideo(VideoPlayer vp)
    {
        vp.Stop();
        isPlaying = false;
        playerMovement.enabled = true;
        playerAnimator.enabled = true;
    }
}
