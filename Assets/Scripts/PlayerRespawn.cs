using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public KeyCode respawnKey = KeyCode.R;
    public Transform[] respawnPoints;

    private void Update()
    {
        // Check if the player pressed the respawn key
        if (Input.GetKeyDown(respawnKey))
        {
            Respawn();
        }
    }

    private void Respawn()
    {
        // Get the player's current position
        Vector3 playerPosition = transform.position;

        // Find the nearest respawn point
        Transform nearestRespawnPoint = GetNearestRespawnPoint(playerPosition);

        // Set the player's position to the nearest respawn point
        transform.position = nearestRespawnPoint.position;
    }

    private Transform GetNearestRespawnPoint(Vector3 playerPosition)
    {
        Transform nearestPoint = null;
        float minDistance = Mathf.Infinity;

        // Iterate through all the respawn points
        foreach (Transform point in respawnPoints)
        {
            // Calculate the distance between the player and the current respawn point
            float distance = Vector3.Distance(playerPosition, point.position);

            // Check if this point is closer than the previous closest point
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestPoint = point;
            }
        }

        return nearestPoint;
    }
}