using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyView : MonoBehaviour
{
    [SerializeField] GameObject enemy, show;

    //bool backStab;
    [SerializeField] float viewRange, viewAngle;
    float dotDirection;
    MovementPlayer movementPlayer;

    private void Start()
    {
        show.SetActive(false);
        movementPlayer = enemy.GetComponent<MovementPlayer>();
    }
    void Update()
    {
        
        if ((enemy.transform.position.x - gameObject.transform.position.x) + (enemy.transform.position.y - gameObject.transform.position.y) + (enemy.transform.position.z - gameObject.transform.position.z) < viewRange && (enemy.transform.position.x - gameObject.transform.position.x) + (enemy.transform.position.y - gameObject.transform.position.y) + (enemy.transform.position.z - gameObject.transform.position.z) > -viewRange)
        {

            dotDirection = Vector3.Dot(enemy.transform.forward, transform.forward);

            if (dotDirection > viewAngle)
            {
                show.SetActive(false);
                movementPlayer.SeeYou(false);
            }
            else
            {
                show.SetActive(true);
                movementPlayer.SeeYou(true);
            }

        }
        else
        {
            show.SetActive(false);
            movementPlayer.SeeYou(true);
        }
    }
}
