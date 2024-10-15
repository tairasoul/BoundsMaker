using UnityEngine;

namespace BoundsMaker
{
    internal class VisualiserComponent : MonoBehaviour
    {
        internal Bounds bounds;
        private void Update()
        {
            VisualizeBound(bounds);
        }

        private void VisualizeBound(Bounds bounds)
        {
            LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();
            if (!lineRenderer)
                lineRenderer = gameObject.AddComponent<LineRenderer>();

            Vector3 center = bounds.center;
            Vector3 size = bounds.size;

            Vector3[] corners =
            [
                center + new Vector3(size.x, size.y, size.z) * 0.5f,
                center + new Vector3(size.x, size.y, -size.z) * 0.5f,
                center + new Vector3(-size.x, size.y, -size.z) * 0.5f,
                center + new Vector3(-size.x, size.y, size.z) * 0.5f,
                center + new Vector3(size.x, -size.y, size.z) * 0.5f,
                center + new Vector3(size.x, -size.y, -size.z) * 0.5f,
                center + new Vector3(-size.x, -size.y, -size.z) * 0.5f,
                center + new Vector3(-size.x, -size.y, size.z) * 0.5f
            ];

            int[] indices =
            [
                0, 1, 1, 2, 2, 3, 3, 0,
                4, 5, 5, 6, 6, 7, 7, 4,
                0, 4, 1, 5, 2, 6, 3, 7 
            ];

            lineRenderer.positionCount = indices.Length;

            for (int i = 0; i < indices.Length; i++)
            {
                lineRenderer.SetPosition(i, corners[indices[i]]);
            }
        }
    }
}