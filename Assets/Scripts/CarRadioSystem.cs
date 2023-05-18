using UnityEngine;

public class CarRadioSystem : MonoBehaviour
{
    public AudioClip[] audioClips;
    private AudioSource audioSource;
    private int currentStationIndex = 0;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayCurrentStation();
    }

    private void Update()
    {
        // Check for input to change station or adjust volume
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // Previous station
            currentStationIndex = (currentStationIndex - 1 + audioClips.Length) % audioClips.Length;
            PlayCurrentStation();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // Next station
            currentStationIndex = (currentStationIndex + 1) % audioClips.Length;
            PlayCurrentStation();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // Increase volume
            audioSource.volume = Mathf.Clamp01(audioSource.volume + 0.1f);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            // Decrease volume
            audioSource.volume = Mathf.Clamp01(audioSource.volume - 0.1f);
        }
    }

    private void PlayCurrentStation()
    {
        audioSource.clip = audioClips[currentStationIndex];
        audioSource.Play();
    }
}
