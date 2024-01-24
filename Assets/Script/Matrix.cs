using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matrix : MonoBehaviour
{
    [SerializeField] float[] identity = new float[9];
    [SerializeField] Vector3 vectorMultiplicator;
    [SerializeField] float[] anotherMatrix = new float[3];


    private void Start()
    {
        print(identity[0] + "," + identity[1] + "," + identity[2]);
        identity[0] = 1; identity[1] = 0; identity[2] = 0;
        print(identity[3] + "," + identity[4] + "," + identity[5]);
        identity[3] = 0; identity[4] = 1; identity[5] = 0;
        print(identity[6] + "," + identity[7] + "," + identity[8]);
        identity[6] = 0; identity[7] = 0; identity[8] = 1;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            print(identity[0] + "," + identity[1] + "," + identity[2]);
            print(identity[3] + "," + identity[4] + "," + identity[5]);
            print(identity[6] + "," + identity[7] + "," + identity[8]);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            print(identity[0] * vectorMultiplicator.x + identity[1] * vectorMultiplicator.y + identity[2] * vectorMultiplicator.z);
            print(identity[3] * vectorMultiplicator.x + identity[4] * vectorMultiplicator.y + identity[5] * vectorMultiplicator.z);
            print(identity[6] * vectorMultiplicator.x + identity[7] * vectorMultiplicator.y + identity[8] * vectorMultiplicator.z);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            print(identity[0] + "," + identity[1] + "," + identity[2]);
            print(identity[3] + "," + identity[4] + "," + identity[5]);
            print(identity[6] + "," + identity[7] + "," + identity[8]);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            print(identity[0] + "," + identity[3] + "," + identity[6]);
            print(identity[1] + "," + identity[4] + "," + identity[7]);
            print(identity[2] + "," + identity[5] + "," + identity[8]);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {

        }
    }
}
