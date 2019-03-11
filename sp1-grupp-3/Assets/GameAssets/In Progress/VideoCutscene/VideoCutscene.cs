using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoCutscene : MonoBehaviour
{
    public bool videoPlaying = false;

    private VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && !videoPlayer.isPlaying) {
            videoPlayer.Play();
        }
        else if (Input.GetKeyDown(KeyCode.P) && videoPlayer.isPlaying) {
            videoPlayer.Stop();
        }


        if (true) {

        }

        videoPlayer.loopPointReached += StopVideo;        
    }

    private void StopVideo(UnityEngine.Video.VideoPlayer vp)
    {
        vp.Stop();
    }
}
