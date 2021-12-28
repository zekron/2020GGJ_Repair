using UnityEngine;
using UnityEngine.UI;

public class Judgement : MonoBehaviour
{
    [SerializeField] MeshFilter filter;
    [SerializeField] new MeshRenderer renderer;
    [SerializeField] int triangleCount = 4;
    [SerializeField] int LeftAngle = 20;
    [SerializeField] int RightAngle = 30;
    [SerializeField] int Radius = 5;

    [SerializeField] Transform A;
    [SerializeField] Transform B;

    VertexHelper vh = new VertexHelper();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.DrawRay(A.position, A.up, Color.red);
        Debug.DrawLine(A.position, B.position, Color.red);

        Debug.Log(IsOutOfBounds(A, B));
    }


    private void OnDrawGizmosSelected()
    {
        #region Draw Triangles
        vh.Clear();
        float x = 0, z = 0;
        vh.AddVert(new Vector3(0, 0, 0), Color.white, new Vector2(0.5f, 0.5f));

        //Left Side
        float delta;
        for (int i = 0; i <= triangleCount * 2; i++)
        {
            if (i <= triangleCount)
            {
                delta = LeftAngle / triangleCount;
                x = Mathf.Sin((i * delta - LeftAngle) * Mathf.Deg2Rad);
                z = Mathf.Cos((i * delta - LeftAngle) * Mathf.Deg2Rad);
                //Debug.LogFormat("({0}, {1})", x, z);
                // 添加顶点
                vh.AddVert(new Vector3(x * Radius, z * Radius, 0), Color.blue, new Vector2(x, z));
            }
            else
            {
                delta = RightAngle / triangleCount;
                x = Mathf.Sin((i - triangleCount) * delta * Mathf.Deg2Rad);
                z = Mathf.Cos((i - triangleCount) * delta * Mathf.Deg2Rad);
                //Debug.LogFormat("({0}, {1})", x, z);
                // 添加顶点
                vh.AddVert(new Vector3(x * Radius, z * Radius, 0), Color.red, new Vector2(x, z));
            }
            if (i > 0)
            {
                vh.AddTriangle(0, i, i + 1);
            }
        }

        //vh.AddTriangle(0, triangleCount * 2, 1);

        Mesh mesh = new Mesh();
        mesh.name = "Range";
        vh.FillMesh(mesh);
        filter.mesh = mesh;

        Material material = new Material(Shader.Find("UI/Default"));
        //material.SetColor("_Color", Color.yellow);
        renderer.material = material;
        renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        renderer.receiveShadows = false;
        #endregion

    }

    string IsOutOfBounds(Transform aimer, Transform target)
    {
        var AxisX = aimer.up;
        var AxisY = target.position - aimer.position;

        float distance = Vector3.Distance(aimer.position, target.position);
        //float dot = Vector3.Dot(aimer.forward, target.position - aimer.position);
        //float angle = Mathf.Acos(Vector3.Dot(A.right, (B.position - A.position).normalized)) * Mathf.Rad2Deg;
        float angle = Vector3.Angle(AxisX, AxisY);

        Vector3 AxisZ = Vector3.Cross(AxisX, AxisY);


        if (distance > Radius) return "Out of radius";
        if (AxisZ.z < 0)    //right side
        {
            if (angle > RightAngle) return "Right side, out of bounds";
            else return "Right side, in bounds";
        }
        else
        {
            if (angle > LeftAngle) return "Left side, out of bounds";
            else return "Left side, in bounds";
        }
    }

    private void OnDrawGizmos()
    {
        
    }
}
