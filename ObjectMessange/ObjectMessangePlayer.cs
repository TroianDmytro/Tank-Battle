using System.Text.Json;

namespace ObjectMessange
{
    public class ObjectMessangePlayer
    {
        public int ID { get; set; }

        public string Name { get; set; } // Player або Projectile
        public int LocationPlayerX { get; set; }
        public int LocationPlayerY { get; set; }

        public string VectorProjectile { get; set; }

        public string Command { get; set; }

        public ObjectMessangePlayer()
        {
            ID = 0;
            Name = string.Empty;
            LocationPlayerX = -1;
            LocationPlayerY = -1;
            VectorProjectile = string.Empty;
            Command = string.Empty;
        }

        //public ObjectMessangePlayer( )
        //{
            
        //}
        public static string SerializeToJSON(ObjectMessangePlayer obj)
        {
            string temp = JsonSerializer.Serialize(obj);
            return temp;
        }

        public static ObjectMessangePlayer DesiarilizeFromJSON(string str)
        {
            ObjectMessangePlayer obj = JsonSerializer.Deserialize<ObjectMessangePlayer>(str);
            return obj;
        }
    }
}
