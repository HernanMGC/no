using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    public Animator animator;
    public float inputHoldDelay = 0.5f;
    public float baseSpeed = 2f;
    public float acceleration = 20f;
    public float stoppingDistance = 0.15f;
	public float slowingSpeed = 0.175f;

    private WaitForSeconds inputHoldWait;
    private float speed;
	private float maxDistance;
	private WalkablePath walkablePath;
	private Collider2D walkablePathCollider;
	private Vector2 destinationPosition;
    private Vector2 currentPosition;
	private bool handleInput = true;
	private Interactable currentInteractable;
	private GameObject characterSprite;

	private const float stopDistanceProportion = 0.1f;

	private readonly int hashLocomotionTag = Animator.StringToHash("Locomotion");

	// Start is called before the first frame update
	private void Start()
    {
        this.inputHoldWait = new WaitForSeconds(inputHoldDelay);
        this.rigidBody = this.GetComponent<Rigidbody2D>();
		this.walkablePath = FindObjectOfType<WalkablePath>();
		this.walkablePathCollider = this.walkablePath.GetComponent<Collider2D>();
		this.characterSprite = this.transform.Find("CharacterSprite").gameObject;

		this.transform.position = this.ClampPosition(this.transform.position);
		destinationPosition = this.transform.position;
    }


    private void OnAnimatorMove()
    {

    }
    // Update is called once per frame
    private void Update()
	{
		this.currentPosition = this.ClampPosition(this.transform.position);
		float remainingDistance = Vector2.Distance(this.destinationPosition, this.currentPosition);

		if (remainingDistance <= this.stoppingDistance)
		{
			Stopping();
		}

		else if (remainingDistance > this.stoppingDistance)
		{
			Slowing(remainingDistance);
		}


	}

	private void Stopping()
	{
		this.animator.SetBool("playerIsMoving", false);
		this.transform.position = this.destinationPosition;

        if (currentInteractable)
        {
            currentInteractable.Interact();
            currentInteractable = null;
            StartCoroutine(WaitForInteraction());
        }
    }

    private void Slowing(float distanceToDestination)
	{
		this.animator.SetBool("playerIsMoving", true);
		transform.position = this.walkablePathCollider.ClosestPoint(Vector2.MoveTowards(transform.position, this.destinationPosition, this.slowingSpeed * Time.deltaTime));

		if ((this.destinationPosition.x - this.currentPosition.x) >= 0)
		{
			this.characterSprite.transform.localScale = new Vector3(Math.Abs(this.characterSprite.transform.localScale.x), this.characterSprite.transform.localScale.y, this.characterSprite.transform.localScale.z);

		} else
		{
			this.characterSprite.transform.localScale = new Vector3(-Math.Abs(this.characterSprite.transform.localScale.x), this.characterSprite.transform.localScale.y, this.characterSprite.transform.localScale.z);

		}
	}
	

    private Vector2 ClampPosition(Vector2 position)
    {
        Vector2 clamped_position = new Vector2((int)position.x, (int)position.y);
        position = clamped_position;

        return position;
    }

	public void OnGroundClick(BaseEventData data)
	{
        Debug.Log("OnGroundClick");
        if (!this.handleInput)
        {
            Debug.Log("OnGroundClick: !handleInput");
            return;
		}

		this.currentInteractable = null;

		PointerEventData pData = (PointerEventData)data;
		this.maxDistance = Time.deltaTime * this.slowingSpeed;
		this.destinationPosition = this.walkablePathCollider.ClosestPoint(pData.pointerCurrentRaycast.worldPosition);
	}

    public void OnInteractableClick(Interactable interactable)
    {
        Debug.Log("OnInteractableClick");
        if (!handleInput)
        {
            Debug.Log("OnInteractableClick: !handleInput");
            return;
        }

        this.currentInteractable = interactable;
        this.destinationPosition = currentInteractable.interactionLocation.position;
    }

    private IEnumerator WaitForInteraction()
	{
        this.handleInput = false;

		yield return inputHoldWait;

		while (animator.GetCurrentAnimatorStateInfo(0).tagHash != this.hashLocomotionTag)
		{
			yield return null;
		}

		this.handleInput = true;
	}
}
