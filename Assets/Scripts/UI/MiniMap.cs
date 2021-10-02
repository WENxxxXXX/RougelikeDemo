using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMap : MonoBehaviour
{
    RawImage rawImage;

    private void Awake()
    {
        rawImage = GetComponent<RawImage>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            rawImage.enabled = !rawImage.enabled;
        }
    }
}
