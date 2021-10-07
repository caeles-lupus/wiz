using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGLoop : MonoBehaviour
{
    public float speed = 0.1f;
    private Vector2 offset = Vector2.zero;
    private Material material;


    // Start is called before the first frame update
    void Start()
    {
        var gg = GetComponent<Renderer>();
        material = gg.material;
        offset = material.GetTextureOffset("_MainTex");
    }

    // Update is called once per frame
    void Update()
    {
        offset.x += speed * Time.deltaTime;
        material.SetTextureOffset("_MainTex", offset);
    }
}
