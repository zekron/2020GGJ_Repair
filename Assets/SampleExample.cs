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
        // ����һ����������Ƭ
        VertexHelper vh = new VertexHelper();
        vh.Clear();

        // ��Ӷ���
        vh.AddVert(new Vector3(0, 0, 0), Color.red, new Vector2(0, 0));
        vh.AddVert(new Vector3(2, 0, 0), Color.red, new Vector2(1, 0));
        vh.AddVert(new Vector3(2, 2, 0), Color.cyan, new Vector2(1, 1));
        vh.AddVert(new Vector3(0, 2, 0), Color.cyan, new Vector2(0, 1));
        vh.AddVert(new Vector3(1, -1, 0), Color.cyan, new Vector2(0, 1));

        // ����������˳��
        vh.AddTriangle(0, 2, 1);
        vh.AddTriangle(0, 3, 2);
        vh.AddTriangle(0, 1, 4);

        // �����չʾ����
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

        // ����һ����������Ƭ
        using (var vh = new VertexHelper())
        {
            // ��Ӷ���
            vh.AddVert(new Vector3(0, 0, 0), Color.red, new Vector2(0, 0));
            vh.AddVert(new Vector3(2, 0, 0), Color.red, new Vector2(1, 0));
            vh.AddVert(new Vector3(2, 2, 0), Color.cyan, new Vector2(1, 1));
            vh.AddVert(new Vector3(0, 2, 0), Color.cyan, new Vector2(0, 1));
            vh.AddVert(new Vector3(1, -1, 0), Color.cyan, new Vector2(0, 1));

            // ����������˳��
            vh.AddTriangle(0, 1, 2);
            vh.AddTriangle(0, 3, 2);
            vh.AddTriangle(0, 1, 4);

            // �����չʾ����
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

        // ���������ε��������㣬����ֵ��Mesh.vertices
        Mesh mesh = new Mesh();
        filter.sharedMesh = mesh;
        mesh.vertices = new Vector3[] {
                new Vector3 (0, 0, 1),
                new Vector3 (0, 2, 0),
                new Vector3 (2, 0, 5),
            };

        // ���������εĶ���˳����Ϊ����ֻ��һ�������Σ�
        // ����ֻ����(0, 1, 2)���˳��
        mesh.triangles = new int[3] { 0, 1, 2 };

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        // ʹ��Shader����һ�����ʣ������ò��ʵ���ɫ��
        Material material = new Material(Shader.Find("Diffuse"));
        material.SetColor("_Color", Color.yellow);

        // ����һ��MeshRender�������洴���Ĳ��ʸ�ֵ������
        // Ȼ��ʹ������湹���Mesh��Ⱦ����Ļ�ϡ�
        MeshRenderer renderer = go.AddComponent<MeshRenderer>();
        renderer.sharedMaterial = material;


        return go;
    }
}
