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
        Debug.Log("Arrow hit: " + collision.gameObject.name); // Log the object name

        if (collision.gameObject.CompareTag("geel"))
        {
            scoreManager.AddScore(10);
            Debug.Log("Hit inner circle! (geel)");
        }
        else if (collision.gameObject.CompareTag("rood"))
        {
            scoreManager.AddScore(8);
            Debug.Log("Hit 2nd circle! (rood)");
        }
        else if (collision.gameObject.CompareTag("blauw"))
        {
            scoreManager.AddScore(6);
            Debug.Log("Hit 3rd circle! (blauw)");
        }
        else if (collision.gameObject.CompareTag("zomb"))
        {
            scoreManager.AddScore(20);
            Debug.Log("Hit zombie target! (zomb)");
        }
        else
        {
            Debug.Log("Hit something else: " + collision.gameObject.tag);
        }
    }
}
