using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointNClickController : MonoBehaviour
{
	private Rigidbody2D rigidBody;
	private Vector2 currentPosition;
	private Vector2 targetPosition;
	private WalkablePath walkablePath;
	private Collider2D walkablePathCollider;
	private bool isMoving;
	private Pickable pickingIntention;
	private Animator animator;
	private GameManager gameManager;
	private CharacterPhrases characterPhrases;
	private GameObject characterSprite;

	public float speed = 20;
	public float positionThreshold = 1;
	public GameObject characterPhrasesPosition;

	// Start is called before the first frame update
	void Start()
	{
		this.characterPhrasesPosition = this.transform.Find("PromptPosition").gameObject;
		this.characterSprite = this.transform.Find("CharacterSprite").gameObject;

		this.gameManager = FindObjectOfType<GameManager>();
		this.rigidBody = this.GetComponent<Rigidbody2D>();
		this.animator = characterSprite.GetComponent<Animator>();
		this.rigidBody.transform.position = this.ClampPosition(this.rigidBody.transform.position);
		this.walkablePath = FindObjectOfType<WalkablePath>();
		this.walkablePathCollider = this.walkablePath.GetComponent<Collider2D>();
		this.currentPosition = this.ClampPosition(this.rigidBody.transform.position);
		this.targetPosition = this.currentPosition;
		this.isMoving = false;
		this.pickingIntention = null;
		this.characterPhrases = this.gameManager.GetCharacterPhrases();
	}

	//public void CharacterSay(string phrase)
	//{
		//Debug.Log("!!Say: " + phrase + " for " + this.gameManager.GetWaitTime(phrase) + " seconds");
		//StartCoroutine(ShowCharacterSay(phrase, this.gameManager.GetWaitTime(phrase)));
	//}

	IEnumerator ShowCharacterSay(string phrase, float time)
	{
		Debug.Log("Say: " + phrase + " for " + time + " seconds");
		this.characterPhrases.GetComponent<RectTransform>().position = new Vector2(this.characterPhrasesPosition.transform.position.x, this.characterPhrasesPosition.transform.position.y);
		this.characterPhrases.gameObject.GetComponent<TextMeshPro>().SetText(phrase);
		this.characterPhrases.gameObject.SetActive(true);
		yield return new WaitForSeconds(time);

		Debug.Log("Sacabos");
		this.characterPhrases.gameObject.SetActive(false);
	}

	// Update is called once per frame
	void Update()
    {
		float maxDistance;

		this.currentPosition = this.ClampPosition(this.rigidBody.transform.position);

		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			this.targetPosition = this.ClampPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
			this.targetPosition = this.walkablePathCollider.ClosestPoint(this.targetPosition);
			this.isMoving = true;
			updatePickingIntention();
		}

		if (isMoving)
		{
			maxDistance = Time.deltaTime * this.speed;
			Vector2 newPosition = this.walkablePathCollider.ClosestPoint(Vector2.MoveTowards(this.rigidBody.transform.position, this.targetPosition, maxDistance));
			if ((targetPosition.x - currentPosition.x) >=0 )
			{
				characterSprite.transform.localScale = new Vector3(Math.Abs(characterSprite.transform.localScale.x), characterSprite.transform.localScale.y, characterSprite.transform.localScale.z);

			} else
			{
				characterSprite.transform.localScale = new Vector3(-Math.Abs(characterSprite.transform.localScale.x), characterSprite.transform.localScale.y, characterSprite.transform.localScale.z);

			}
			this.rigidBody.transform.position = newPosition;
			this.animator.SetBool("playerIsMoving", true);
		} else
		{
			this.animator.SetBool("playerIsMoving", false);
		}
	}

	private void updatePickingIntention()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero); ;

		if (hit.collider != null && hit.collider.gameObject.GetComponent<Pickable>())
		{
			this.pickingIntention = hit.collider.gameObject.GetComponent<Pickable>();

		} else
		{
			this.pickingIntention = null;
		}

		Debug.Log(this.pickingIntention);
	}

	void LateUpdate()
	{
		if (Vector2.Distance(this.targetPosition, this.currentPosition) <= this.positionThreshold)
		{
			this.isMoving = false;
		}
	}

	private Vector2 ClampPosition(Vector2 position)
	{
		Vector2 clamped_position = new Vector2((int)position.x, (int)position.y);
		position = clamped_position;

		return position;
	}

	public Pickable GetPickingIntention()
	{
		return pickingIntention;
	}

	public bool IsMoving()
	{
		return isMoving;
	}
}
