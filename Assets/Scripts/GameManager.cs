using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public Tooltip tooltip;
	public PointNClickController characterController;

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

	public PointNClickController GetCharacterController()
	{
		return this.characterController;
	}

	public void ObjectPicked(GameObject pickable)
	{
		Debug.Log(pickable);
	}
}
