using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[System.Serializable]
public class TrailSegment
{
    public Vector3 position;
    public Quaternion rotation;
    public float width;
    private float currentLife;
    public QuikTrailRenderer trail;
    public float vCoord;

    public float CurrentLife { get => currentLife; }

    public TrailSegment(Vector3 position, QuikTrailRenderer trail, Quaternion rotation, float vCoord)
    {
        this.vCoord = vCoord;
        this.position = position;
        this.trail = trail;
        this.rotation = rotation;
        currentLife = 1f;
    }

    public void Update()
    {
        currentLife -= Time.deltaTime / trail.time;
        if (currentLife <= 0)
        {
            trail.Segments.Remove(this);
        }
    }
}

[ExecuteAlways]
public class QuikTrailRenderer : MonoBehaviour
{
    public Material material;

    public float time = 1f;
    public Gradient color;
    public Color colorMultiplier = Color.white;
    public AnimationCurve curve = new AnimationCurve(new Keyframe[] { new Keyframe(0, 1), new Keyframe(1, 1) });
    public float width = 0.5f;
    public float minVertexDistance = 0.2f;
    public float maxVertexDistance = 3f;
    public LineAlignment alignment;
    public bool castShadows = false;
    public bool receiveShadows = false;

    public bool renderTrail = true;

    [Header("Segments")]
    private List<TrailSegment> segments = new List<TrailSegment>();
    public List<TrailSegment> Segments { get => segments; }

    [Header("Debug")]
    public bool drawGizmos = false;
    public Color debugCol;
    public float debugRadius;

    private int[] triangles;
    int trianglesSet = 0;

    private Vector3[] vertices;
    int verticesSet = 0;

    private Vector2[] uvs;
    int uvsSet = 0;

    private Color[] colors;
    int colorsSet = 0;

    private Mesh mesh;
    private int totalCount = 0;
    private float halfWidth => width / 2;

    private void OnDrawGizmos()
    {
        if (!drawGizmos) return;
        Gizmos.color = debugCol;
        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], debugRadius);
        }
    }


    private void Update()
    {
        if (!(segments.Count >= 2))
        {
            segments.Clear();
            AddSegmentAtPosition();
            AddSegmentAtPosition();
        }

        if (Vector3.Distance(segments[segments.Count - 1].position, transform.position) > minVertexDistance)
        {
            AddSegmentAtPosition();
        }

        CreateMesh();
        DrawMesh();
    }

    private void OnDestroy()
    {
        //RenderPipelineManager.beginCameraRendering -= DrawMesh;
    }

    private void OnEnable()
    {
        ClearAll();
    }

    private void DrawMesh()
    {
        if (this == null) return;
        if (!gameObject.activeInHierarchy) return;
        if (!renderTrail) return;

        Graphics.DrawMesh(mesh, Matrix4x4.TRS(Vector3.zero, Quaternion.identity, transform.localScale), material, 0, null, 0, null, castShadows, receiveShadows);
    }

    private Camera gameCamera;
    private void CreateMesh()
    {
        if (!gameObject.activeInHierarchy) return;

        // to avoid meshes leaking in memory
        if (mesh != null)
        {
            if (!Application.isEditor) Destroy(mesh);
            else DestroyImmediate(mesh);
        }

        mesh = new Mesh();

        vertices = new Vector3[segments.Count * 2];
        triangles = new int[(segments.Count - 1) * 6];
        uvs = new Vector2[vertices.Length];
        colors = new Color[vertices.Length];

        verticesSet = 0;
        trianglesSet = 0;
        uvsSet = 0;
        colorsSet = 0;

        for (int i = 0; i < segments.Count; i++)
        {
            if (curve == null) return;
            TrailSegment s = segments[i];
            float currentWidth = curve.Evaluate(1 - s.CurrentLife);


            Quaternion q = Quaternion.identity;
            if (!gameCamera)
            {
                gameCamera = Camera.main;
            }

            Vector3 cameraDirection = Vector3.forward;

            if (gameCamera)
            {
                cameraDirection = (gameCamera.transform.position - s.position).normalized;
                //q =  Quaternion.LookRotation(dir, Vector3.up);
                //Debug.DrawLine(s.position , s.position + dir * 50f, Color.red);
            }
            Vector3 segmentDirection = Vector3.forward;
            if (i == segments.Count - 1)
                segmentDirection = (s.position - segments[i - 1].position).normalized;
            else
                segmentDirection = (segments[i + 1].position - s.position).normalized;

            Vector3 cross = Vector3.Cross(cameraDirection, segmentDirection);

            Vector3 rightVertex = s.position + cross * halfWidth * currentWidth;
            Vector3 leftVertex = s.position - cross * halfWidth * currentWidth;

            AddVertex(rightVertex);
            AddVertex(leftVertex);

            segments[i].Update();
        }

        for (int i = 0; i < segments.Count - 1; i++)
        {
            int b = i * 2;
            AddTriangle(b);
            AddTriangle(b + 1);
            AddTriangle(b + 2);
            AddTriangle(b + 3);
            AddTriangle(b + 2);
            AddTriangle(b + 1);
        }

        for (int i = 0; i < vertices.Length; i++)
        {
            int sIndex = Mathf.FloorToInt(i / 2);
            if (sIndex > segments.Count - 1) break;
            TrailSegment segment = segments[sIndex];
            float u = i % 2 == 0 ? 0 : 1;
            int previous = sIndex == 0 ? 0 : sIndex - 1;

            float evaluate = 0f;
            if (segments.Count > 1)
                evaluate = Mathf.Clamp01(1 - ((float)sIndex / (float)(segments.Count - 1)));

            if (color != null)
                AddColor(color.Evaluate(evaluate) * colorMultiplier);

            AddUV(new Vector2(u, segment.vCoord));
        }


        mesh.vertices = vertices;
        mesh.colors = colors;
        mesh.uv = uvs;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }

    void AddVertex(Vector3 vertex)
    {
        vertices[verticesSet] = vertex;

        verticesSet++;
    }
    void AddTriangle(int triangle)
    {
        triangles[trianglesSet] = triangle;

        trianglesSet++;
    }
    void AddUV(Vector2 uv)
    {
        uvs[uvsSet] = uv;

        uvsSet++;
    }
    void AddColor(Color color)
    {
        colors[colorsSet] = color;

        colorsSet++;
    }

    public void ClearAll()
    {
        segments.Clear();
    }

    private void AddSegmentAtPosition()
    {
        Quaternion q = Quaternion.identity;
        if (segments.Count >= 1)
        {
            Vector3 dir = (transform.position - LastSegment.position).normalized;
            dir = new Vector3(Mathf.Abs(dir.x), Mathf.Abs(dir.y), Mathf.Abs(dir.z));
            if (dir.magnitude > 0)
                q = Quaternion.LookRotation(dir, Vector3.up);
            else
                q = Quaternion.identity;
        }

        Vector3 pos = LastSegment == null ? transform.position : LastSegment.position;
        float init = LastSegment == null ? 0 : LastSegment.vCoord;
        float v = init + Vector3.Distance(transform.position, pos);

        segments.Add(new TrailSegment(transform.position, this, q, v));
        totalCount++;
    }

    private TrailSegment LastSegment => segments.Count > 0 ? segments[segments.Count - 1] : null;

}