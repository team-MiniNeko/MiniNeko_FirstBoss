using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheFinale : MonoBehaviour
{
    public GameObject BigDrone;
    public GameObject MiniDrone;
    public GameObject EnemyFolder;
    public GameObject w1;
    public GameObject w2;
    public GameObject w3;
    public GameObject w4;
    int EmptyCount;
    bool issummoning = false;
    float[] lists = {-400f, 400f};
    float bft;
    // Start is called before the first frame update
    void Start()
    {
        EmptyCount = EnemyFolder.GetComponentsInChildren<Transform>().Length;
        bft = Time.time;
    }
    IEnumerator SummonWaves()
    {   
        issummoning = true;
        for(int i = 0; i <= 300; i++){
            transform.position = new Vector3(transform.position.x,
                                            transform.position.y-0.1f,
                                            transform.position.z);
            yield return new WaitForSeconds(0.001f);
        }
        yield return new WaitForSeconds(5f);
        for(int i = 0; i <= 300; i++){
            transform.position = new Vector3(transform.position.x,
                                            transform.position.y+0.1f,
                                            transform.position.z);
            yield return new WaitForSeconds(0.001f);
        }
        for(int i = 0; i < 6; i++){
            GameObject newEnemy  = Instantiate(BigDrone);
            newEnemy.transform.position = new Vector3(lists[Random.Range(0,2)],0f,0f);
            newEnemy.transform.SetParent(EnemyFolder.transform);
            yield return new WaitForSeconds(0.3f);
        }
        for(int i = 0; i < 4; i++){
            GameObject newEnemy  = Instantiate(MiniDrone);
            newEnemy.transform.position = new Vector3(lists[Random.Range(0,2)],0f,0f);
            newEnemy.transform.SetParent(EnemyFolder.transform);
            yield return new WaitForSeconds(0.3f);
        }
        issummoning = false;
    }
    // Update is called once per frame
    bool isdown = false;
    void Update()
    {
        if(w1.activeSelf == false&&w2.activeSelf == false&&w3.activeSelf == false&&w4.activeSelf == false)
        {   
            GetComponent<BoxCollider2D>().enabled = true;
            transform.position = new Vector3(transform.position.x,
                                            transform.position.y+(42.9f - transform.position.y)*Time.deltaTime,
                                            transform.position.z);
            if(Time.time - bft >= 3f)
            {   
                bft = Time.time;
                GameObject newEnemy  = Instantiate(MiniDrone);
                newEnemy.transform.position = new Vector3(lists[Random.Range(0,2)],0f,0f);
                newEnemy  = Instantiate(BigDrone);
                newEnemy.transform.position = new Vector3(lists[Random.Range(0,2)],0f,0f);
            }
        }else{
            Debug.Log($"{EnemyFolder.GetComponentsInChildren<Transform>().Length} {EmptyCount}");
            if(EnemyFolder.GetComponentsInChildren<Transform>().Length == EmptyCount && !issummoning){
                StartCoroutine(SummonWaves());
            }
            if(Time.time - bft >= 5f)
            {
                bft = Time.time;
                GameObject newEnemy  = Instantiate(MiniDrone);
                newEnemy.transform.position = new Vector3(lists[Random.Range(0,2)],0f,0f);
            }
        }
        
    }
}
