using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadioController : MonoBehaviour
{
    [Header("List of tracks")]
    [SerializeField] private Track[] audioTracks;
    private int trackIndex;

    [Header("Text UI")]
    [SerializeField] private Text trackTextUI;

    private AudioSource radioAudioSource;

    private void Start()
    {
        radioAudioSource = GetComponent<AudioSource>();
        
        trackIndex = 0;
        radioAudioSource.clip = audioTracks[trackIndex].trackAudioClip;
        trackTextUI.text = audioTracks[trackIndex].name;
    }

    public void SkipForwardButton()
    {
        if (trackIndex < audioTracks.Length - 1)
        {
            trackIndex++;
            UpdateTrack(trackIndex);
            
        }
        else
        {
            trackIndex -= audioTracks.Length;
        }
        PlayAudio();
    }
    public void SkipBackwardsButton()
    {
        if(trackIndex >= 1) 
        { 
            trackIndex--;
            UpdateTrack(trackIndex);
            
        }
        else
        {
            trackIndex += audioTracks.Length;
        }
        PlayAudio();
    }

    void UpdateTrack(int index)
    {
        radioAudioSource.clip = audioTracks[index].trackAudioClip;
        trackTextUI.text = audioTracks[index].name;
    }

    public void AudioVolume(float volume)
    {
        radioAudioSource.volume = volume;
    }



    public bool isPaused = true;

    public void PlayAudio()
    {
        isPaused = false;
        radioAudioSource.Play();
        StartCoroutine(WaitForMusicEnd());
    }
    
    public void PauseAudio()
    {
        isPaused = true;
        radioAudioSource.Pause();
    }
    public void StopAudio()
    {
        isPaused = true;
        radioAudioSource.Stop();
    }

    IEnumerator WaitForMusicEnd()
    {
        while (radioAudioSource.isPlaying || isPaused == true)
        {
            yield return null;
        }
        SkipForwardButton();
    }
}
