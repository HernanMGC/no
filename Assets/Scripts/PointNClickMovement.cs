using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointNClickMovement : MonoBehaviour
{
	private float maxDistance;
	private Rigidbody2D rigidBody;
	private Vector2 currentPosition;
	private Vector2 targetPosition;

	public float speed = 100;
	//public Vector2 journey;
	//public Vector2 gap;
	//public double stopThreshold = .05;

	// Start is called before the first frame update
	void Start()
    {
		rigidBody = GetComponent<Rigidbody2D>();
	}

    // Update is called once per frame
    void Update()
    {
		currentPosition = rigidBody.transform.position;

		//if (Input.GetMouseButtonDown(0))
		//{
		//	targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		//	journey.x = targetPosition.x - currentPosition.x;
		//	journey.y = targetPosition.y - currentPosition.y;
		//	rigidBody.velocity = new Vector2(
		//			journey.normalized.x * speedX * Time.deltaTime,
		//			journey.normalized.y * speedY * Time.deltaTime
		//		);
		//}

		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}

		maxDistance = Time.deltaTime * speed;

		rigidBody.transform.position = Vector3.MoveTowards(rigidBody.transform.position, targetPosition, maxDistance);
	}


	//private void LateUpdate()
	//{
	//	currentPosition = rigidBody.transform.position;
	//	gap.x = Mathf.Abs(targetPosition.x) - Mathf.Abs(currentPosition.x);
	//	gap.y = Mathf.Abs(targetPosition.y) - Mathf.Abs(currentPosition.y);
	//	if (stopThreshold > Mathf.Abs(gap.x) && stopThreshold > Mathf.Abs(gap.y))
	//	{
	//		rigidBody.velocity = new Vector2(0, 0);
	//	}
	//}
}
