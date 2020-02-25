using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class dragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	//Camera camera;
    Vector3 transformObject;
    public float xx, yy;
    public Vector2 eventPosition;
    Transform startParent;
    public static GameObject itemDragged;
    public static GameObject[] transformObjects = new GameObject[9];
    public static int cupeCount = 0;
    void Start()
    {
        //camera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        itemDragged = gameObject;
        transformObject = transform.position;
        startParent = transform.parent;

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {

            transformObjects[i] = gameObject.transform.GetChild(i).gameObject;
            cupeCount++;
        }
        Debug.Log("Drag On");

        gameObject.GetComponent<RectTransform>().localScale = new Vector3(1.4f, 1.4f);

        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        xx = Input.mousePosition.x;
        yy = Input.mousePosition.y;
        gameObject.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, 1));
    }
    public void OnEndDrag(PointerEventData eventData)
    {

        itemDragged = null;
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        if (transform.parent == startParent)
        {
            gameObject.transform.position = transformObject;
        }

        cupeCount = 0;

        gameObject.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f);

        transformObjects = new GameObject[9];

        Debug.Log("Drag End");
    }

}
