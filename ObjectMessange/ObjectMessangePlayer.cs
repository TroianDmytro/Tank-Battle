using System.Text.Json;

namespace ObjectMessange
{
    public class ObjectMessangePlayer
    {
        public int ID { get; set; }
        public string Login { get; set; } // Login player
        public string Name { get; set; } // Player або Projectile
        public int LocationPlayerX { get; set; }
        public int LocationPlayerY { get; set; }

        public string VectorProjectile { get; set; }

        public string Command { get; set; }

        public ObjectMessangePlayer()
        {
            ID = 0;
            Login = string.Empty;
            Name = string.Empty;
            LocationPlayerX = -1;
            LocationPlayerY = -1;
            VectorProjectile = string.Empty;
            Command = string.Empty;
        }

       
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

        public override string? ToString()
        {
            return $"ID:{ID}," +
                $"Login:{Login}," +
                $"Name:{Name}," +
                $"LocationPlayerX:{LocationPlayerX}," +
                $"LocationPlayerY:{LocationPlayerY}," +
                $"VectorProjectile:{VectorProjectile}," +
                $"Command:{Command};";
        }
       
    }
}
