using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;

public class mouseUpScript : MonoBehaviour {
	//public Image screenShot;
	public GameObject menuanim,starButton, ligtButtonGameOver, lightButton, playButton;
	public GameObject backColorPanel,puzzleAreaPanel;
	public bool timeScale = false;
	int girisSayisi=0;
	Vector3 transformCupeObjects;
	bool returnPosition = false,GameOwer=false,bool1=false,bool3 = false,objcChangeParent=false;
	int cupeCount=0,cupeCountControl=0;
	public int[] onCupe;
	int j = 0;
	GameObject tempDropObject,lowerPanel,panel;
	AnimationCurve startCurve,endCurve;
	List<KeyValuePair<int,int>> satir = new List<KeyValuePair<int,int>> ();
	List<KeyValuePair<int,int>> sutun = new List<KeyValuePair<int,int>> ();
	//bool[] satirIslem = null;
	figureCreate fgrClass ;
	menuFunction menuAnimFunction;



	void Start(){
		starButton = GameObject.Find ("star").gameObject;
		ligtButtonGameOver = GameObject.Find ("backLightGameOver").gameObject;
		menuanim = GameObject.Find ("menuAnim").gameObject;
		backColorPanel = GameObject.Find ("BlackPanel").gameObject;



		lowerPanel = GameObject.Find ("Panelfigure").gameObject;
		panel = GameObject.Find ("Panel").gameObject;
		fgrClass = panel.GetComponent<figureCreate> ();



		menuAnimFunction = GameObject.Find ("menuAnim").gameObject.GetComponent<menuFunction> ();

	}
	void Update(){

		if(bool3){

			bool3 = false;
			for (int i = 0; i < 10; i++) {
				for (int j = 0; j < 10; j++) {

					if(zeminOlustur.floorArrayNum[i,j]==3)
						zeminOlustur.floorArrayNum[i,j]=1;

				}
			}

		}

		if(bool1)
		{
			bool1 = false;
			floorTenTenControl();

			if(satir.Count>0 || sutun.Count>0)
				menuanim.GetComponent<AudioSource>().PlayOneShot(menuAnimFunction.sesDosyalari[1], PlayerPrefs.GetInt("volumeGame"));
			else
				menuanim.GetComponent<AudioSource>().PlayOneShot(menuAnimFunction.sesDosyalari[3], PlayerPrefs.GetInt("volumeGame"));
			
			if (satir.Count > 0) {
				for (int i=0;i<satir.Count;i++) {
					fgrClass.newSkor.text = (IntParseFast (fgrClass.newSkor.text) + 10).ToString();
					StartCoroutine(destroyRow(satir[i].Key,satir[i].Value,i));
				}
			}

			if (sutun.Count > 0) {
				for (int i=0;i<sutun.Count;i++) {
					fgrClass.newSkor.text = (IntParseFast (fgrClass.newSkor.text) + 10).ToString();
					StartCoroutine(destroyColum(sutun[i].Key,sutun[i].Value));
				}
			}


				

			if (fgrClass.bringAFigureCount == 3) {
				fgrClass.bringAFigureCount = 0;

				fgrClass.figureCreateObj ();
			}

			ObjsPlaceControl ();

			//StartCoroutine (xx());
		}

		if (GameOwer) {
			menuanim.GetComponent<AudioSource>().PlayOneShot(menuAnimFunction.sesDosyalari[2], PlayerPrefs.GetInt("volumeGame"));
			print ("GAMEE OWEEERRRRR");
			GameOwer = false;
			menuAnimFunction.gameOver = true;
			if (IntParseFast (fgrClass.newSkor.text) > IntParseFast (fgrClass.heighSkor.text)) {
				PlayerPrefs.SetInt ("highSkor", IntParseFast (fgrClass.newSkor.text));
				fgrClass.heighSkor.text=PlayerPrefs.GetInt ("highSkor").ToString();
			}

			PlayerPrefs.SetInt ("setMainMenu",0);

			StartCoroutine (xx());



		}
	}



