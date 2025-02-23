using System.Collections;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioClip[] audioClips; // Array to hold your MP3 files
    private AudioSource audioSource; // Reference to the AudioSource component

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component
        StartCoroutine(PlayAudioWithInterval()); // Start playing the audio at intervals
    }

    IEnumerator PlayAudioWithInterval()
    {
        while (true)
        {
            // Randomly select an AudioClip from the array
            int randomIndex = Random.Range(0, audioClips.Length);
            AudioClip selectedClip = audioClips[randomIndex];

            // Play the selected clip
            audioSource.clip = selectedClip;
            audioSource.Play();

            // Wait for the duration of the audio clip before playing another one
            yield return new WaitForSeconds(selectedClip.length);

            // Wait for 10 seconds before playing the next clip
            yield return new WaitForSeconds(1f);
        }
    }
}
