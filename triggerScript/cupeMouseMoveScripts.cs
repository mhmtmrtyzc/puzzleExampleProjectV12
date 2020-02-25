using UnityEngine;
using System.Collections;

public class cupeMouseMoveScripts : MonoBehaviour {

    int girisSayisi = 0;
    Vector3 transformCupeObjects;
    bool returnPosition = false;
    int cupeCount = 0;
    void OnMouseUp()
    {
        for (int i = 0; i < GameObject.Find("puzzleArea").transform.childCount; i++)
        {

            if (GameObject.Find("puzzleArea").transform.GetChild(i).gameObject.GetComponent<slotDropHandler>().hoverSlots != null)
            {
                girisSayisi++;

                Debug.Log(GameObject.Find("puzzleArea").transform.GetChild(i).gameObject.name);
                if (GameObject.Find("puzzleArea").transform.GetChild(i).childCount < 2)
                    GameObject.Find("puzzleArea").transform.GetChild(i).gameObject.GetComponent<slotDropHandler>().dropManuel();
                else
                    returnPosition = true;

            }
            else
                returnPosition = true;
        }

        if (returnPosition)
        {
            transform.parent.gameObject.transform.localScale = new Vector3(1f, 1f);
            transform.parent.gameObject.transform.position = transformCupeObjects;
        }

        Debug.Log("Giriş Sayısı    =   --- - - - - - - " + girisSayisi);
        Debug.Log("Tutulan Nesnedeki Cupe Sayısı    =   --- - - - - - - " + cupeCount);
        cupeCount = 0;
    }

    void OnMouseDown()
    {
        returnPosition = false;
        transformCupeObjects = transform.parent.gameObject.transform.position;

        // CUPE COUNT CALCULATE
        for (int i = 0; i < transform.parent.gameObject.transform.childCount; i++)
        {
            cupeCount++;
        }
    }

    void OnMouseDrag()
    {
        transform.parent.gameObject.transform.localScale = new Vector3(1.4f, 1.4f);
        transform.parent.gameObject.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
