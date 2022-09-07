using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeFlip : MonoBehaviour {



		void Update () {
		if (BaseSceneManager<mc>.Instance.isFirstStarted == true) {
			
				if (Input.GetMouseButtonDown (0)) {


					gameObject.GetComponent<Animator> ().Play ("flip");


				}

		}
			
	}
}
