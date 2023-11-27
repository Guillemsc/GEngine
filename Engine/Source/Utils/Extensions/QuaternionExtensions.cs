using System.Numerics;

namespace GEngine.Utils.Extensions;

public static class QuaternionExtensions
{
    public static Vector3 ToRadiantAngles(this Quaternion quaternion)
    {
        Vector3 angles = new();

        // Roll / x
        double sinrCosp = 2 * (quaternion.W * quaternion.X + quaternion.Y * quaternion.Z);
        double cosrCosp = 1 - 2 * (quaternion.X * quaternion.X + quaternion.Y * quaternion.Y);
        angles.X = (float)Math.Atan2(sinrCosp, cosrCosp);

        // Pitch / y
        double sinp = 2 * (quaternion.W * quaternion.Y - quaternion.Z * quaternion.X);
        if (Math.Abs(sinp) >= 1)
        {
            angles.Y = (float)Math.CopySign(Math.PI / 2, sinp);
        }
        else
        {
            angles.Y = (float)Math.Asin(sinp);
        }

        // Yaw / z
        double sinyCosp = 2 * (quaternion.W * quaternion.Z + quaternion.X * quaternion.Y);
        double cosyCosp = 1 - 2 * (quaternion.Y * quaternion.Y + quaternion.Z * quaternion.Z);
        angles.Z = (float)Math.Atan2(sinyCosp, cosyCosp);

        return angles;
    }

    public static Quaternion Inverse(this Quaternion quaternion)
    {
        return Quaternion.Inverse(quaternion);
    }
}