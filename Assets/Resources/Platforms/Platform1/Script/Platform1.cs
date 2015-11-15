using UnityEngine;
using System.Collections;

public class Platform1 : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    SpriteRenderer sprite;
    void Awake()
    {
        //var body = transform.FindChild("Body");

        //sprite = body.GetComponent<SpriteRenderer>();

        //Vector2 spriteSize = new Vector2(sprite.bounds.size.x / transform.localScale.x, sprite.bounds.size.y / transform.localScale.y);

        //GameObject childPrefab = new GameObject();

        //SpriteRenderer childSprite = childPrefab.AddComponent<SpriteRenderer>();
        //childPrefab.transform.position = transform.position;
        //childSprite.sprite = sprite.sprite;

        //GameObject child;
        //for (int i = 0, h = (int)Mathf.Round(sprite.bounds.size.y); i * spriteSize.y < h; i++)
        //{
        //    for (int j = 0, w = (int)Mathf.Round(sprite.bounds.size.x); j * spriteSize.x < w; j++)
        //    {
        //        child = Instantiate(childPrefab) as GameObject;
        //        child.transform.position = transform.position - (new Vector3(spriteSize.x * j, spriteSize.y * i, 0));
        //        child.transform.parent = transform;
        //    }
        //}

        //Destroy(childPrefab);
        //sprite.enabled = false;

    }



    void OnDrawGizmos()
    {

        var body = transform.FindChild("Body");

        int childs = transform.childCount;
        for (int i = 0; i < childs; i++)
        {
            GameObject.Destroy(transform.GetChild(i).gameObject);
        }


      




        return;
        sprite = body.GetComponent<SpriteRenderer>();

        Vector2 spriteSize = new Vector2(sprite.bounds.size.x / transform.localScale.x, sprite.bounds.size.y / transform.localScale.y);



        Transform child;
        for (int i = 0, h = (int)Mathf.Round(sprite.bounds.size.y); i * spriteSize.y < h; i++)
        {
            for (int j = 0, w = (int)Mathf.Round(sprite.bounds.size.x); j * spriteSize.x < w; j++)
            {
                child = Instantiate(body) as Transform;
                child.position = transform.position - (new Vector3(spriteSize.x * j, spriteSize.y * i, 0));
                child.parent = transform;
            }
        }

        // Destroy(childPrefab);
        //sprite.enabled = false;


    }
}
