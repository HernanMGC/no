using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    public Animator animator;
    public float inputHoldDelay = 0.5f;
    public float baseSpeed = 2f;
    public float acceleration = 20f;
    public float stoppingDistance = 0.15f;

    private WaitForSeconds inputHoldWait;
    private float speed;
    private Vector2 destinationPosition;
    private Vector2 currentPosition;

    private const float stopDistanceProportion = 0.1f;

	// Start is called before the first frame update
	private void Start()
    {
        this.inputHoldWait = new WaitForSeconds(inputHoldDelay);
        this.rigidBody = this.GetComponent<Rigidbody2D>();

        destinationPosition = this.transform.position;
    }


    private void OnAnimatorMove()
    {

    }
    // Update is called once per frame
    private void Update()
    {
        float remainingDistance = Vector2.Distance(this.destinationPosition, this.currentPosition);
        this.currentPosition = this.ClampPosition(this.rigidBody.transform.position);


        if (remainingDistance <= this.stoppingDistance * stopDistanceProportion)
        {
            Stopping(out speed);
        }


    }

    private void Stopping(out float speed)
    {

    }

    private void Slowing(out float speed, float distanceToDestination)
    {

    }

    private void Moving()
    {

    }
    private Vector2 ClampPosition(Vector2 position)
    {
        Vector2 clamped_position = new Vector2((int)position.x, (int)position.y);
        position = clamped_position;

        return position;
    }
}
