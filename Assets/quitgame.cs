using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quitgame : MonoBehaviour
{
    public void QuitGame()
    {
        // 실제 빌드된 게임 종료
        Application.Quit();

        // UNITY_EDITOR이 정의되어 있을 때만 실행됨 (즉, 에디터에서만 실행)
        #if UNITY_EDITOR
            // 에디터에서 Play 모드를 정지시킴
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
