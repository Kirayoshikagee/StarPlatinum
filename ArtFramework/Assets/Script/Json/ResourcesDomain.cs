using System.Collections;
using System.Collections.Generic;

public enum ResourcesType {
    None,
    Prefab,
    Sprite,
    Audio,
    Shader,
    Material
}

public class ResourcesDomain  {
    public List<ResourcesDao> resources;

    public void SetResources(List<ResourcesDao> resources) {
        this.resources = resources;
    }

    public List<ResourcesDao> GetResources() {
        return this.resources;
    }
}

public class ResourcesDao {
    public string name;
    public string url;
    public ResourcesType resType;
    public List<ResourcesDao> dependences;

    public string Name {
        get;set;
    }

    public string Url {
        get;set;
    }

    public ResourcesType ResType {
        get;set;
    }

    public void SetDependences(List<ResourcesDao> dependences) {
        this.dependences = dependences;
    }

    public List<ResourcesDao> GetDependences() {
        return this.dependences;
    }
}
