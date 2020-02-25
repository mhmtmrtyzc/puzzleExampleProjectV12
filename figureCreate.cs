using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;


public class figureCreate : MonoBehaviour {
	public Text newSkor, heighSkor;
	public Button pauseBtn;
	public Color[] colorScala = new Color[6];
	public int bringAFigureCount = 0,width=0,height=0;
	int selectColor;
    public List<itemCupe> dragObj1;
    public List<itemCupe> dragObj2;
    public List<itemCupe> dragObj3;
    public GameObject[] gamePrefabs = new GameObject[19];
//    List<int[,]> dragFigureObjs = new List<int[,]>();
    public float onePlace, secondPlace, tirthPlace,heigthHalf;
	public Image PanelWH,tentenImage;
	public GameObject PanelWHFigure,mainDisplayPanel;

	public GameObject tempFigureObject,tempFigureObject2,tempFigureObject3;
	public AnimationCurve startCurve, endCurve;
	public Transform transform1, transform2, transform3;
	public Transform transform1After, transform2After, transform3After;
    //public List<int[,]> dragFigureObjs = new List<int[,]>();
	UITween.AnimationParts.State objState;
	public RectTransform rectT;

    void Start () {

		width = System.Convert.ToInt32(rectT.rect.width); 
		height = System.Convert.ToInt32(rectT.rect.height);

        onePlace=PanelWH.GetComponent<RectTransform>().rect.xMax*2/6;
        secondPlace = PanelWH.GetComponent<RectTransform>().rect.xMax * 2 / 3+ PanelWH.GetComponent<RectTransform>().rect.xMax * 2 / 6;
        tirthPlace = PanelWH.GetComponent<RectTransform>().rect.xMax * 2 - PanelWH.GetComponent<RectTransform>().rect.xMax * 2 / 6;
        heigthHalf = PanelWH.GetComponent<RectTransform>().rect.yMin;
		tentenImage.transform.localPosition=new Vector2(0,0);
		newSkor.transform.localPosition = new Vector2 (-150,0);
		heighSkor.transform.localPosition = new Vector2 (150,0);
		//pauseBtn.transform.localPosition = new Vector2 (Screen.width/2-30,0);
		heighSkor.text=PlayerPrefs.GetInt ("highSkor").ToString();

		if (PlayerPrefs.GetInt ("setMainMenu") == 1) {
			figureCreateObj ();
			mainDisplayPanel.SetActive (false);

		} else {
			mainDisplayPanel.SetActive (true);
		}

    }

