using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyView : MonoBehaviour
{
    [SerializeField] GameObject enemy, show;
    [SerializeField] Transform laserSpawnPoint;
    [SerializeField] float viewRange, viewAngle;
    float dotConversion, dotDirection;
    Vector3 eF;



    private void Start()
    {
        show.SetActive(false);
    }
    void Update()
    {
        Ray ray = new Ray(laserSpawnPoint.position, laserSpawnPoint.forward);
        bool cast = Physics.Raycast(ray, out RaycastHit hit, viewRange);
        Vector3 hitPosition = cast ? hit.point : laserSpawnPoint.position + laserSpawnPoint.forward * viewRange;

        if ((enemy.transform.position.x - gameObject.transform.position.x) + (enemy.transform.position.y - gameObject.transform.position.y) + (enemy.transform.position.z - gameObject.transform.position.z) < hitPosition.x && (enemy.transform.position.x - gameObject.transform.position.x) + (enemy.transform.position.y - gameObject.transform.position.y) + (enemy.transform.position.z - gameObject.transform.position.z) > -hitPosition.x)
        {

            dotDirection = Vector3.Dot(enemy.transform.forward, transform.forward);
            eF = transform.position - enemy.transform.position;
            dotConversion = Vector3.Dot(enemy.transform.forward, eF.normalized);

            if (dotConversion > 0.8 && dotDirection > viewAngle || eF.magnitude > hitPosition.x)
            {
                show.SetActive(false);
            }
            else
            {
                show.SetActive(true);
            }

        }
        else
        {
            show.SetActive(false);
        }
    }
}
