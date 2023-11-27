using System.Numerics;

namespace GEngine.Utils.Extensions;

public static class Matrix4X4Extensions
{
    public static Matrix4x4 Compose(
        Vector3 position,
        Quaternion rotation,
        Vector3 scale
    )
    {
        return Matrix4x4.CreateScale(scale) * 
               Matrix4x4.CreateFromQuaternion(rotation) *
               Matrix4x4.CreateTranslation(position);
    }
    
    public static void Decompose(
        this Matrix4x4 matrix4X4,
        out Vector3 position,
        out Quaternion rotation,
        out Vector3 scale
    )
    {
        Matrix4x4.Decompose(matrix4X4, out scale, out rotation, out position);
    }
}