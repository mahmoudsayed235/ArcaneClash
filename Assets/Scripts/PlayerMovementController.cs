using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    private int currentLane = 3;

    void Update()
    {
        // Check if space key is pressed
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (currentLane != 1)
            {
                currentLane--;
                Teleport(true);

            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (currentLane != 5)
            {
                currentLane++;
                Teleport(false);

            }
        }
        
    }

    void Teleport(bool Left)
    {
        float MovementValue=2;
        if (Left)
        {
            MovementValue = Mathf.Abs(MovementValue) * -1;

        }
        else
        {
            MovementValue = Mathf.Abs(MovementValue);

        }
        // Set the player's position to the new position
        transform.position += new Vector3(MovementValue, 0,0);
    }
}
