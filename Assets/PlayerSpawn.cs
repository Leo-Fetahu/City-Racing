using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawn : MonoBehaviour
{
    public Transform spawnPoint; // The spawn point for the player
    public string previousSceneName; // The name of the previous scene

    private void Start()
    {
        // Get the name of the previous scene from PlayerPrefs
        string previousScene = PlayerPrefs.GetString("PreviousScene");

        // Check if the previous scene matches the desired scene
        if (previousScene == previousSceneName)
        {
            // Find the player GameObject
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            // Check if the player exists
            if (player != null)
            {
                // Move the player to the spawn point position
                player.transform.position = spawnPoint.position;
            }
        }

        // Save the current scene as the previous scene for the next switch
        PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
    }
}
