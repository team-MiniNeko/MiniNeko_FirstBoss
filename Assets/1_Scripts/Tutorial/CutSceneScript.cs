using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SkipTutorialUi : MonoBehaviour
{
    public int Level;
    public TutorialManager tutomana;
    public bool hasCutScene = true;
    public float ScaleFactor = 2f;
    public Transform CamPos;
    public bool useInf = false;
    Transform mainCam;
    bool isCutScene = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.gameObject.CompareTag("Player"))
            return;
        Debug.Log($"{name}: triggered");
        

        tutomana.SetUiLevel(Level);

        if (hasCutScene)
        {
            // mainCam 관련 코드는 파괴/비활성화 전에 실행해야 합니다.
            mainCam = Camera.allCameras[0].transform;
            mainCam.gameObject.GetComponent<CameraScripts>().Target = CamPos;
            mainCam.GetComponent<CameraScripts>().originSize = 14 * ScaleFactor;
        }
        if(!useInf)
            gameObject.SetActive(false);
    }
    void Update(){
    }
}
