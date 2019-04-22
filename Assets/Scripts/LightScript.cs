using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    public Sprite brokenSprite;
    public GameObject light;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Break(){
        Debug.Log("BREAKING LIGHT");
        GetComponent<AudioSource>().Play();
        GetComponent<SpriteRenderer>().sprite = brokenSprite;
        Destroy(light);
    }
}