    public void figureCreateObj()
    {
		
		// create object 1
		tempFigureObject=(GameObject)Instantiate(gamePrefabs[UnityEngine.Random.Range(0, 19)],PanelWHFigure.transform);
        tempFigureObject.transform.localScale = new Vector3(1,1,1);

		selectColor = UnityEngine.Random.Range (0, 8);
		for(int i = 0 ; i<tempFigureObject.transform.childCount;i++){
			tempFigureObject.transform.GetChild(i).gameObject.transform.GetComponent<Image>().color=colorScala[selectColor];

		}

		tempFigureObject.transform.localPosition=new Vector2(Screen.width+1000,heigthHalf-(tempFigureObject.GetComponent<BoxCollider2D>().size.y/2));
		tempFigureObject.GetComponent<EasyTween> ().rectTransform = tempFigureObject.GetComponent<RectTransform>();
		tempFigureObject.GetComponent<EasyTween> ().animationParts.SetAniamtioDuration(0.5f);
		tempFigureObject.GetComponent<EasyTween> ().SetAnimationPosition(new Vector2 (onePlace,heigthHalf-(tempFigureObject.GetComponent<BoxCollider2D>().size.y/2)),new Vector2(Screen.width+1000,heigthHalf-(tempFigureObject.GetComponent<BoxCollider2D>().size.y/2)),startCurve,endCurve);
		tempFigureObject.GetComponent<EasyTween>().animationParts.ObjectState=UITween.AnimationParts.State.OPEN;
		tempFigureObject.GetComponent<EasyTween>().animationParts.EndState=UITween.AnimationParts.EndTweenClose.NOTHING;
		tempFigureObject.GetComponent<EasyTween>().OpenCloseObjectAnimation();

		// create object 2
		tempFigureObject2=(GameObject)Instantiate(gamePrefabs[UnityEngine.Random.Range(0, 19)],PanelWHFigure.transform);
		tempFigureObject2.transform.localScale = new Vector3(1,1,1);
		selectColor = UnityEngine.Random.Range (0, 8);
		for(int i = 0 ; i<tempFigureObject2.transform.childCount;i++){
			tempFigureObject2.transform.GetChild(i).gameObject.transform.GetComponent<Image>().color=colorScala[selectColor];

		}

		tempFigureObject2.transform.localPosition=new Vector2(Screen.width+1000,heigthHalf-(tempFigureObject2.GetComponent<BoxCollider2D>().size.y/2));
		tempFigureObject2.GetComponent<EasyTween> ().rectTransform = tempFigureObject2.GetComponent<RectTransform>();
		tempFigureObject2.GetComponent<EasyTween> ().animationParts.SetAniamtioDuration(0.5f);
		tempFigureObject2.GetComponent<EasyTween> ().SetAnimationPosition(new Vector2 (secondPlace,heigthHalf-(tempFigureObject2.GetComponent<BoxCollider2D>().size.y/2)),new Vector2(Screen.width+1000,heigthHalf-(tempFigureObject2.GetComponent<BoxCollider2D>().size.y/2)),startCurve,endCurve);
		tempFigureObject2.GetComponent<EasyTween>().animationParts.ObjectState=UITween.AnimationParts.State.OPEN;
		tempFigureObject2.GetComponent<EasyTween>().animationParts.EndState=UITween.AnimationParts.EndTweenClose.NOTHING;
		tempFigureObject2.GetComponent<EasyTween> ().OpenCloseObjectAnimation ();


		// create object 3
		tempFigureObject3=(GameObject)Instantiate(gamePrefabs[UnityEngine.Random.Range(0, 19)],PanelWHFigure.transform);
		tempFigureObject3.transform.localScale = new Vector3(1,1,1);
		selectColor = UnityEngine.Random.Range (0, 8);
		for(int i = 0 ; i<tempFigureObject3.transform.childCount;i++){
			
			tempFigureObject3.transform.GetChild(i).gameObject.transform.GetComponent<Image>().color=colorScala[selectColor];

		}

		tempFigureObject3.transform.localPosition=new Vector2(Screen.width+1000,heigthHalf-(tempFigureObject3.GetComponent<BoxCollider2D>().size.y/2));
		tempFigureObject3.GetComponent<EasyTween> ().rectTransform = tempFigureObject3.GetComponent<RectTransform>();
		tempFigureObject3.GetComponent<EasyTween> ().animationParts.SetAniamtioDuration(0.5f);
		tempFigureObject3.GetComponent<EasyTween> ().SetAnimationPosition(new Vector2 (tirthPlace,heigthHalf-(tempFigureObject3.GetComponent<BoxCollider2D>().size.y/2)),new Vector2(Screen.width+1000,heigthHalf-(tempFigureObject3.GetComponent<BoxCollider2D>().size.y/2)),startCurve,endCurve);
		tempFigureObject3.GetComponent<EasyTween>().animationParts.ObjectState=UITween.AnimationParts.State.OPEN;
		tempFigureObject3.GetComponent<EasyTween>().animationParts.EndState=UITween.AnimationParts.EndTweenClose.NOTHING;
		tempFigureObject3.GetComponent<EasyTween> ().OpenCloseObjectAnimation ();

		for(int i = 0 ; i<PanelWHFigure.transform.childCount;i++){
			
			if(PanelWHFigure.transform.GetChild(i).transform.childCount==0)
				Destroy (PanelWHFigure.transform.GetChild(i).gameObject,3f);

		}

    }

}
