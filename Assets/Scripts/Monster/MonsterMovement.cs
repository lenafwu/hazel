using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    public Transform playerTransform;
    public bool isChasing;
    public float chaseDistance;
    public EnemyDamage enemyDamage;
    // Start is called before the first frame update
    void Start()
    {
        


    }

    // Update is called once per frame
    void Update()
    {
        if (!enemyDamage.isHit)
        { 
            if (isChasing)
            {
                GetComponent<Moving>().enabled = false;
                if (transform.position.x < playerTransform.position.x)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                    transform.position += Vector3.right * GetComponent<Moving>().speed * 1.5f * Time.deltaTime;
                }
                else
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                    transform.position += Vector3.left * GetComponent<Moving>().speed * 1.5f * Time.deltaTime;
                }
            }else{
                if(Vector2.Distance(transform.position, playerTransform.position) < chaseDistance)
                {
                    isChasing = true;
                }
            }

            if(Vector2.Distance(transform.position, playerTransform.position) > chaseDistance)
            {
                isChasing = false;
                GetComponent<Moving>().enabled = true;
            }
        }
    }
}
