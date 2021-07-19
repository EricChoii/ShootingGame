using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private float cubeSize = .3f;
    private int cubesInRow = 3;

    float cubesPivotDistance;
    Vector3 cubesPivot;

    private float explosionRadius = 50f;
    private float explosionUpward = 90f;
    private float explosionForce = 10f;

    private const int FRAGMENT = 10;

    // Use this for initialization
    void Start()
    {
        cubesPivotDistance = cubeSize * cubesInRow / 2;

        cubesPivot = new Vector3(cubesPivotDistance, cubesPivotDistance, cubesPivotDistance);
    }

    public void explode(GameObject _gameObject)
    {
        _gameObject.SetActive(false);

        for (int x = 0; x < cubesInRow; x++)
        {
            for (int y = 0; y < cubesInRow; y++)
            {
                for (int z = 0; z < cubesInRow; z++)
                {
                    createPiece(x, y, z);
                }
            }
        }

        Vector3 explosionPos = transform.position;

        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);

        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, explosionUpward);
            }
        }
    }

    void createPiece(int x, int y, int z)
    {
        GameObject piece;
        Transform _parent = GameObject.Find("TobeRemoved").GetComponent<Transform>();
        piece = GameObject.CreatePrimitive(PrimitiveType.Cube);
        piece.transform.parent = _parent;

        piece.transform.position = transform.position + new Vector3(cubeSize * x, cubeSize * y, cubeSize * z) - cubesPivot;
        piece.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);

        piece.AddComponent<Rigidbody>();
        piece.GetComponent<Rigidbody>().mass = cubeSize;
        piece.layer = FRAGMENT;
        piece.GetComponent<MeshRenderer>().material.color = transform.GetComponent<MeshRenderer>().material.color;

        Destroy(piece, 4f);
    }
}
