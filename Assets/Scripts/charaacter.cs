using UnityEngine;
using System.Collections;

public class charaacter : MonoBehaviour
{

    // Use this for initialization
    Collider2D col;
    void Start()
    {
        col = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
          

           transform.Translate(new Vector2(0, -2));
        }
   

    }


   
}