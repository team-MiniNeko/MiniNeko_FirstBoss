using UnityEngine;

public class WorldSpaceCanvasResizer : MonoBehaviour
{
    private RectTransform canvasRect;
    private float referenceHeight;
    
    // UI 디자인의 기준 높이 값 (예: 1080p 기준 1080)
    [Tooltip("UI를 디자인한 기준 해상도의 높이입니다.")]
    public float baseCanvasHeight = 1080f; 

    void Awake()
    {
        canvasRect = GetComponent<RectTransform>();
        // Rect Transform의 높이값을 기본값으로 저장
        referenceHeight = baseCanvasHeight; 

        AdjustCanvasWidth();
    }

    void Start()
    {
        // 에디터에서 테스트 시 실시간으로 비율이 바뀌는 것을 반영
        // 빌드된 게임에서는 Start()에서 한 번만 호출해도 됩니다.
        AdjustCanvasWidth(); 
    }

    private void AdjustCanvasWidth()
    {
        // 현재 카메라의 화면 비율 (width / height)을 가져옵니다.
        float currentAspect = Camera.main.aspect; 

        // 비율에 맞춘 새로운 너비 계산:
        // New_Width = Current_Aspect * Reference_Height
        float newWidth = currentAspect * referenceHeight;

        // RectTransform의 너비를 새로운 값으로 설정합니다.
        // SetSizeWithCurrentAnchors는 RectTransform의 크기를 설정하는 안전한 방법입니다.
        canvasRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newWidth);
        
        // (선택 사항) 높이도 baseCanvasHeight로 다시 고정합니다.
        canvasRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, referenceHeight);
    }
}