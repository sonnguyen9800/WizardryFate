using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class MadLove : VersionedMonoBehaviour {
		/// <summary>The object that the AI should move to</summary>
		private GameObject target;
		IAstarAI ai;

		void OnEnable () {
			target = GameObject.FindGameObjectWithTag("DropLeft");
			ai = GetComponent<IAstarAI>();
			// Update the destination right before searching for a path as well.
			// This is enough in theory, but this script will also update the destination every
			// frame as the destination is used for debugging and may be used for other things by other
			// scripts as well. So it makes sense that it is up to date every frame.
			if (ai != null) ai.onSearchPath += Update;
		}

		void OnDisable () {
			if (ai != null) ai.onSearchPath -= Update;
		}

		/// <summary>Updates the AI's destination every frame</summary>
		void Update () {
			if (target != null && ai != null) ai.destination = target.transform.position;
		}


}
