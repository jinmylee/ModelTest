using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastWave : MonoBehaviour
{
    public int pointCnt;
    public float maxRadious;
    public float speed;
    public float startwidth;
    public float force;


    private LineRenderer lineRenderer;
    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = pointCnt + 1;
    }

    private IEnumerator Balst()
    {
        float currentRadious = 0f;
        while(currentRadious <maxRadious)
        {
            currentRadious += Time.deltaTime * speed;
            Draw(currentRadious);
            Damage(currentRadious);
            yield return null;
        }

        
    }

    private void Damage(float currentRadious)
    {
        Collider[] hittingobj = Physics.OverlapSphere(transform.position, currentRadious);

        for (int i = 0; i < hittingobj.Length; i++)
        {
            Rigidbody rb = hittingobj[i].GetComponent<Rigidbody>();

            if (!rb) continue;
            Vector3 direction = (hittingobj[i].transform.position - transform.position).normalized;
            rb.AddForce(direction*force, ForceMode.Impulse);
            
        }


    }

     private void Draw(float currentRadious)
    {
        float angleBtweenPiont = 360f / pointCnt;

        for (int i = 0; i <=pointCnt; i++)
        {
            float angle = i * angleBtweenPiont * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(Mathf.Sin(angle),Mathf.Cos(angle), 0f);
            Vector3 Position = direction * currentRadious;

            lineRenderer.SetPosition(i, Position);
        }

        lineRenderer.widthMultiplier = Mathf.Lerp(0f, startwidth, 1f - currentRadious / maxRadious);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(Balst());
        }
    }
}
