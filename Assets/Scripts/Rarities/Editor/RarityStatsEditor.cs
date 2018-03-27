using System.Collections.Generic;
using RarityClasses;
using UnityEditor;
using UnityEngine;

// Wrote another one because there may be a case where I 
// might need the rarity% within the weapon itself.
// It works similarly to the ItemSpawnerEditor
[CustomEditor (typeof (RarityStats))]
public class RarityStatsEditor : Editor
{
	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI ();

		RarityStats _rs = target as RarityStats;
		float prevRT = 1f;

		EditorGUILayout.Space ();
		EditorGUILayout.LabelField ("Spawn Chance", EditorStyles.boldLabel);

		foreach (RarityType rt in _rs._rarityTypes)
		{
			if (rt == null) return;

			SerializedObject serializedObject = new UnityEditor.SerializedObject (rt);

			EditorGUI.BeginChangeCheck ();

			rt._chanceToSpawn = EditorGUILayout.Slider (rt._rarityName, rt._chanceToSpawn, 0f, 1f);

			if (rt == null) return;
			if (rt._chanceToSpawn > prevRT)
				rt._chanceToSpawn = prevRT;
			prevRT = rt._chanceToSpawn;

			if (EditorGUI.EndChangeCheck ())
			{
				serializedObject.ApplyModifiedProperties ();
				EditorUtility.SetDirty (rt);
			}
		}
	}
}