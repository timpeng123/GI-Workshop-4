// COMP30019 - Graphics and Interaction
// (c) University of Melbourne, 2022

using System.Collections;
using UnityEngine;

public class SwarmManager : MonoBehaviour
{
    // External parameters/variables
    [SerializeField] private GameObject enemyTemplate;
    [SerializeField] private int enemyRows;
    [SerializeField] private int enemyCols;
    [SerializeField] private float enemySpacing;
    [SerializeField] private float stepSize;
    [SerializeField] private float stepTime;
    [SerializeField] private float leftBoundaryX;
    [SerializeField] private float rightBoundaryX;

    private void Start()
    {
        // Initialise the swarm by instantiating enemy prefabs.
        GenerateSwarm();
        
        // Start swarm at the far left.
        transform.localPosition = new Vector3(this.leftBoundaryX, 0f, 0f);

        // Use a coroutine to periodically step the swarm. Coroutines are worth
        // learning about if you are unfamiliar with them. In Unity they allow
        // us to define sequences that span multiple frames in a very clean way.
        // Although it might look like it, using coroutines is *not* the same as
        // using multithreading! Read more here:
        // https://docs.unity3d.com/Manual/Coroutines.html
        StartCoroutine(StepSwarmPeriodically());
    }
    
    private IEnumerator StepSwarmPeriodically() 
    {
        // Yep, this is an infinite loop, but the gameplay isn't ever "halted"
        // since the function is invoked as a coroutine. It's also automatically 
        // stopped when the game object is destroyed.
        while (true)
        {
            yield return new WaitForSeconds(this.stepTime); // Not blocking!
            StepSwarm();
        }
    }

    // Automatically generate swarm of enemies based on the given serialized
    // attributes/parameters.
    private void GenerateSwarm()
    {
        // Write code here...
        for (int i = 0; i < enemyCols; i++)
        {
            for (int j = 0; j < enemyRows; j++)
            {
                var enemy = Instantiate(enemyTemplate, transform);
                // initialize enemies in x-z plane
                enemy.transform.localPosition = new Vector3(i * enemySpacing, 0f, j * enemySpacing);
            }
        }
    }

    // Step the swarm across the screen, based on the current direction, or down
    // and reverse when it reaches the edge.
    private void StepSwarm()
    {
        // Write code here...
        
        // Tip: You probably want a private variable to keep track of the
        // direction the swarm is moving. You could alternate this between 1 and
        // -1 to serve as a vector multiplier when stepping the swarm.

        var swarmWidth = (this.enemyCols - 1) * this.enemySpacing;
        var swarmMinX = transform.localPosition.x;
        var swarmMaxX = swarmMinX + swarmWidth;

        // if object reaches either left or right boundary, move downwards and reverse direction
        // else, move sideways
        // use swarmMinX and swarmMaxX to determine boundaries
        // below code moves swarm up and down in z-axis, please debug and fix it

        if (swarmMinX >= this.leftBoundaryX || swarmMaxX <= this.rightBoundaryX)
        {
            // Move downwards in the y-axis.
            transform.localPosition += new Vector3(0f, 0f, -this.stepSize);

            // Reverse the direction of movement.
            this.stepSize *= -1;

            
        }
        else
        {
            // Move sideways according to the direction and step size.
            transform.localPosition += new Vector3(this.stepSize, 0f, 0f);
        }
    }
}
