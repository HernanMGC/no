using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(Inventory))]
public class InventoryEditor : Editor
{
	private SerializedProperty itemImagesProperty;
	private SerializedProperty itemsProperty;
	private bool[] showItemsSlot = new bool[Inventory.numItemSlots];

	private const string inventoryPropItemImagesName = "itemImages";
	private const string inventoryPropItemsName = "item";

	private void OnEnable()
	{
		itemImagesProperty = serializedObject.FindProperty(inventoryPropItemImagesName);
		itemsProperty = serializedObject.FindProperty(inventoryPropItemsName);
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		for (int i = 0; i < Inventory.numItemSlots; i++)
		{
			ItemSlotGUI(i);
		}

		serializedObject.ApplyModifiedProperties();
	}

	private void ItemSlotGUI(int index)
	{
		EditorGUILayout.BeginVertical(GUI.skin.box);
		EditorGUI.indentLevel++;

		showItemsSlot[index] = EditorGUILayout.Foldout(showItemsSlot[index], "Item slot " + index);

		if (showItemsSlot[index])
		{
			EditorGUILayout.PropertyField(itemImagesProperty.GetArrayElementAtIndex(index));
			EditorGUILayout.PropertyField(itemsProperty.GetArrayElementAtIndex(index))
		}

		EditorGUI.indentLevel--;
		EditorGUILayout.EndVertical();
	}
}
