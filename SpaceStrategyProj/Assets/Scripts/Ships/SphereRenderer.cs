using UnityEngine;

public class SphereRenderer : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public int segments = 32;
    public float radius = 2f;
    public Transform targetObject;

    void Start()
    {
        CreateSphere();
    }

    void CreateSphere()
    {
        lineRenderer.positionCount = segments * segments;
        lineRenderer.loop = false;
        lineRenderer.useWorldSpace = true;

        Vector3[] points = new Vector3[segments * segments];
        int pointIndex = 0;

        for (int i = 0; i < segments; i++)
        {
            float lat = Mathf.PI * (-0.5f + (float)i / (segments - 1));
            float y = Mathf.Sin(lat);
            float scale = Mathf.Cos(lat);

            for (int j = 0; j < segments; j++)
            {
                float lon = 2 * Mathf.PI * (float)j / segments;
                float x = Mathf.Cos(lon) * scale;
                float z = Mathf.Sin(lon) * scale;

                Vector3 point = new Vector3(x, y, z) * radius;
                if (targetObject != null)
                {
                    point += targetObject.position;
                }
                points[pointIndex++] = point;
            }
        }

        lineRenderer.SetPositions(points);
    }

    void Update()
    {
        if (targetObject != null)
        {
            CreateSphere();
        }
    }
}