	IEnumerator xx() {


		yield return StartCoroutine( takeScreenShot ()); 
		//yield return new WaitForSecondsRealtime(0);
		print("resim kaydedildi");
	}

	public IEnumerator takeScreenShot()
	{
		String saveLocation = Application.persistentDataPath;
		yield return new WaitForEndOfFrame (); 

		if (PlayerPrefs.GetInt ("mainBackColor") == 0) {


			backColorPanel.GetComponent<Image> ().color = menuAnimFunction.cameraBackGround[0];

		} else if (PlayerPrefs.GetInt ("mainBackColor") == 1) {


			backColorPanel.GetComponent<Image> ().color =  menuAnimFunction.cameraBackGround[1];
		}

		var tex = new Texture2D (Screen.width,Screen.width, TextureFormat.RGB24, false);


		tex = new Texture2D (Screen.width*570/600,Screen.width*570/600, TextureFormat.RGB24, false);
		tex.ReadPixels (new Rect(Screen.width/2f-(Screen.width*570/600)/2f,Screen.height/2f-(Screen.width*570/600)/2f,Screen.width*570f/600f,Screen.width*570f/600f), 0, 0);


		tex.Apply ();

		// Encode texture into PNG
		var bytes = tex.EncodeToPNG();

		Destroy(tex);

		File.WriteAllBytes(saveLocation + "ScreenShot.jpg", bytes);

		//Kaydedilen Görüntüyü alıp Sprite a çevirip gerekli objeye atıyoruz 

		Texture2D texGet = new Texture2D (Screen.width*570/600,Screen.width*570/600, TextureFormat.RGB24, false);
		texGet.LoadImage(File.ReadAllBytes (saveLocation+"ScreenShot.jpg"));
		Sprite spriteScreen = Sprite.Create(texGet, new Rect(0, 0, texGet.width, texGet.height), new Vector2(0.5f, 0.5f));

		menuAnimFunction.screenShot.GetComponent<Image> ().sprite = spriteScreen;

		////////////////////////////////

		backColorPanel.GetComponent<Image> ().enabled = true;

		starButton.GetComponent<Image> ().enabled = true;
		starButton.transform.GetChild (0).gameObject.GetComponent<Image> ().enabled = true;
		ligtButtonGameOver.GetComponent<Image> ().enabled = true;
		ligtButtonGameOver.transform.GetChild (0).gameObject.GetComponent<Image> ().enabled = true;

		menuAnimFunction.adsButton.SetActive (true);
		menuAnimFunction.eysButton.SetActive (true);
		menuAnimFunction.menuEysGameObj.SetActive (true);

		if (PlayerPrefs.GetInt ("mainBackColor") == 0) {

			menuAnimFunction.screenShotBackColor.GetComponent<Image> ().color = menuAnimFunction.cameraBackGround [0];

			menuAnimFunction.adsButton.GetComponent<Image>().color=menuAnimFunction.cameraBackGround[7];

			menuAnimFunction.eysButton.GetComponent<Image>().color=menuAnimFunction.cameraBackGround[7];

		} else if (PlayerPrefs.GetInt ("mainBackColor") == 1) {

			menuAnimFunction.screenShotBackColor.GetComponent<Image> ().color = menuAnimFunction.cameraBackGround [1];

			menuAnimFunction.adsButton.GetComponent<Image>().color=menuAnimFunction.cameraBackGround[8];

			menuAnimFunction.eysButton.GetComponent<Image>().color=menuAnimFunction.cameraBackGround[8];

		}

		menuAnimFunction.gameOverSkor.text = fgrClass.newSkor.text;
		menuAnimFunction.sharePanelAnim.GetComponent<EasyTween> ().OpenCloseObjectAnimation ();
		menuanim.GetComponent<EasyTween> ().OpenCloseObjectAnimation ();

		for (int i = 0; i < lowerPanel.transform.childCount; i++) {
			lowerPanel.transform.GetChild (i).GetComponent<mouseUpScript> ().enabled = false;
		}
	}

