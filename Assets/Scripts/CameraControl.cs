using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{  
    public void SetCamera()
    {
        if ((float)Screen.width / (float)Screen.height <= 0.5f)
        {
            Camera.main.orthographicSize = 6f;
        }
    }
}
