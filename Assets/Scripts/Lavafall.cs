using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lavafall : MonoBehaviour
{
    public float speed = 50;

    private float currentscroll;
    private Material _material;

    // Start is called before the first frame update
    void Start()
    {
        _material = GetComponent<SpriteRenderer>().material;
    }

    void Update()
    {
        currentscroll += speed * Time.deltaTime;
        _material.mainTextureOffset = new Vector2(currentscroll, 0);
    }
}