	void OnMouseUp()
	{
		if (menuAnimFunction.timeScale)
			return;

		onCupe = new int[cupeCount];
		for (int i = 0; i < 100; i++)
		{
			if (GameObject.Find("puzzleArea").transform.GetChild(i).gameObject.GetComponent<slotDropHandler>().hoverSlots != null)
			{
				onCupe[j] = i;
				j++;
			}

			if (GameObject.Find("puzzleArea").transform.GetChild(i).gameObject.GetComponent<slotDropHandler>().ControlObject != null && GameObject.Find("puzzleArea").transform.GetChild(i).gameObject.transform.childCount < 2)
				cupeCountControl++; 
		}

		j = 0;

		if (cupeCountControl==cupeCount)
		{
			for (int i = 0; i < onCupe.Length; i++)
			{

				if (GameObject.Find("puzzleArea").transform.GetChild(onCupe[i]).gameObject.GetComponent<slotDropHandler>().hoverSlots != null)
				{
					girisSayisi++;


					if (GameObject.Find("puzzleArea").transform.GetChild(onCupe[i]).childCount < 2)
					{
						GameObject.Find("puzzleArea").transform.GetChild(onCupe[i]).gameObject.GetComponent<slotDropHandler>().dropManuel();

						zeminOlustur.floorGameObj[onCupe[i]/10,onCupe[i]%10]=GameObject.Find("puzzleArea").transform.GetChild(onCupe[i]).gameObject;

						GameObject.Find("puzzleArea").transform.GetChild(onCupe[i]).gameObject.GetComponent<slotDropHandler>().hoverSlots = null;
						GameObject.Find("puzzleArea").transform.GetChild(onCupe[i]).gameObject.GetComponent<slotDropHandler>().ControlObject = null;

						zeminOlustur.floorArrayNum[onCupe[i]/10,onCupe[i]%10]=3;
						objcChangeParent = true;

						returnPosition = false;

					}
					else
						returnPosition = true;

				}
				else
					returnPosition = true;
			}



		}
		else
			returnPosition = true;


		if (returnPosition) {
			transform.localScale = new Vector3 (1f, 1f);
			transform.position = transformCupeObjects;
			for (int i = 0; i < transform.childCount; i++) {

				transform.GetChild (i).gameObject.transform.GetChild (0).GetComponent<BoxCollider2D> ().enabled = false;
				transform.GetChild (i).gameObject.GetComponent<EasyTween> ().OpenCloseObjectAnimation ();

			}
			for (int i = 0; i < GameObject.Find ("puzzleArea").transform.childCount; i++) {
				if (GameObject.Find ("puzzleArea").transform.GetChild (i).gameObject.GetComponent<slotDropHandler> ().ControlObject != null) {
					GameObject.Find ("puzzleArea").transform.GetChild (i).gameObject.GetComponent<slotDropHandler> ().ControlObject = null;
					GameObject.Find ("puzzleArea").transform.GetChild (i).gameObject.GetComponent<slotDropHandler> ().hoverSlots = null;
				}
			}

			returnPosition = false;
		} else {
			bool1 = true;

			fgrClass.newSkor.text = (IntParseFast (fgrClass.newSkor.text) + cupeCount).ToString();
		}

		if (objcChangeParent) {
			objcChangeParent = false;
			fgrClass.GetComponent<figureCreate> ().bringAFigureCount++;
		}
		cupeCount = 0;


	}
	// Satır sutun 10 kutuda dolumu kontrolü
	void floorTenTenControl(){
		satir = new List<KeyValuePair<int,int>> ();
		sutun = new List<KeyValuePair<int,int>> ();
		int control = 0,controlHor=0,h=0,v=0,oldumu=0;
		for (int i = 0; i < 10; i++) {
			for (int j = 0; j < 10;j++) {
				if (zeminOlustur.floorArrayNum [i,j] == 1 || zeminOlustur.floorArrayNum [i,j] == 2 || zeminOlustur.floorArrayNum [i,j] == 3
					|| zeminOlustur.floorArrayNum [i,j] == 4) 
					control++;

				if (zeminOlustur.floorArrayNum [i, j] == 3
					|| zeminOlustur.floorArrayNum [i, j] == 4)
					v = j;

				if (zeminOlustur.floorArrayNum [j,i] == 1 || zeminOlustur.floorArrayNum [j,i] == 2 || zeminOlustur.floorArrayNum [j,i] == 3
					|| zeminOlustur.floorArrayNum [j,i] == 4) 
					controlHor++;

				if (zeminOlustur.floorArrayNum [j,i] == 3
					|| zeminOlustur.floorArrayNum [j,i] == 4)
					h = j;

			} 

			if (control == 10) {
				//yield return StartCoroutine(destroyRow(i,v));
				satir.Add (new KeyValuePair<int,int> (i, v));
				//satir.Add (i);
				//destroyRow (i,v);
				control = 0;
				oldumu++;
			} else {
				control = 0;
			}

			if (controlHor == 10) {
				//destroyColum (i);
				sutun.Add (new KeyValuePair<int,int> (i, h));
				//sutun.Add(i);
				controlHor = 0;
				oldumu++;
			} else {
				controlHor = 0;
			}


		}

		for (int i = 0; i < satir.Count; i++) {
			for (int j = 0; j < 10; j++) {
				zeminOlustur.floorArrayNum [satir[i].Key, j] = 0;
			}
		}



		for (int i = 0; i < sutun.Count; i++) {
			for (int j = 0; j < 10; j++) {
				//zeminOlustur.floorGameObj [i,v] = null;
				zeminOlustur.floorArrayNum [j,sutun[i].Key] = 0;
			}
		}

		if (oldumu == 0)
			bool3 = true;




	}
	// Satır yoketmek için
	IEnumerator destroyRow(int i,int v,int arrayId){


		if (zeminOlustur.floorGameObj [i,v]!=null) {

			zeminOlustur.floorGameObj [i,v].GetComponent<EasyTween> ().OpenCloseObjectAnimation ();
			zeminOlustur.floorGameObj [i,v] = null;
			//zeminOlustur.floorArrayNum [i,v] = 4;

		}
		StartCoroutine(destroyRowLeft(i,v));
		StartCoroutine(destroyRowRight(i,v));

		//		bool2 = true;

		yield return new WaitForSecondsRealtime (0);
	}

