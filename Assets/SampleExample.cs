using UnityEngine;
using UnityEngine.UI;

public class SampleExample : MonoBehaviour
{
    [SerializeField] Mesh mesh;
    [SerializeField] MeshFilter filter;
    [SerializeField] new MeshRenderer renderer;
    [SerializeField] float[] values;

    void FixedUpdate()
    {
        VertexHelperPopulate();
        //Debug.Log(FindChild(transform, "a"));
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
            int length = values.Length;
            float delta = Mathf.PI * 2 / length;
            float x = 0, y = 0;
            // 添加顶点
            vh.AddVert(new Vector3(0, 0, 0), Color.white, new Vector2(0, 0));
            for (int i = 0; i < length; i++)
            {
                x = Mathf.Sin(i * delta);
                y = Mathf.Cos(i * delta);
                vh.AddVert(new Vector3(x * values[i], y * values[i], 0), Color.yellow, new Vector2(1, 1));

                if (i > 0)
                {
                    vh.AddTriangle(0, i, i + 1);
                }
            }
            vh.AddTriangle(0, length, 1);

            // 将结果展示出来
            Mesh mesh = new Mesh();
            mesh.name = "Quad";
            vh.FillMesh(mesh);
            filter.mesh = mesh;
        }
    }
}
