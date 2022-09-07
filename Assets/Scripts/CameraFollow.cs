using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform target;
	public static CameraFollow instance;
	public GameObject spikez;
	private Vector3 originalPosz;
//	public static bool gameOva = false;
	public float camSpeedz;
	void Start() {
		
//		Vector3 spikezPos = new Vector3 (transform.position.x, target.position.y - 26f, -10.6f);
		instance = this;
//		gameOva = false;
//		camSpeedz = 1.5f;
	


//		}

	}


	// void LateUpdate
	void Update () {

		if (target.position.y > transform.position.y) {
			Vector3 newPos = new Vector3 (transform.position.x, target.position.y, -15f);
			transform.position = newPos;
//
//			if (spikez.transform.position.y < target.position.y - 43f) {
//				Vector3 spikezPos = new Vector3 (transform.position.x, target.position.y - 43f, -14f);
//				spikez.transform.position = spikezPos;
//			}

		} 

		if (spikez.transform.position.y < transform.position.y - 32f) {
							Vector3 spikezPos = new Vector3 (transform.position.x, transform.position.y - 32f, -14f);
							spikez.transform.position = spikezPos;
						}

//		if (gameOva == false) {
		if (BaseSceneManager<mc>.Instance.isActive == true) {
			if (target.position.y > 2f) {
				transform.position += new Vector3 (0, Time.deltaTime * 1.5f, 0);
			}
//		}
		}



	}


	public static void Shake (float duration, float amount) {
		instance.StartCoroutine (instance.coShake(duration, amount));
	}

	public IEnumerator coShake(float duration, float amount) {
		float endTime = Time.time + duration;
		originalPosz = transform.position;
		//		CameraFollow.gameOva = true;

		while (Time.time < endTime) {
			transform.localPosition = new Vector3(transform.position.x + Random.insideUnitSphere.x * amount, transform.position.y + Random.insideUnitSphere.y * amount, transform.position.z);
			duration = duration - Time.deltaTime;
			yield return null;
		}
		transform.position = new Vector3 (originalPosz.x, originalPosz.y, originalPosz.z);
	}



}