	IEnumerator destroyRowLeft(int i ,int v){

		for(int t=i;t<i+1;t++){
			for(int m=v-1;m>-1;m--){
				if (zeminOlustur.floorGameObj [t ,m] != null) {
					zeminOlustur.floorGameObj [t ,m ].GetComponent<EasyTween> ().OpenCloseObjectAnimation ();
					zeminOlustur.floorGameObj [t ,m ] = null;
					yield return new WaitForSeconds (0.03f);
				}
			}
		}


	}

	IEnumerator destroyRowRight(int i ,int v){

		for(int t=i;t<i+1;t++){
			for(int m=v+1;m<10;m++){
				if (zeminOlustur.floorGameObj [t ,m] != null) {
					zeminOlustur.floorGameObj [t ,m ].GetComponent<EasyTween> ().OpenCloseObjectAnimation ();
					zeminOlustur.floorGameObj [t ,m ] = null;
					yield return new WaitForSeconds (0.03f);
				}
			}
		}

	}

	IEnumerator destroyColum(int i,int h){


		if (zeminOlustur.floorGameObj [h,i]!=null ) {

			zeminOlustur.floorGameObj [h,i].GetComponent<EasyTween> ().OpenCloseObjectAnimation ();
			zeminOlustur.floorGameObj [h,i] = null;

		}
		StartCoroutine(destroyColumUp(h,i));
		StartCoroutine(destroyColumDown(h,i));

		yield return new WaitForSecondsRealtime (0);
	}

