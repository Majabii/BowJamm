using UnityEngine;

public class HitDetection : MonoBehaviour
{
    private ScoreManager scoreManager;  // Reference to the ScoreManager script

    void Start()
    {
        // Find and reference the ScoreManager script in the scene
        scoreManager = FindObjectOfType<ScoreManager>();
        if (scoreManager == null)
        {
            Debug.LogError("ScoreManager not found in the scene!");
        }
    }

    // Detect when this object collides with another object
    void OnCollisionEnter(Collision collision)
    {
        // Check the tag of the object that hit
        if (collision.gameObject.CompareTag("innercercle"))
        {
            scoreManager.AddScore(30); // Add 30 points for enemy
            Debug.Log("Hit inner circle! Adding 30 points.");
        }
        else if (collision.gameObject.CompareTag("2nccircle"))
        {
            scoreManager.AddScore(20); // Add 20 points for bonus
            Debug.Log("Hit 2nccircle! Adding 20 points.");
        }
        else if (collision.gameObject.CompareTag("3thcircle"))
        {
            scoreManager.AddScore(10); // Add 10 points for obstacle
            Debug.Log("Hit 3thcircle! Adding 10 points.");
        }
        else if (collision.gameObject.CompareTag("zombie"))
        {
            scoreManager.AddScore(40); // Add 40 points for obstacle
            Debug.Log("Hit 3thcircle! Adding 40 points.");
        }
    }
}
