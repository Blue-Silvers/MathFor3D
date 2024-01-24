using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyView : MonoBehaviour
{
    [SerializeField] GameObject enemy, show;
    [SerializeField] Transform laserSpawnPoint;
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
        Ray ray = new Ray(laserSpawnPoint.position, laserSpawnPoint.forward);
        bool cast = Physics.Raycast(ray, out RaycastHit hit, viewRange);
        Vector3 hitPosition = cast ? hit.point : laserSpawnPoint.position + laserSpawnPoint.forward * viewRange;

        if ((enemy.transform.position.x - gameObject.transform.position.x) + (enemy.transform.position.y - gameObject.transform.position.y) + (enemy.transform.position.z - gameObject.transform.position.z) < hitPosition.x && (enemy.transform.position.x - gameObject.transform.position.x) + (enemy.transform.position.y - gameObject.transform.position.y) + (enemy.transform.position.z - gameObject.transform.position.z) > -hitPosition.x)
        {

            dotDirection = Vector3.Dot(enemy.transform.forward, transform.forward);

            if (dotDirection > viewAngle)
            {
                show.SetActive(false);
                movementPlayer.SeeYou(true);
            }
            else
            {
                show.SetActive(true);
                movementPlayer.SeeYou(false);
            }

        }
        else
        {
            show.SetActive(false);
            movementPlayer.SeeYou(true);
        }
    }
}
