using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Target;
    public GameObject BlastEffect;
    public bool track = true;
    void OnTriggerEnter2D(Collider2D collision)
    {   
        Debug.Log("Missile: " + collision.name);
        if (collision.CompareTag("Structer") || collision.CompareTag("Player")){
            GameObject newM = Instantiate(BlastEffect);
            newM.transform.position = transform.position;
            Destroy(gameObject, 0.01f);
            Destroy(newM,0.5f);
        }
    }
    float Startlife;
    float life;

    
    void Start()
    {
        Startlife = Time.time;
        life = 0f;
        Target = GameObject.FindWithTag("Player");
        transform.rotation = Quaternion.Euler(0, 0, 90f);
        
    }
    void Update()
    {
        transform.position = new Vector3(transform.position.x,transform.position.y,0);
        life = Time.time - Startlife;
        if (life > 30)
            Destroy(gameObject);
        if (Target != null &&life > 0.5 && life < 1 && track){
            Vector2 directionToTarget = new Vector2(
                Target.transform.position.x - transform.position.x,
                Target.transform.position.y - transform.position.y
            );
            float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, 0, angle - 90f);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                15f * Time.deltaTime
            ); 
        }

        // 4. 전진 이동 (오브젝트의 현재 '앞' 방향으로 이동)
        float speed = 8f;
        // transform.up은 현재 회전된 오브젝트의 로컬 Y축(위쪽), 즉 '앞' 방향입니다.
        transform.Translate(transform.up * speed * Time.deltaTime, Space.World);
    }
}
