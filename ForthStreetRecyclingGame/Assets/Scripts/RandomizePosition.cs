using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizePosition : MonoBehaviour
{
    public GameObject[] sprites; // Array to hold your 2D sprites
    public float minDistance = 50f; // Minimum distance between sprites

    void Start()
    {
        // Define the boundaries of your canvas
        float canvasWidth = GetComponent<RectTransform>().rect.width;
        float canvasHeight = GetComponent<RectTransform>().rect.height;

        // Loop through each sprite and assign a random position within the canvas boundaries
        foreach (GameObject sprite in sprites)
        {
            RectTransform spriteTransform = sprite.GetComponent<RectTransform>();

            // Generate random positions within the canvas boundaries
            Vector3 randomPosition = GetRandomPosition(canvasWidth, canvasHeight);

            // Ensure the sprite doesn't collide with other sprites
            while (IsColliding(randomPosition))
            {
                randomPosition = GetRandomPosition(canvasWidth, canvasHeight);
            }

            // Assign the random position to the sprite
            spriteTransform.localPosition = randomPosition;
        }
    }

    // Function to get a random position within the canvas boundaries
    Vector3 GetRandomPosition(float canvasWidth, float canvasHeight)
    {
        float randomX = Random.Range(-canvasWidth / 2f, canvasWidth / 2f);
        float randomY = Random.Range(-canvasHeight / 2f, canvasHeight / 2f);
        return new Vector3(randomX, randomY, 0f);
    }

    // Function to check if the sprite collides with other sprites
    bool IsColliding(Vector3 position)
    {
        foreach (GameObject sprite in sprites)
        {
            RectTransform spriteTransform = sprite.GetComponent<RectTransform>();
            float distance = Vector3.Distance(position, spriteTransform.localPosition);
            if (distance < minDistance)
            {
                return true;
            }
        }
        return false;
    }
}
