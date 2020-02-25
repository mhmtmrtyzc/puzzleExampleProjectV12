using UnityEngine;
using System.Collections;

public class floorInSide : MonoBehaviour {
    void OnMouseDown()
    {
        Debug.Log("OnMouseUp!");
    }
    public GameObject item
    {
        get
        {
            if (transform.childCount > 1)
            {
                return transform.GetChild(1).gameObject;
            }
            else
                return null;

        }
    }
    
    void Start()
    {

    }
    void Update()
    {
        for (int i = 0; i < Input.touchCount; ++i)
        {

            if (Input.GetTouch(i).phase == TouchPhase.Ended)
            {

                Debug.Log("OnMouseUp!");
            }

        }

    }
    /* void OnMouseUp()
     {
         if (!item)
         {

             triggerControl.hoverObject.transform.SetParent(transform);
             transform.GetChild(1).GetComponent<RectTransform>().localScale = new Vector3(1f, 1f);





         }
     }*/
    /*
    public void OnDrop(PointerEventData eventData)
    {
        

       
                if (!item)
                {

                   triggerControl.hoverObject.transform.SetParent(transform);
                   transform.GetChild(1).GetComponent<RectTransform>().localScale = new Vector3(1f, 1f);

                    

                   

                }
           
       
    }*/

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
