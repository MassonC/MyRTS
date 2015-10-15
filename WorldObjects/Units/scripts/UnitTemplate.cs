using System.IO;
using System.Xml.Serialization;

[XmlRoot ("UnitTemplate")]
public struct UnitTemplate
{
    public string name;
    public int templateID;

    public int attack;
    public int defense;
    public int strength;
    public int resilience;

    public float moveSpeed;
    public float turnSpeed;

    public void WriteData(string filePath)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(UnitTemplate));
        FileStream stream = new FileStream(filePath + name + ".xml", FileMode.Create);
        serializer.Serialize(stream, this);
        stream.Close();
    }

    public static UnitTemplate LoadFromFile(string filePath, string name)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(UnitTemplate));
        FileStream stream = new FileStream(filePath + name, FileMode.Open);
        UnitTemplate template = (UnitTemplate)serializer.Deserialize(stream);
        stream.Close();
        return template;
    }
}
