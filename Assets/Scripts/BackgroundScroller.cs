using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{

    [SerializeField] float scrollSpeed = 0.3f;

    Material material;
    Vector2 ofsett;
    // Use this for initialization
    void Start()
    {
        material = GetComponent<Renderer>().material;
        ofsett = new Vector2(0, scrollSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        material.mainTextureOffset += ofsett * Time.deltaTime;
    }
}
