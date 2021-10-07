using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float height = Camera.main.orthographicSize * 2f;
        float width = height * Screen.width / Screen.height;

        if (gameObject.name == "Background")
        {
            transform.localScale = new Vector3(width, height, 1f);
        }
        else if (gameObject.name == "Ground")
        {
            transform.localScale = new Vector3(width + 3f, 5, 1f);
        }
    }

}
