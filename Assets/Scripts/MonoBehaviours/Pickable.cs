using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pickable : MonoBehaviour
{
	private GameManager gameManager;
	private Tooltip tooltip;
	private GameObject tooltipPosition;

	public string pickedText;
	public string hoverText;
	public string itemName;
	public Sprite itemSprite;
	public Item item;

	// Start is called before the first frame update
	void Start()
    {
		this.gameManager = FindObjectOfType<GameManager>();
		this.tooltip = this.gameManager.GetTooltip();
		this.tooltipPosition = this.transform.Find("TooltipPosition").gameObject;

		this.GetComponent<SpriteRenderer>().sprite = this.itemSprite;


		if (hoverText == "")
		{
			this.hoverText = this.itemName;
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void OnMouseOver()
	{
		this.tooltip.GetComponent<RectTransform>().position = new Vector2(this.tooltipPosition.transform.position.x, this.tooltipPosition.transform.position.y);
		this.tooltip.gameObject.GetComponent<TextMeshPro>().SetText(this.hoverText);
		this.tooltip.gameObject.SetActive(true);
	}

	void OnMouseExit()
	{
		this.tooltip.gameObject.SetActive(false);
	}

	void OnMouseDown()
	{
		
	}

	//void OnTriggerEnter2D(Collider2D collider)
	//{
	//	this.CheckPick(collider);
//	}

	//void OnTriggerStay2D(Collider2D collider)
	//{
	//	this.CheckPick(collider);
	//}

//	private void CheckPick(Collider2D collider)
	//{
		//PointNClickController character = this.gameManager.GetCharacterController();
		//GameObject characterGO = character.gameObject;
		//if (collider.gameObject == characterGO && character.GetPickingIntention() == this)
		//{
		//	if (!character.IsMoving())
		//	{
		//		gameManager.SendMessage("ObjectPicked", this.gameObject);
		//		Destroy(this.gameObject);
	//			this.tooltip.gameObject.SetActive(false);
		//	}
	//	}
	//}
}

