using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class SkillNode
{
    public string id;
    public string name;
    public string[] prerequisites;
    public string ability;
}

[System.Serializable]
public class SkillTreeData
{
    public int version;
    public List<SkillNode> skills;
}

public class SkillTree : MonoBehaviour
{
    [SerializeField] private TextAsset skillConfig;

    private Dictionary<string, SkillNode> nodes = new Dictionary<string, SkillNode>();
    private HashSet<string> unlocked = new HashSet<string>();

    private void Awake()
    {
        if (skillConfig != null)
        {
            Load(skillConfig.text);
        }
    }

    public void Load(string json)
    {
        var data = JsonUtility.FromJson<SkillTreeData>(json);
        nodes = data.skills.ToDictionary(s => s.id, s => s);
    }

    public bool CanUnlock(string id)
    {
        if (!nodes.ContainsKey(id)) return false;
        foreach (var pre in nodes[id].prerequisites)
            if (!unlocked.Contains(pre))
                return false;
        return true;
    }

    public bool Unlock(string id)
    {
        if (CanUnlock(id))
        {
            unlocked.Add(id);
            return true;
        }
        return false;
    }

    public bool IsUnlocked(string id) => unlocked.Contains(id);
}
