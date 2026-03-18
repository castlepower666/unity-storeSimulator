using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockObject : MonoBehaviour
{
    public StockInfo info;
    public Rigidbody rb;
    public float moveSpeed;
    public bool isPlaced;
    public Collider col;

    void Start()
    {
        info = StockInfoController.Instance.GetInfo(info.name);
    }

    void Update()
    {
        if (isPlaced)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, Vector3.zero, moveSpeed * Time.deltaTime);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.identity, moveSpeed * Time.deltaTime);
        }
    }
    public void Pickup()
    {
        rb.isKinematic = true;
        rb.interpolation = RigidbodyInterpolation.None;

        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        isPlaced = false;
        col.enabled = false;
    }

    public void MakePlaed()
    {
        isPlaced = true;

        col.enabled = false;
    }

    public void Release()
    {
        rb.isKinematic = false;
        rb.interpolation = RigidbodyInterpolation.Interpolate;

        col.enabled = true;
    }

    public void PlaceInBox()
    {
        rb.isKinematic = true;
        rb.interpolation = RigidbodyInterpolation.None;

        col.enabled = false;
    }

}
