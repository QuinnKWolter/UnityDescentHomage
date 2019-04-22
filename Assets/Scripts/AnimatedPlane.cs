using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedPlane : MonoBehaviour
{
    public Texture[] textures;
    public float changeInterval = 0.1F;
    private MeshRenderer rend;

    void Start()
    {
        rend = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        if (textures.Length == 0)
            return;

        int index = Mathf.FloorToInt(Time.time / changeInterval);
        index = index % textures.Length;
        rend.material.mainTexture = textures[index];
    }
}
