using UnityEngine;
using System.Collections;

public class eysButton : MonoBehaviour {
	public GameObject blackPAnel;

	void OnMouseUp(){
		blackPAnel.SetActive (true);
	}

	void OnMouseDrag(){

	}

	void OnMouseDown(){
		blackPAnel.SetActive (false);
	}
}
