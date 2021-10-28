using UnityEngine;
using UnityEngine.UI;

public class SampleExample : MonoBehaviour
{
    [SerializeField] Mesh mesh;
    [SerializeField] MeshFilter filter;
    [SerializeField] new MeshRenderer renderer;
    [SerializeField] Vector3[] newVertices;
    [SerializeField] int[] newTriangles;

    void Start()
    {
        // 创建一个正方形面片
        VertexHelper vh = new VertexHelper();
        vh.Clear();

        // 添加顶点
        vh.AddVert(new Vector3(0, 0, 0), Color.red, new Vector2(0, 0));
        vh.AddVert(new Vector3(2, 0, 0), Color.red, new Vector2(1, 0));
        vh.AddVert(new Vector3(2, 2, 0), Color.cyan, new Vector2(1, 1));
        vh.AddVert(new Vector3(0, 2, 0), Color.cyan, new Vector2(0, 1));
        vh.AddVert(new Vector3(1, -1, 0), Color.cyan, new Vector2(0, 1));

        // 设置三角形顺序
        vh.AddTriangle(0, 2, 1);
        vh.AddTriangle(0, 3, 2);
        vh.AddTriangle(0, 1, 4);

        // 将结果展示出来
        //MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        Mesh mesh = new Mesh();
        mesh.name = "Quad";
        vh.FillMesh(mesh);
        filter.mesh = mesh;
        Material material = new Material(Shader.Find("UI/Default"));
        //material.SetColor("_Color", Color.yellow);
        renderer.material = material;
        renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        renderer.receiveShadows = false;

        Debug.Log(FindChild(transform, "a"));
    }

    public static Transform FindChild(Transform parent, string name)
    {
        if (parent == null) return null;

        int cnt = 0;
        Transform child;
        while (parent.childCount > cnt)
        {
            child = parent.GetChild(cnt);
            if (child.name == name) return child;
            cnt++;
        }
        return null;
    }

    [ContextMenu("PopulateMesh")]
    private void VertexHelperPopulate()
    {
        Material material = new Material(Shader.Find("UI/Default"));
        //material.SetColor("_Color", Color.yellow);
        renderer.material = material;
        renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        renderer.receiveShadows = false;

        // 创建一个正方形面片
        using (var vh = new VertexHelper())
        {
            // 添加顶点
            vh.AddVert(new Vector3(0, 0, 0), Color.red, new Vector2(0, 0));
            vh.AddVert(new Vector3(2, 0, 0), Color.red, new Vector2(1, 0));
            vh.AddVert(new Vector3(2, 2, 0), Color.cyan, new Vector2(1, 1));
            vh.AddVert(new Vector3(0, 2, 0), Color.cyan, new Vector2(0, 1));
            vh.AddVert(new Vector3(1, -1, 0), Color.cyan, new Vector2(0, 1));

            // 设置三角形顺序
            vh.AddTriangle(0, 1, 2);
            vh.AddTriangle(0, 3, 2);
            vh.AddTriangle(0, 1, 4);

            // 将结果展示出来
            Mesh mesh = new Mesh();
            mesh.name = "Quad";
            vh.FillMesh(mesh);
            filter.mesh = mesh;
        }
    }

    public GameObject GetTriangle()
    {
        GameObject go = new GameObject("Triangle");
        MeshFilter filter = go.AddComponent<MeshFilter>();

        // 构建三角形的三个顶点，并赋值给Mesh.vertices
        Mesh mesh = new Mesh();
        filter.sharedMesh = mesh;
        mesh.vertices = new Vector3[] {
                new Vector3 (0, 0, 1),
                new Vector3 (0, 2, 0),
                new Vector3 (2, 0, 5),
            };

        // 构建三角形的顶点顺序，因为这里只有一个三角形，
        // 所以只能是(0, 1, 2)这个顺序。
        mesh.triangles = new int[3] { 0, 1, 2 };

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        // 使用Shader构建一个材质，并设置材质的颜色。
        Material material = new Material(Shader.Find("Diffuse"));
        material.SetColor("_Color", Color.yellow);

        // 构建一个MeshRender并把上面创建的材质赋值给它，
        // 然后使其把上面构造的Mesh渲染到屏幕上。
        MeshRenderer renderer = go.AddComponent<MeshRenderer>();
        renderer.sharedMaterial = material;


        return go;
    }
}
