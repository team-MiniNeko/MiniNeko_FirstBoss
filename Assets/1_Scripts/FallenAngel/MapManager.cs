using DG.Tweening;
using System.Collections;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance;
    public FallenAngelAttack fallenAngel;
    public GameObject[] Block;
    private bool isThree = true;
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        switch (fallenAngel.phase)
        {
            case 1:
                PhaseOne();
                break;
            case 2:
                PhaseTwo();
                break;
            case 3:
                if (isThree)
                {
                    PhaseThree();
                    isThree = false;
                }
                break;
            default:
                Debug.LogError("저런 에러가 걸리셨군요.");
                break;
        }
    }
    void PhaseOne()
    {

    }
    void PhaseTwo()
    {

    }
    void PhaseThree()
    {
        Sequence seq = DOTween.Sequence();
        seq.AppendCallback(() =>
        {
            Block[0].transform.position = new Vector3(2.5f, 3.5f, 0);
            Block[1].transform.position = new Vector3(-11.75f, -6.85f, 0);
            Block[2].transform.position = new Vector3(-8f, -23.6f, 0);
            Block[3].transform.position = new Vector3(13f, -23.6f, 0);
            Block[4].transform.position = new Vector3(16.76f, -6.86f, 0);
        });
        Vector3 first = new Vector3(2.5f, 3.5f, 0);
        Vector3 second = new Vector3(-11.75f, -6.85f, 0);
        Vector3 third = new Vector3(-6.3f, -23.6f, 0);
        Vector3 fourth = new Vector3(11.3f, -23.6f, 0);
        Vector3 fifth = new Vector3(16.76f, -6.86f, 0);
        Vector3[] pathOne = new Vector3[]
        {
            first,
            new Vector3(-9.2f, -2.5f, 0),   // 곡선을 만들기 위한 중간 지점 (꺾이는 곳)
            second
        };
        Vector3[] pathTwo = new Vector3[]
        {
            second,
            new Vector3(-9.2f, -20.5f, 0),   // 곡선을 만들기 위한 중간 지점 (꺾이는 곳)
            third
        };
        Vector3[] pathThree = new Vector3[]
        {
            third,
            new Vector3(2.5f, -26.5f, 0),   // 곡선을 만들기 위한 중간 지점 (꺾이는 곳)
            fourth
        };
        Vector3[] pathFour = new Vector3[]
        {
            fourth,
            new Vector3(14.2f, -20.5f, 0),   // 곡선을 만들기 위한 중간 지점 (꺾이는 곳)
            fifth
        };
        Vector3[] pathFive = new Vector3[]
        {
            fifth,
            new Vector3(14.2f, -2.5f, 0),
            first
        };

        // --- Step 1 ---
        seq.Append(
            DOTween.Sequence()
                .Join(Block[0].transform.DOPath(pathOne, 1f, PathType.CatmullRom))
                .Join(Block[1].transform.DOPath(pathTwo, 1f, PathType.CatmullRom))
                .Join(Block[2].transform.DOPath(pathThree, 1f, PathType.CatmullRom))
                .Join(Block[3].transform.DOPath(pathFour, 1f, PathType.CatmullRom))
                .Join(Block[4].transform.DOPath(pathFive, 1f, PathType.CatmullRom))
        );
        seq.AppendInterval(3f);

        // --- Step 2 ---
        seq.Append(
            DOTween.Sequence()
                .Join(Block[4].transform.DOPath(pathOne, 1f, PathType.CatmullRom))
                .Join(Block[0].transform.DOPath(pathTwo, 1f, PathType.CatmullRom))
                .Join(Block[1].transform.DOPath(pathThree, 1f, PathType.CatmullRom))
                .Join(Block[2].transform.DOPath(pathFour, 1f, PathType.CatmullRom))
                .Join(Block[3].transform.DOPath(pathFive, 1f, PathType.CatmullRom))
        );
        seq.AppendInterval(3f);

        // --- Step 3 ---
        seq.Append(
            DOTween.Sequence()
                .Join(Block[3].transform.DOPath(pathOne, 1f, PathType.CatmullRom))
                .Join(Block[4].transform.DOPath(pathTwo, 1f, PathType.CatmullRom))
                .Join(Block[0].transform.DOPath(pathThree, 1f, PathType.CatmullRom))
                .Join(Block[1].transform.DOPath(pathFour, 1f, PathType.CatmullRom))
                .Join(Block[2].transform.DOPath(pathFive, 1f, PathType.CatmullRom))
        );
        seq.AppendInterval(3f);

        // --- Step 4 ---
        seq.Append(
            DOTween.Sequence()
                .Join(Block[2].transform.DOPath(pathOne, 1f, PathType.CatmullRom))
                .Join(Block[3].transform.DOPath(pathTwo, 1f, PathType.CatmullRom))
                .Join(Block[4].transform.DOPath(pathThree, 1f, PathType.CatmullRom))
                .Join(Block[0].transform.DOPath(pathFour, 1f, PathType.CatmullRom))
                .Join(Block[1].transform.DOPath(pathFive, 1f, PathType.CatmullRom))
        );

        // --- Step 5 ---
        seq.Append(
            DOTween.Sequence()
                .Join(Block[1].transform.DOPath(pathOne, 1f, PathType.CatmullRom))
                .Join(Block[2].transform.DOPath(pathTwo, 1f, PathType.CatmullRom))
                .Join(Block[3].transform.DOPath(pathThree, 1f, PathType.CatmullRom))
                .Join(Block[4].transform.DOPath(pathFour, 1f, PathType.CatmullRom))
                .Join(Block[0].transform.DOPath(pathFive, 1f, PathType.CatmullRom))
        );
        seq.AppendInterval(3f);

        seq.SetLoops(-1, LoopType.Restart);   // 무한 반복!
    }
}