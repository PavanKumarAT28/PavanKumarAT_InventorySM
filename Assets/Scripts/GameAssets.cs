using UnityEngine;

[CreateAssetMenu]
public class GameAssets : ScriptableObject
{
    [Header("Spites")]
    public Sprite Cube;
    public Sprite Sphere;
    public Sprite Cylinder;
    public Sprite Capsule;

    [Header("Meshs")]
    public Mesh cubeMesh;
    public Mesh sphereMesh;
    public Mesh cylinderMesh;
    public Mesh capsuleMesh;
}