	IEnumerator destroyColumUp(int h,int i){

		for(int t=i;t<i+1;t++){
			for(int m=h-1;m>-1;m--){
				if (zeminOlustur.floorGameObj [m ,t] != null) {
					zeminOlustur.floorGameObj [m ,t ].GetComponent<EasyTween> ().OpenCloseObjectAnimation ();
					zeminOlustur.floorGameObj [m, t] = null;
					yield return new WaitForSeconds (0.03f);
				}
			}
		}
	}

	IEnumerator destroyColumDown(int h,int i){

		for(int t=i;t<i+1;t++){
			for(int m=h+1;m<10;m++){
				if (zeminOlustur.floorGameObj [m ,t] != null) {
					zeminOlustur.floorGameObj [m ,t ].GetComponent<EasyTween> ().OpenCloseObjectAnimation ();
					zeminOlustur.floorGameObj [m, t] = null;
					yield return new WaitForSeconds (0.03f);
				}
			}
		}
	}

	void ObjsPlaceControl(){

		bool objPlaceBool = false;
		int objPlaceNum = 0;

		for(int i= 0 ; i<lowerPanel.transform.childCount;i++){

			if (lowerPanel.transform.GetChild (i).transform.childCount >= 1) {
				
				objPlaceBool = ObjControl (lowerPanel.transform.GetChild(i).gameObject.name);

				if (objPlaceBool) {
					objPlaceBool = false;
					objPlaceNum++;
				}
			}

			if (objPlaceNum > 0)
				break;
		}

		if (objPlaceNum == 0)
			GameOwer = true;
		else {
			objPlaceNum = 0;
			GameOwer = false;
			print ("YER VARRRRRRRR");
		}

	}

