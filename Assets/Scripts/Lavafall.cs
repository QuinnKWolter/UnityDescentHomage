using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lavafall : MonoBehaviour
{
    // Scroll main texture based on time

    float scrollSpeed = 0.5f;
    MeshRenderer rend;

    void Start()
    {
        rend = GetComponent<MeshRenderer> ();
    }

    void Update()
    {
        float offset = Time.time * scrollSpeed;
        rend.material.SetTextureOffset("_MainTex", new Vector2(0, -offset));
    }
}
