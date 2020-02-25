using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class zeminOlustur : MonoBehaviour {
   
    public List<itemCupe> itemFloor ;
	public static int[,] floorArrayNum=new int[10,10];
	public static GameObject[,] floorGameObj=new GameObject[10,10];
    public bool aktif = false;
    public GameObject imageTween;
    [SerializeField]
    private Transform puzzleField;


    [SerializeField]
    private GameObject floor;

   
	// Use this for initialization
    void Start()
    {
		int i=1;
		for(int k=0;k<10;k++){
			for(int t=0;t<10;t++){
				GameObject imageFloor = (GameObject)Instantiate(floor);
				imageFloor.name =""+i;
				imageFloor.transform.SetParent(puzzleField,false);
				floorArrayNum [k, t] = 0;
				i++;
			}

		}
     

    }

    void Update()
    {
        if (aktif)
            aktifEt();
    }

    public void aktifEt()
    {
        aktif = false;
        imageTween.GetComponent<EasyTween>().OpenCloseObjectAnimation();
       
    }
}
