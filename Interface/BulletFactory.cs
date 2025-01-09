using CustomProject.GameObject;
using CustomProject.Interface;

namespace CustomProject
{
    public class BulletFactory //Factory pattern implementaion for 3 types of bullet: AP, HE, and Multiple Bullet
    {
        public enum BulletType
        {
            APBullet, HEBullet, MultipleBullet
        }

        public static IBullet CreateBullet(BulletType type, float x, float y, float speed)
        {
            switch (type)
            {
                case BulletType.APBullet:
                    return new APBullet(x, y, speed);
                case BulletType.HEBullet:
                    return new HEBullet(x, y, speed - 3);
                case BulletType.MultipleBullet:
                    return new MultipleBullet(x, y, speed - 1);
                default:
                    throw new ArgumentException("Invalid bullet type");
            }
        }
    }
}
