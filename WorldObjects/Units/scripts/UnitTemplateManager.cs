using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class UnitTemplateManager : MonoBehaviour
{    
    static Dictionary<int, UnitTemplate> templates = new Dictionary<int, UnitTemplate>();

    //public Unit genericUnit;

    public static UnitTemplate UnitReference (int templateID)
    {
        return templates[templateID];
    }

    public void Init()
    {
        DirectoryInfo directory = new DirectoryInfo(GameManager.STATS_DATA_PATH);
        FileInfo[] files = directory.GetFiles();
        for (int i = 0; i < files.Length; i++)
        {
            if (!files[i].Name.Contains(".meta")) // Litte bastards. Hiding in the directory, screwing things up.
            {
                AddTemplate(files[i].Name);
            }
        }
    }

    void AddTemplate(string name)
    {
        UnitTemplate template = UnitTemplate.LoadFromFile(GameManager.STATS_DATA_PATH, name);
        templates.Add(template.templateID, template);
    }
}
