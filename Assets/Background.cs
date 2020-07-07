using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Background : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetupBackground();
    }

    private void SetupBackground()
    {
      
        this.transform.parent = Camera.main.transform;
        float screenRation = ((float)Screen.width / (float)Screen.height) ;
        Vector3 scale = new Vector3(Camera.main.orthographicSize *2* screenRation, Camera.main.orthographicSize * 2, 100f);
        this.transform.localScale = scale;
    }
}
