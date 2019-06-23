using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public Tooltip tooltip;
	public CharacterPhrases characterPhrases;
	//public PointNClickController characterController;

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public Tooltip GetTooltip()
	{
		return this.tooltip;
	}

	public CharacterPhrases GetCharacterPhrases()
	{
		return this.characterPhrases;
	}

	//public PointNClickController GetCharacterController()
	//{
		//return this.characterController;
	//}

	//public void ObjectPicked(GameObject pickable)
	//{
		//Debug.Log(pickable.GetComponent<Pickable>().pickedText);
		//characterController.CharacterSay(pickable.GetComponent<Pickable>().pickedText);
	//}

	//public float GetWaitTime(string text) {
		//float wpm = 180;
		//float word_length = 5;
		//float words = text.Length / word_length;
		//float words_time = ((words / wpm) * 60);

		//float delay = 1.5f;
		//float bonus = 1;

		//return delay + words_time + bonus;
	//}

}
