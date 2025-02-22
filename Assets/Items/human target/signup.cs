using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class signup : MonoBehaviour
{
    // Time to rotate to x = 0 and back down to x = -90 (smoothly over 2 seconds)
    public float rotationDuration = 2f;
    // Time to wait before the sign pops up (random time between 5 and 15 seconds)
    private float randomWaitTime;
    // Time to wait before going back down (4 seconds after popping up)
    public float timeBeforeGoingDown = 4f;
    // The start and end rotation angles
    private float startRotationX = -90f;
    private float popUpRotationX = 0f;
    private float elapsedTime = 0f;
    private bool isPoppedUp = false;
    private bool isGoingDown = false;
    private bool firsttimedown = true;

    void Start()
    {
        // Log the signup start
        Debug.Log("Human sign up started.");
        // Set the random wait time between 5 and 15 seconds
        SetRandomWaitTime();
        Debug.Log($"The sign will pop up after {randomWaitTime} seconds.");
        // Set the initial rotation of the sign
        transform.rotation = Quaternion.Euler(startRotationX, 0f, 0f);
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        // Phase 1: Wait for random time between 5 and 15 seconds before popping up
        if (!isPoppedUp && elapsedTime >= randomWaitTime)
        {
            // Log when the sign starts popping up
            Debug.Log("Sign is now popping up.");
            // Start popping up the sign (rotate to x = 0) over 2 seconds
            isPoppedUp = true;
            elapsedTime = 0f;  // Reset time to start smooth rotation to 0
        }

        // Phase 2: Smoothly rotate to x = 0 in 2 seconds
        if (isPoppedUp && !isGoingDown)
        {
            // Calculate rotation between the current position (-90) and the up position (0) over the specified duration
            float rotationX = Mathf.Lerp(startRotationX, popUpRotationX, elapsedTime / rotationDuration);
            transform.rotation = Quaternion.Euler(rotationX, 0f, 0f);

            // Log the current rotation progress
            Debug.Log($"Rotating sign: X = {rotationX}");

            // Once rotation is complete (after 2 seconds), wait 4 seconds before going back down
            if (elapsedTime >= rotationDuration)
            {
                Debug.Log("Sign reached upright position (X = 0). Waiting for 4 seconds before going down.");
                elapsedTime = 0f;
                isGoingDown = true;  // Start countdown to go down
            }
        }

        // Phase 3: Wait 4 seconds after the pop-up and then rotate back to x = -90 over 2 seconds
        if (isGoingDown && elapsedTime >= timeBeforeGoingDown)
        {
            // Log the transition to going back down
            Debug.Log("Sign is now going back down.");
            Debug.Log($"{firsttimedown}");
            if (firsttimedown)
            {
                Debug.Log("Adding score.");
                FindObjectOfType<ScoreManager>().AddScore(10); // Add 10 points;
                firsttimedown = false; // Set it to false after adding points
            }
            // Smoothly rotate back to x = -90 in 2 seconds
            float rotationX = Mathf.Lerp(popUpRotationX, startRotationX, (elapsedTime - timeBeforeGoingDown) / rotationDuration);
            transform.rotation = Quaternion.Euler(rotationX, 0f, 0f);

            // Log the current rotation progress while going down
            Debug.Log($"Rotating back down: X = {rotationX}");

            // Once the sign reaches x = -90, we reset the process and start again with a new random wait time
            if (elapsedTime >= timeBeforeGoingDown + rotationDuration)
            {
                Debug.Log("Sign reached starting position (X = -90). Waiting for the next random time to pop up.");
                // After going down, set the next random wait time and restart the process
                SetRandomWaitTime();
                elapsedTime = 0f;  // Reset the elapsed time
                isPoppedUp = false;  // Sign is down, ready for next pop-up
                isGoingDown = false;  // Reset going down flag
                firsttimedown = true; // this needs to be 1 for the scoreboard
            }
        }
    }

    // Method to set a new random wait time between 5 and 15 seconds
    private void SetRandomWaitTime()
    {
        randomWaitTime = Random.Range(5f, 15f);
    }
}
