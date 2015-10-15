using System.IO;
using System.Xml.Serialization;

[XmlRoot("Stats")]
public struct Stats 
{

    public string name;

    public int attack;
    public int defense;
    public int strength;
    public int resilience;

    public float moveSpeed;
    public float turnSpeed;

    public void WriteData(string filePath)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(Stats));
        FileStream stream = new FileStream(GameManager.STATS_DATA_PATH, FileMode.Create);
        serializer.Serialize(stream, this);
        stream.Close();
    }

    public static Stats LoadFromFile(string filePath)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(Stats));
        FileStream stream = new FileStream(GameManager.STATS_DATA_PATH, FileMode.Open);
        Stats container = (Stats)serializer.Deserialize(stream);
        stream.Close();
        return container;
    }
}
