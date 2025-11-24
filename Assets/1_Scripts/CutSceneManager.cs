using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CutSceneStatus{
    //------필드------//
    public static bool isInCutScene = false; //컷신중인가?
    
    //-----메서드-----//
    public static void StartCutScene()  {CutSceneStatus.isInCutScene = true;}
    //컷씬을 시작합니다.
    public static void EndCutScene()    {CutSceneStatus.isInCutScene = false;}

    //컷씬을 종료합니다.
    public static bool CheckStatus()    {return isInCutScene;}
    //컷신의 상황을 반환합니다.
}
public class CutSceneManager : MonoBehaviour
{
    
}
