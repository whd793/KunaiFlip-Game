using System;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ColorSchemen : MonoBehaviour
{
 
    public Material badassRef;
  
    public Material platformRef;

	public Material hitplatformRef;

    public List<Material> badass;


    public List<Material> platform;

	public List<Material> hitplatform;
	public GameObject knifeImage;

	public GameObject pBnz;
	public GameObject rBnz;
	public GameObject lBnz;
	public GameObject bBnz;
	public GameObject bbBnz;
	public GameObject reBnz;
	public GameObject reeBnz;
    private void Start()
    {

		int playerGameStatez = PlayerPrefs.GetInt("playerGameStatez");
		if (playerGameStatez == 0) {
			int ranz = UnityEngine.Random.Range (0, this.platform.Count);
			this.platformRef.color = this.platform [ranz].color;
			this.hitplatformRef.color = this.hitplatform [ranz].color;

			knifeImage.GetComponent<SpriteRenderer> ().color = this.platformRef.color;


		} else if (playerGameStatez == 1) {
			this.platformRef.color = this.platform [Base.currentLevel % 3].color;
			this.hitplatformRef.color = this.hitplatform [Base.currentLevel % 3].color;
			knifeImage.GetComponent<SpriteRenderer> ().color = this.platformRef.color;

				} else {
			this.platformRef.color = this.platform [Base.currentLevel % 3].color;
			this.hitplatformRef.color = this.hitplatform [Base.currentLevel % 3].color;
			knifeImage.GetComponent<SpriteRenderer> ().color = this.platformRef.color;

			}


    }

}
