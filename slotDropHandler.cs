using UnityEngine;
using System.Collections;
using System;

public class slotDropHandler : MonoBehaviour {
	
    public GameObject hoverSlots;
    public GameObject ControlObject;
	public AnimationCurve startCurve, endCurve;




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

	// Üstündeki Objeyi içine alıyor ve Hücrenin animasyonunu hazırlıyor
   public void dropManuel()
    {
        if (!hoverSlots)
            return;

        if (!item)
        {
            hoverSlots.transform.SetParent(transform);
            transform.GetChild(1).GetComponent<RectTransform>().localScale = new Vector3(1f, 1f);
			transform.GetComponent<EasyTween> ().rectTransform = transform.GetChild (1).gameObject.GetComponent<RectTransform>();
			transform.GetComponent<EasyTween> ().animationParts.ObjectState = UITween.AnimationParts.State.OPEN;
			transform.GetComponent<EasyTween> ().animationParts.EndState = UITween.AnimationParts.EndTweenClose.DESTROY;
			transform.GetComponent<EasyTween> ().SetAnimatioDuration (0.3f);
			transform.GetComponent<EasyTween> ().SetAnimationScale (new Vector3(0,0,0),new Vector3(1,1,1),startCurve,endCurve);
        }
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
