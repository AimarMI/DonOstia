using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{

    public Button restart;

    GameObject char1GO;
    Char1 char1;

    GameObject char2GO;
    Char2 char2;

    // Start is called before the first frame update
    void Start()
    {
        char1GO = GameObject.Find("Char1");
        char1 = char1GO.GetComponent<Char1>();

        char2GO = GameObject.Find("Char2");
        char2 = char2GO.GetComponent<Char2>();
    }

    // Update is called once per frame
    void Update()
    {
        Draw();
    }

    void Draw() {

        if (char1.getCurrentHealth() <= 0 || char2.getCurrentHealth() <= 0)
        {
            restart.gameObject.SetActive(true);

            char1.enabled = false;
            char2.enabled = false;
        }
        else 
            restart.gameObject.SetActive(false);
        
    }
}