	bool ObjControl(string name){
		name = name.Remove (name.Length-7,7);
		int emptySpace = 0;

		if (name.Equals ("RR3")) {

			for (int x = 0; x < 8; x++) {
				for (int y = 0; y < 8; y++) {

					if (zeminOlustur.floorArrayNum [x + 2, y + 2] == 0) {
						emptySpace++;
						if (zeminOlustur.floorArrayNum [x + 1, y + 2] == 0) {
							emptySpace++;
							if (zeminOlustur.floorArrayNum [x, y + 2] == 0) {
								emptySpace++;
								if (zeminOlustur.floorArrayNum [x + 2, y + 1] == 0) {
									emptySpace++;
									if (zeminOlustur.floorArrayNum [x + 2, y] == 0)
										emptySpace++;
								}
							}
						}
					}

					if (emptySpace == 5)
						return true;
					else
						emptySpace = 0;
				}


			}

			return false;

		} else if (name.Equals ("RR2")) {

			for (int x = 0; x < 8; x++) {
				for (int y = 0; y < 8; y++) {

					if (zeminOlustur.floorArrayNum [x, y] == 0) {
						emptySpace++;

						if (zeminOlustur.floorArrayNum [x, y + 1] == 0) {
							emptySpace++;

							if (zeminOlustur.floorArrayNum [x, y + 2] == 0) {
								emptySpace++;

								if (zeminOlustur.floorArrayNum [x + 1, y + 2] == 0) {
									emptySpace++;

									if (zeminOlustur.floorArrayNum [x + 2, y + 2] == 0)
										emptySpace++;
								}
							}
						}
					}


					if (emptySpace == 5)
						return true;
					else
						emptySpace = 0;
				}


			}

			return false;

		} else if (name.Equals ("RR4")) {

			for (int x = 0; x < 8; x++) {
				for (int y = 0; y < 8; y++) {

					if (zeminOlustur.floorArrayNum [x, y] == 0) {
						emptySpace++;
						if (zeminOlustur.floorArrayNum [x + 1, y] == 0) {
							emptySpace++;
							if (zeminOlustur.floorArrayNum [x + 2, y] == 0) {
								emptySpace++;
								if (zeminOlustur.floorArrayNum [x + 2, y + 1] == 0) {
									emptySpace++;
									if (zeminOlustur.floorArrayNum [x + 2, y + 2] == 0)
										emptySpace++;
								}
							}
						}
					}

					if (emptySpace == 5)
						return true;
					else
						emptySpace = 0;

				}


			}

			return false;

		} else if (name.Equals ("RR1")) {

			for (int x = 0; x < 8; x++) {
				for (int y = 0; y < 8; y++) {

					if (zeminOlustur.floorArrayNum [x, y] == 0) {
						emptySpace++;
						if (zeminOlustur.floorArrayNum [x + 1, y] == 0) {
							emptySpace++;
							if (zeminOlustur.floorArrayNum [x + 2, y] == 0) {
								emptySpace++;
								if (zeminOlustur.floorArrayNum [x, y + 1] == 0) {
									emptySpace++;
									if (zeminOlustur.floorArrayNum [x, y + 2] == 0)
										emptySpace++;
								}
							}
						}
					}

					if (emptySpace == 5)
						return true;
					else
						emptySpace = 0;
				}


			}

			return false;

		} else if (name.Equals ("1d")) {

			for (int x = 0; x < 10; x++) {
				for (int y = 0; y < 10; y++) {
					if (zeminOlustur.floorArrayNum [x, y] == 0)
						return true;
				}
			}
		} else if (name.Equals ("2d")) {
			for (int x = 0; x < 9; x++) {
				for (int y = 0; y < 10; y++) {
					if (zeminOlustur.floorArrayNum [x, y] == 0) {
						emptySpace++;

						if (zeminOlustur.floorArrayNum [x + 1, y] == 0)
							emptySpace++;
					}
					if (emptySpace == 2)
						return true;
					else
						emptySpace = 0;
				}
			}
		}else if (name.Equals ("2y")) {
			for (int x = 0; x < 10; x++) {
				for (int y = 0; y < 9; y++) {
					if (zeminOlustur.floorArrayNum [x, y] == 0) {
						emptySpace++;

						if (zeminOlustur.floorArrayNum [x, y + 1] == 0)
							emptySpace++;
					}
					if (emptySpace == 2)
						return true;
					else
						emptySpace = 0;
				}
			}
		}else if (name.Equals ("3d")) {
			for (int x = 0; x < 8; x++) {
				for (int y = 0; y < 10; y++) {
					if (zeminOlustur.floorArrayNum [x, y] == 0) {
						emptySpace++;

						if (zeminOlustur.floorArrayNum [x + 1, y] == 0) {
							emptySpace++;

							if (zeminOlustur.floorArrayNum [x + 2, y] == 0)
								emptySpace++;
						}
					}
					if (emptySpace == 3)
						return true;
					else
						emptySpace = 0;
				}
			}
		}
		else if (name.Equals ("3y")) {
			for (int x = 0; x < 10; x++) {
				for (int y = 0; y < 8; y++) {
					if (zeminOlustur.floorArrayNum [x, y] == 0) {
						emptySpace++;

						if (zeminOlustur.floorArrayNum [x, y + 1] == 0) {
							emptySpace++;

							if (zeminOlustur.floorArrayNum [x, y + 2] == 0)
								emptySpace++;
						}
					}
					if (emptySpace == 3)
						return true;
					else
						emptySpace = 0;
				}
			}
		}else if (name.Equals ("2k")) {
			for (int x = 0; x < 9; x++) {
				for (int y = 0; y < 9; y++) {
					if (zeminOlustur.floorArrayNum [x, y] == 0) {
						emptySpace++;

						if (zeminOlustur.floorArrayNum [x, y + 1] == 0) {
							emptySpace++;

							if (zeminOlustur.floorArrayNum [x + 1, y] == 0) {
								emptySpace++;

								if (zeminOlustur.floorArrayNum [x + 1, y + 1] == 0)
									emptySpace++;
							}
						}
					}
					if (emptySpace == 4)
						return true;
					else
						emptySpace = 0;
				}
			}
		}else if (name.Equals ("3dy")) {
			for (int x = 0; x < 8; x++) {
				for (int y = 0; y < 8; y++) {
					if (zeminOlustur.floorArrayNum [x, y] == 0) {
						emptySpace++;

						if (zeminOlustur.floorArrayNum [x, y + 1] == 0) {
							emptySpace++;

							if (zeminOlustur.floorArrayNum [x, y + 2] == 0) {
								emptySpace++;

								if (zeminOlustur.floorArrayNum [x + 1, y] == 0) {
									emptySpace++;

									if (zeminOlustur.floorArrayNum [x + 1, y + 1] == 0) {
										emptySpace++;

										if (zeminOlustur.floorArrayNum [x + 1, y + 2] == 0) {
											emptySpace++;

											if (zeminOlustur.floorArrayNum [x + 2, y] == 0) {
												emptySpace++;

												if (zeminOlustur.floorArrayNum [x + 2, y + 1] == 0) {
													emptySpace++;

													if (zeminOlustur.floorArrayNum [x + 2, y + 2] == 0)
														emptySpace++;
												}
											}
										}
									}
								}
							}
						}
					}
					if (emptySpace == 9)
						return true;
					else
						emptySpace = 0;
				}
			}
		}else if (name.Equals ("4y")) {
			for (int x = 0; x < 10; x++) {
				for (int y = 0; y < 7; y++) {
					if (zeminOlustur.floorArrayNum [x, y] == 0) {
						emptySpace++;

						if (zeminOlustur.floorArrayNum [x, y + 1] == 0) {
							emptySpace++;

							if (zeminOlustur.floorArrayNum [x, y + 2] == 0) {
								emptySpace++;

								if (zeminOlustur.floorArrayNum [x, y + 3] == 0)
									emptySpace++;
							}
						}
					}
					if (emptySpace == 4)
						return true;
					else
						emptySpace = 0;
				}
			}
		}
		else if (name.Equals ("4d")) {
			for (int x = 0; x < 7; x++) {
				for (int y = 0; y < 10; y++) {
					if (zeminOlustur.floorArrayNum [x, y] == 0) {
						emptySpace++;

						if (zeminOlustur.floorArrayNum [x + 1, y] == 0) {
							emptySpace++;

							if (zeminOlustur.floorArrayNum [x + 2, y] == 0) {
								emptySpace++;

								if (zeminOlustur.floorArrayNum [x + 3, y] == 0)
									emptySpace++;
							}
						}
					}
					if (emptySpace == 4)
						return true;
					else
						emptySpace = 0;
				}
			}
		}
		else if (name.Equals ("5d")) {
			for (int x = 0; x < 6; x++) {
				for (int y = 0; y < 10; y++) {
					if (zeminOlustur.floorArrayNum [x, y] == 0) {
						emptySpace++;

						if (zeminOlustur.floorArrayNum [x + 1, y] == 0) {
							emptySpace++;

							if (zeminOlustur.floorArrayNum [x + 2, y] == 0) {
								emptySpace++;

								if (zeminOlustur.floorArrayNum [x + 3, y] == 0) {
									emptySpace++;

									if (zeminOlustur.floorArrayNum [x + 4, y] == 0)
										emptySpace++;
								}
							}
						}
					}
					if (emptySpace == 5)
						return true;
					else
						emptySpace = 0;
				}
			}
		}
		else if (name.Equals ("5y")) {
			for (int x = 0; x < 10; x++) {
				for (int y = 0; y < 6; y++) {
					if (zeminOlustur.floorArrayNum [x, y] == 0) {
						emptySpace++;

						if (zeminOlustur.floorArrayNum [x, y + 1] == 0) {
							emptySpace++;

							if (zeminOlustur.floorArrayNum [x, y + 2] == 0) {
								emptySpace++;

								if (zeminOlustur.floorArrayNum [x, y + 3] == 0) {
									emptySpace++;

									if (zeminOlustur.floorArrayNum [x, y + 4] == 0)
										emptySpace++;
								}
							}
						}
					}
					if (emptySpace == 5)
						return true;
					else
						emptySpace = 0;
				}
			}
		}
		else if (name.Equals ("r1")) {
			for (int x = 0; x < 9; x++) {
				for (int y = 0; y < 9; y++) {
					if (zeminOlustur.floorArrayNum [x, y] == 0) {
						emptySpace++;

						if (zeminOlustur.floorArrayNum [x, y + 1] == 0) {
							emptySpace++;

							if (zeminOlustur.floorArrayNum [x + 1, y] == 0) 
								emptySpace++;

						}
					}
					if (emptySpace == 3)
						return true;
					else
						emptySpace = 0;
				}
			}
		}
		else if (name.Equals ("r2")) {
			for (int x = 0; x < 9; x++) {
				for (int y = 0; y < 9; y++) {
					if (zeminOlustur.floorArrayNum [x, y] == 0) {
						emptySpace++;

						if (zeminOlustur.floorArrayNum [x, y + 1] == 0) {
							emptySpace++;

							if (zeminOlustur.floorArrayNum [x + 1, y+1] == 0) 
								emptySpace++;

						}
					}
					if (emptySpace == 3)
						return true;
					else
						emptySpace = 0;
				}
			}
		}
		else if (name.Equals ("r3")) {
			for (int x = 0; x < 9; x++) {
				for (int y = 0; y < 9; y++) {
					if (zeminOlustur.floorArrayNum [x, y+1] == 0) {
						emptySpace++;

						if (zeminOlustur.floorArrayNum [x+1, y + 1] == 0) {
							emptySpace++;

							if (zeminOlustur.floorArrayNum [x + 1, y] == 0) 
								emptySpace++;

						}
					}
					if (emptySpace == 3)
						return true;
					else
						emptySpace = 0;
				}
			}
		}
		else if (name.Equals ("r4")) {
			for (int x = 0; x < 9; x++) {
				for (int y = 0; y < 9; y++) {
					if (zeminOlustur.floorArrayNum [x, y] == 0) {
						emptySpace++;

						if (zeminOlustur.floorArrayNum [x+1, y] == 0) {
							emptySpace++;

							if (zeminOlustur.floorArrayNum [x + 1, y+1] == 0) 
								emptySpace++;

						}
					}
					if (emptySpace == 3)
						return true;
					else
						emptySpace = 0;
				}
			}
		}

		return false;
	}

	void OnMouseDown()
	{
		if (menuAnimFunction.timeScale)
			return;

		returnPosition = false;
		transformCupeObjects = transform.position;
		cupeCountControl = 0;

		// Cupe sayısı
		for(int i=0; i < transform.childCount; i++)
		{
			cupeCount++;
			transform.GetChild(i).gameObject.transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = true;
			transform.GetChild(i).gameObject.GetComponent<EasyTween>().OpenCloseObjectAnimation();
		}
	}

	void OnMouseDrag()
	{
		if (menuAnimFunction.timeScale)
			return;

		gameObject.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
	}


	public int IntParseFast(string value)
	{
		int result = 0;
		for (int i = 0; i < value.Length; i++)
		{
			char letter = value[i];
			result = 10 * result + (letter - 48);
		}
		return result;
	}
}
