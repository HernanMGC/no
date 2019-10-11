using UnityEngine;

public class Interactable : MonoBehaviour
{
	public Transform interactionLocation;
	public ConditionCollection[] conditionCollections = new ConditionCollection[0];
	public ReactionCollection defaultReactionCollection;


	public void Interact()
	{
        Debug.Log("Interactable.Interact()");
		for (int i = 0; i < conditionCollections.Length; i++)
		{
			if (conditionCollections[i].CheckAndReact())
				return;
		}

		Debug.Log(defaultReactionCollection);
		defaultReactionCollection.React();
	}
}
