using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointNClickMovement : MonoBehaviour
{
	private float maxDistance;
	private Rigidbody2D rigidBody;
	private Vector2 currentPosition;
	private Vector2 targetPosition;
	private WalkablePath walkablePath;
	private Collider2D walkablePathCollider;

	public float speed = 20;

	// Start is called before the first frame update
	void Start()
    {
		rigidBody = GetComponent<Rigidbody2D>();
		rigidBody.transform.position = ClampPosition(rigidBody.transform.position);
		walkablePath = FindObjectOfType<WalkablePath>();
		walkablePathCollider = walkablePath.GetComponent<Collider2D>();
		currentPosition = ClampPosition(rigidBody.transform.position);
		targetPosition = currentPosition;
	}

	// Update is called once per frame
	void Update()
    {
		currentPosition = ClampPosition(rigidBody.transform.position);

		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			targetPosition = ClampPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
			targetPosition = walkablePathCollider.ClosestPoint(targetPosition);
		}

		maxDistance = Time.deltaTime * speed;
		rigidBody.transform.position = walkablePathCollider.ClosestPoint(Vector2.MoveTowards(rigidBody.transform.position, targetPosition, maxDistance));
	}

	private Vector2 ClampPosition(Vector2 position)
	{
		Vector2 clamped_position = new Vector2((int)position.x, (int)position.y);
		position = clamped_position;

		return position;
	}
}
