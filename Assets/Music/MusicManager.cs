using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip musicClip;
    public Transform playerTransform;
    public Transform musicObjectTransform;
    public float maxDistance = 10f;
    public float minDistance = 1f;
    public bool loopMusic = true;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            // If no AudioSource, add one
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Set the music clip
        audioSource.clip = musicClip;

        // Set whether the music should loop
        audioSource.loop = loopMusic;

        // Play the music
        audioSource.Play();
    }

    void Update()
    {
        // If we don't have a player or object to track, return
        if (playerTransform == null || musicObjectTransform == null)
            return;

        // Calculate the distance between the player and the music source
        float distance = Vector3.Distance(playerTransform.position, musicObjectTransform.position);

        // Adjust the volume based on the distance
        AdjustVolumeBasedOnDistance(distance);
    }

    void AdjustVolumeBasedOnDistance(float distance)
    {
        // Normalize the distance within the range
        float normalizedDistance = Mathf.InverseLerp(maxDistance, minDistance, distance);

        // Inverse Lerp returns a value between 0 and 1. We reverse it so that the volume gets louder as the distance decreases
        float volume = 1f - normalizedDistance;

        // Clamp the volume to make sure it's between 0 and 1
        audioSource.volume = Mathf.Clamp(volume, 0f, 1f);
    }

    // Optionally, create a method to change the music during runtime
    public void ChangeMusic(AudioClip newMusic)
    {
        audioSource.Stop();  // Stop current music
        audioSource.clip = newMusic;
        audioSource.Play();  // Play the new music
    }
}
