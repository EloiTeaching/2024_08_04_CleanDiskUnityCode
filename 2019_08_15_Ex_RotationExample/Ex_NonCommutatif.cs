using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ex_NonCommutatif : MonoBehaviour
{
    public Vector3 euleurA;
    public Vector3 euleurB;

    public Quaternion rotationA;
    public Quaternion rotationB;

    public Transform affectedA;
    public Transform affectedB;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnValidate()
    {

        rotationA = Quaternion.Euler(euleurA);
        rotationB = Quaternion.Euler(euleurB);
        affectedA.localRotation = rotationA * rotationB;
        affectedB.localRotation = rotationB * rotationA;
    }
}
