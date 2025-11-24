using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTreeManager : MonoBehaviour
{
    public static SkillTreeManager Instance;
    [SerializeField]
    public RectTransform contentRoot;
    public GameObject nodePrefab;
    public GameObject connectionPrefab;
    public SkillData[] allSkills;
    public SkillData rootSkill;

    private Dictionary<SkillData, SkillNodeUI> nodes = new Dictionary<SkillData, SkillNodeUI>();
    private List<GameObject> connections = new List<GameObject>();

    public List<SkillData> nextSkills = new List<SkillData>();

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
    private void Start()
    {
        BuildTreeRecursive(rootSkill, Vector2.zero, 250f, 0);
    }
    void BuildTreeRecursive(SkillData current, Vector2 position, float radius, float angleOffset, int depth = 0, Vector2? fromDir = null)
    {
        // 현재 노드 생성
        SkillNodeUI ui = CreateNode(current, position);

        if (current.nextSkills == null || current.nextSkills.Count == 0)
            return;
        if (depth == 0)
        {
            float angleStep = 360f / current.nextSkills.Count;
            for (int i = 0; i < current.nextSkills.Count; i++)
            {
                SkillData child = current.nextSkills[i];
                float angle = (angleStep * i + angleOffset) * Mathf.Deg2Rad;
                Vector2 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
                Vector2 childPos = position + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;

                SkillNodeUI childUI = CreateNode(child, childPos);
                CreateConnecton(ui.GetComponent<RectTransform>(), childUI.GetComponent<RectTransform>());

                BuildTreeRecursive(child, childPos, radius * 0.75f, angleOffset + (angleStep * i), depth + 1, dir);
            }
        }
        else
        {
            Vector2[] directions = new Vector2[]
            {
                new Vector2 (1, 0),
                new Vector2 (1, 1),
                new Vector2 (0, 1),
                new Vector2 (-1, 1),
                new Vector2 (-1, 0),
                new Vector2 (-1, -1),
                new Vector2 (0, -1),
                new Vector2 (1, -1)
            };

            int startIndex = 0;
            if (fromDir.HasValue)
            {
                float bestDot = -999;
                for (int i = 0; i < directions.Length; i++)
                {
                    float dot = Vector2.Dot(fromDir.Value, directions[i]);
                    if (bestDot < dot)
                    {
                        bestDot = dot;
                        startIndex = i;
                    }
                }
                for (int i = 0; i < current.nextSkills.Count; i++)
                {
                    SkillData child = current.nextSkills[i];
                    int dirIndex = (startIndex + i) % directions.Length;
                    Vector2 dir = directions[dirIndex];

                    Vector2 childPos = position + dir * radius;

                    SkillNodeUI childUI = CreateNode(child, childPos);
                    CreateConnecton(ui.GetComponent<RectTransform>(), childUI.GetComponent<RectTransform>());

                    BuildTreeRecursive(child, childPos, radius * 0.75f, angleOffset, depth + 1, dir);
                }

            }
        }
    }
    SkillNodeUI CreateNode(SkillData data, Vector2 position)
    {
        if (data == null || nodes.ContainsKey(data)) return nodes[data];

        GameObject g = Instantiate(nodePrefab, contentRoot);
        RectTransform rt = g.GetComponent<RectTransform>();
        rt.anchoredPosition = position;

        SkillNodeUI ui = g.GetComponent<SkillNodeUI>();
        ui.skillData = data;
        ui.SetRank(0);
        ui.onClicked += OnNodeClicked;

        nodes[data] = ui;
        return ui;
    }

    void CreateConnecton(RectTransform a, RectTransform b)
    {
        GameObject conn = Instantiate(connectionPrefab, contentRoot);
        RectTransform r = conn.GetComponent<RectTransform>();

        conn.transform.SetAsFirstSibling();

        Vector2 posA = a.anchoredPosition;
        Vector2 posB = b.anchoredPosition;

        Vector2 dir = posB - posA;
        float dist = dir.magnitude;
        r.sizeDelta = new Vector2(dist, r.sizeDelta.y);
        r.anchoredPosition = posA + dir * 0.5f;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        r.localRotation = Quaternion.Euler(0, 0, angle);

        connections.Add(conn);
    }
    void OnNodeClicked(SkillNodeUI node)
    {
        bool canUnlock = true;
        foreach (var pre in node.skillData.preRequisites)
        {
            if (pre == null)
            {
                Debug.LogError($"[ERROR] 스킬 데이터 '{node.skillData.skillName}'의 선행 스킬 배열에 NULL 참조가 있습니다. 에디터에서 확인하세요!!", node.skillData);
                canUnlock = false;
                break;
            }
            if (!nodes.ContainsKey(pre) || nodes[pre].currentRank <= 0)
            {
                canUnlock = false;
                break;
            }
        }
        if (canUnlock && node.currentRank < node.skillData.maxRank)
        {
            node.SetRank(node.currentRank + 1);
            Debug.Log($"스킬 획득: {node.skillData.skillName}");
        }
        else
        {
            Debug.Log("선행 조건 부족 또는 이미 최대 레벨입니다.");
        }
    }
    public void SaveProgress()
    {
        foreach (var kv in nodes)
        {
            string key = "skill" + kv.Key.name;
            PlayerPrefs.SetInt(key, kv.Value.currentRank);
            PlayerPrefs.Save();
        }
    }
    public void LoadProgress()
    {
        foreach (var kv in nodes)
        {
            string key = "skill" + kv.Key.name;
            if (PlayerPrefs.HasKey(key))
                kv.Value.SetRank(PlayerPrefs.GetInt(key));
        }
    }
}