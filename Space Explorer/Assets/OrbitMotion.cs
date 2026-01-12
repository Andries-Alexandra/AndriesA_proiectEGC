using UnityEngine;

public class OrbitMotion : MonoBehaviour
{
    public Transform targetToOrbit; //obiectul in jurul caruia se vor roti planetele

    public float orbitSpeed = 20f; //viteza de revolutie

    public float selfRotationSpeed = 50f; //viteza de rotatie

    void Update()
    {
        //invartirea planetei in jurul axei sale
        transform.Rotate(Vector3.up * selfRotationSpeed * Time.deltaTime);

        if (targetToOrbit != null)
        {
            transform.RotateAround(targetToOrbit.position, Vector3.up, orbitSpeed * Time.deltaTime);
        }
    }
}