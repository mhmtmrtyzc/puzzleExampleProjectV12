using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class triggerControl : MonoBehaviour {
    public GameObject hoverObject2;
    public static GameObject hoverObject;
	Color firstColor;
    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "t")
        {
           // print(gameObject.transform.parent.gameObject.transform.name + " = " + coll.transform.name);
			//firstColor= gameObject.transform.parent.gameObject.GetComponent<Image> ().color;

            if (gameObject.transform.parent.gameObject.transform.childCount == 1)
            {
                hoverObject = coll.transform.parent.gameObject;
                hoverObject2 = hoverObject;
                gameObject.transform.parent.gameObject.GetComponent<slotDropHandler>().ControlObject = coll.transform.parent.gameObject;
                gameObject.transform.parent.gameObject.GetComponent<slotDropHandler>().hoverSlots = coll.transform.parent.gameObject;
				//gameObject.transform.parent.gameObject.GetComponent<Image> ().color = coll.transform.parent.gameObject.GetComponent<Image> ().color;
            }
            else if(gameObject.transform.parent.gameObject.transform.childCount != 1)
            {
                gameObject.transform.parent.gameObject.GetComponent<slotDropHandler>().ControlObject = coll.transform.parent.gameObject;
            }
        }
    }
    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "t")
        {


            if (gameObject.transform.parent.gameObject.transform.childCount == 1)
            {
                hoverObject = null;
                hoverObject2 = hoverObject;
                gameObject.transform.parent.gameObject.GetComponent<slotDropHandler>().ControlObject = null;
                gameObject.transform.parent.gameObject.GetComponent<slotDropHandler>().hoverSlots = null;
				//gameObject.transform.parent.gameObject.GetComponent<Image> ().color = firstColor;
            }
            else if (gameObject.transform.parent.gameObject.transform.childCount != 1)
                {
                    gameObject.transform.parent.gameObject.GetComponent<slotDropHandler>().ControlObject = null;
                }
           
        }
            
    }

    

}
