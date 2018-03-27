using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

// The code below is horrible
// Here is a panda to ease the pain. It's my first custom editor script.

//                                  -|-_
//                                   | _

//                                  <|/\
//                                   | |,

//                                  |-|-o
//                                  |<|.

//                   _,..._,m,      |,
//                ,/'      '"";     | |,
//               /             ".
//             ,'mmmMMMMmm.      \  -|-_"
//           _/-"^^^^^"""%#%mm,   ;  | _ o
//     ,m,_,'              "###)  ;,
//    (###%                 \#/  ;##mm.
//     ^#/  __        ___    ;  (######)
//      ;  //.\\     //.\\   ;   \####/
//     _; (#\"//     \\"/#)  ;  ,/
//    @##\ \##/   =   `"=" ,;mm/
//    `\##>.____,...,____,<####@

namespace ProcGen
{
	[CustomEditor (typeof (ItemSpawner))]
	public class ItemSpawnerEditor : Editor
	{
		// I'm basically creating a an array of floats for each element that will be spawned.
		// The float is used as a percent to calulate the likelyhood the object has to spawn.
		public override void OnInspectorGUI ()
		{
			base.OnInspectorGUI ();
			float prevRT = 1f;

			ItemSpawner itemSpawner = target as ItemSpawner;

			EditorGUILayout.Space ();
			EditorGUILayout.LabelField ("Spawn Chance", EditorStyles.boldLabel);

			SerializedObject itemSpawnerSO = new UnityEditor.SerializedObject (itemSpawner);

			List<Transform> listT = itemSpawner._objectsToSpawn;
			List<float> listF = itemSpawner._spawnChances;
			// Serialized Property to change, important if we want the variables 
			// to remember their values when Unity is restarted.
			SerializedProperty spawnChanceSP = itemSpawnerSO.FindProperty ("_spawnChances");

			EditorGUI.BeginChangeCheck ();

			// Making sure the array sizes are identical.
			do {
				if (listT.Count > spawnChanceSP.arraySize)
				{
					spawnChanceSP.arraySize++;
					listF.Add (0.0f);
				}
				else if (listT.Count < spawnChanceSP.arraySize)
				{
					spawnChanceSP.arraySize--;
					listF.RemoveAt (spawnChanceSP.arraySize);
				}
			} while (listT.Count != spawnChanceSP.arraySize);

			// adding appropriate number of sliders.
			for (int i = 0; i < listT.Count; i++)
			{
				if (listT[i] == null) return;

				float current = spawnChanceSP.GetArrayElementAtIndex (i).floatValue;
				listF[i] = current;

				listF[i] = EditorGUILayout.Slider (Regex.Replace (listT[i].gameObject.name, "(\\B[A-Z])", " $1"), listF[i], 0f, 1f);

				if (listF[i] > prevRT)
					listF[i] = prevRT;
				prevRT = listF[i];

				current = listF[i];
			}

			if (EditorGUI.EndChangeCheck ())
			{
				itemSpawnerSO.ApplyModifiedProperties ();
				EditorUtility.SetDirty (itemSpawner);
			}
		}
	}
}