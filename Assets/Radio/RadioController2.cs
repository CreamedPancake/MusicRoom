using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadioController2 : MonoBehaviour
{
    [Header("List of tracks")]
    [SerializeField] private Track[] audioTracks;
    private int trackIndex2;

    [Header("Text UI")]
    [SerializeField] private Text trackTextUI2;

    private AudioSource radioAudioSource2;

    private void Start()
    {
        radioAudioSource2 = GetComponent<AudioSource>();
        
        trackIndex2 = 0;
        radioAudioSource2.clip = audioTracks[trackIndex2].trackAudioClip;
        trackTextUI2.text = audioTracks[trackIndex2].name;
    }

    public void SkipForwardButton()
    {
        if (trackIndex2 < audioTracks.Length - 1)
        {
            trackIndex2++;
            UpdateTrack(trackIndex2);
            
        }
        else
        {
            trackIndex2 -= audioTracks.Length;
        }
        PlayAudio();
    }
    public void SkipBackwardsButton()
    {
        if(trackIndex2 >= 1) 
        { 
            trackIndex2--;
            UpdateTrack(trackIndex2);
            
        }
        else
        {
            trackIndex2 += audioTracks.Length;
        }
        PlayAudio();
    }

    void UpdateTrack(int index)
    {
        radioAudioSource2.clip = audioTracks[index].trackAudioClip;
        trackTextUI2.text = audioTracks[index].name;
    }

    public void AudioVolume(float volume)
    {
        radioAudioSource2.volume = volume;
    }



    public bool isPaused = true;

    public void PlayAudio()
    {
        isPaused = false;
        radioAudioSource2.Play();
        StartCoroutine(WaitForMusicEnd());
    }
    
    public void PauseAudio()
    {
        isPaused = true;
        radioAudioSource2.Pause();
    }
    public void StopAudio()
    {
        isPaused = true;
        radioAudioSource2.Stop();
    }

    IEnumerator WaitForMusicEnd()
    {
        while (radioAudioSource2.isPlaying || isPaused == true)
        {
            yield return null;
        }
        SkipForwardButton();
    }
}
