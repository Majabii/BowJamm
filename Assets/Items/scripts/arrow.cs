using UnityEngine;
using System.Collections; // Add this line to use IEnumerator and coroutines

public class ArrowScript : MonoBehaviour
{
    // Rigidbody reference to apply forces to the arrow
    private Rigidbody rb;

    // Lifetime of the arrow before it destroys itself
    public float lifetime = 50f;

    // Arrow prefab to instantiate (drag this into the inspector)
    public GameObject arrowPrefab;

    // Force applied to the arrow
    public float launchForceX = 5f;
    public float launchForceY = 10f;
    public float launchForceZ = 5f;

    void Start()
    {
        // Get the Rigidbody component attached to the arrow
        rb = GetComponent<Rigidbody>();

        // Ensure Rigidbody is present and active
        if (rb == null)
        {
            Debug.LogError("Rigidbody is missing from the Arrow GameObject!");
            return;
        }

        // Launch the first arrow
        ShootArrow(launchForceX, launchForceY, launchForceZ);

        // Destroy the arrow after the specified lifetime
        Destroy(gameObject, lifetime);

        // Start the coroutine to create a new arrow every 3 seconds
        StartCoroutine(SpawnArrowEveryFewSeconds());
    }

    // Method to apply the launch force to the arrow's Rigidbody
    public void ShootArrow(float launchForceX, float launchForceY, float launchForceZ)
    {
        if (rb != null)
        {
            // Create a Vector3 force based on the given x, y, and z values
            Vector3 launchForce = new Vector3(launchForceX, launchForceY, launchForceZ);

            // Apply the force to the Rigidbody immediately (Impulse mode)
            rb.AddForce(launchForce, ForceMode.Impulse);

            // Log the force to debug if the launch is happening
            Debug.Log($"Arrow launched with force: {launchForce}");
        }
    }

    // Coroutine to spawn a new arrow every 3 seconds
    private IEnumerator SpawnArrowEveryFewSeconds()
    {
        while (true) // Run indefinitely
        {
            // Wait for 3 seconds
            yield return new WaitForSeconds(3f);

            // Spawn a new arrow at the current position and rotation
            Instantiate(arrowPrefab, transform.position, transform.rotation);

            // Optionally, you can also add a random force or different parameters for each new arrow
            ShootArrow(launchForceX, launchForceY, launchForceZ);
        }
    }

    // Detect collision with a target
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the arrow hits a target
        if (collision.gameObject.CompareTag("Target"))
        {
            Debug.Log("Arrow hit the target!");

            // Optionally, destroy the arrow upon hitting the target
            Destroy(gameObject);
        }
    }

    // You can use this method to get the current velocity of the arrow (x, y, z values)
    public Vector3 GetArrowVelocity()
    {
        if (rb != null)
        {
            return rb.velocity; // Return the velocity of the arrow in 3D space
        }
        return Vector3.zero; // Return zero if Rigidbody is not found
    }
}
