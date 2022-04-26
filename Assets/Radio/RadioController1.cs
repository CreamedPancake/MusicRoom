using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadioController1 : MonoBehaviour
{
    [Header("List of tracks")]
    [SerializeField] private Track[] audioTracks;
    private int trackIndex1;

    [Header("Text UI")]
    [SerializeField] private Text trackTextUI1;

    private AudioSource radioAudioSource1;

    private void Start()
    {
        radioAudioSource1 = GetComponent<AudioSource>();
        
        trackIndex1 = 0;
        radioAudioSource1.clip = audioTracks[trackIndex1].trackAudioClip;
        trackTextUI1.text = audioTracks[trackIndex1].name;
    }

    public void SkipForwardButton()
    {
        if (trackIndex1 < audioTracks.Length - 1)
        {
            trackIndex1++;
            UpdateTrack(trackIndex1);
            
        }
        else
        {
            trackIndex1 -= audioTracks.Length;
        }
        PlayAudio();
    }
    public void SkipBackwardsButton()
    {
        if(trackIndex1 >= 1) 
        { 
            trackIndex1--;
            UpdateTrack(trackIndex1);
            
        }
        else
        {
            trackIndex1 += audioTracks.Length;
        }
        PlayAudio();
    }

    void UpdateTrack(int index)
    {
        radioAudioSource1.clip = audioTracks[index].trackAudioClip;
        trackTextUI1.text = audioTracks[index].name;
    }

    public void AudioVolume(float volume)
    {
        radioAudioSource1.volume = volume;
    }



    public bool isPaused = true;

    public void PlayAudio()
    {
        isPaused = false;
        radioAudioSource1.Play();
        StartCoroutine(WaitForMusicEnd());
    }
    
    public void PauseAudio()
    {
        isPaused = true;
        radioAudioSource1.Pause();
    }
    public void StopAudio()
    {
        isPaused = true;
        radioAudioSource1.Stop();
    }

    IEnumerator WaitForMusicEnd()
    {
        while (radioAudioSource1.isPlaying || isPaused == true)
        {
            yield return null;
        }
        SkipForwardButton();
    }
}
