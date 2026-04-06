using UnityEngine;
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(EdgeCollider2D))]
[RequireComponent(typeof(WaterTriggerHandler))]
public class eauinteractive : MonoBehaviour
{
[Header("Mesh generation")]
[Range(2, 500)] public int NombredePointsX = 70;
public float Width = 10f;
public float Height = 4f;
public Material Materiaueau;
private const int NombredePointsY = 2;

[Header("Gizmo")]
public Color GizmoColor = Color.white;

private Mesh mesh;
private MeshRenderer meshRenderer;
private MeshFilter meshFilter;
private Vector3[] vertices;
private int[] topVertices;

private EdgeCollider2D edgeCollider;

   private void Reset()
{
  edgeCollider = GetComponent<EdgeCollider2D>();
  edgeCollider.isTrigger = true;
}

public void GenerateMesh(){
mesh = new Mesh();
//ajouter les points
vertices = new Vector3[NombredePointsX * NombredePointsY];
topVertices = new int[NombredePointsX];
for (int y = 0; y < NombredePointsY; y++){
for (int x = 0; x < NombredePointsX; x++){
float xPos = (x / (float)(NombredePointsX - 1)) * Width / 2;
float yPos = (y / (float)(NombredePointsY - 1)) * Height;
vertices[y * NombredePointsX + x] = new Vector3(xPos, yPos, 0);

if (y == NombredePointsY - 1){
topVertices[y * NombredePointsX + x] = y * NombredePointsX + x;

}
}
//configurer les triangles
int[] triangles = new int[(NombredePointsX - 1) * (NombredePointsY - 1) * 6];
int t = 0;
for (int y = 0; y < NombredePointsY - 1; y++){
for (int x = 0; x < NombredePointsX - 1; x++){
int i = y * NombredePointsX + x;
triangles[t++] = i;
triangles[t++] = i + NombredePointsX;
triangles[t++] = i + 1;
triangles[t++] = i + 1;
triangles[t++] = i + NombredePointsX;
triangles[t++] = i + NombredePointsX + 1;
}

//uv 
Vector2[] uv = new Vector2[vertices.Length];
for (int i = 0; i < vertices.Length; i++){
uv[i] = new Vector2(vertices[i].x / Width, vertices[i].y / Height);
}
if (meshFilter == null) meshFilter = GetComponent<MeshFilter>();
if (meshRenderer == null) meshRenderer = GetComponent<MeshRenderer>();
meshRenderer.material = Materiaueau;
mesh.vertices = vertices;
mesh.triangles = triangles;
mesh.uv = uv;
mesh.RecalculateNormals();
mesh.RecalculateBounds();
meshFilter.mesh = mesh;

}
}
}
}