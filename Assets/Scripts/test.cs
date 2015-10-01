using UnityEngine;
using System.Collections;

public class test : MonoBehaviour
{
    public Transform bot;
    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < Random.Range(18, 52); i++)
        {
           var rndVector = new Vector3(Random.Range(1, 200), Random.Range(-1.68f, 3), 1);
         //   Instantiate(bot, rndVector,Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {

        GUI.BeginGroup(new Rect(2, 2, 300, 150));
        if (GUI.Button(new Rect(1, 1, 100, 45), "connect"))
        {
            NetworkCore.Connect("10.50.10.29", 800);
        }

        if (GUI.Button(new Rect(110, 1, 100, 45), "test"))
        {
            var t = NetworkCore.ObjectReference[0];
            float time = Time.deltaTime * 1;
            t.gameObject.transform.position = Vector3.Lerp(t.gameObject.transform.position, new Vector3(10, -2, 3), time);
        }
        GUI.EndGroup();
    }
}
