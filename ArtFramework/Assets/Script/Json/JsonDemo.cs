using System.Collections;
using System.Collections.Generic;

public class JsonDemo {
    public List<JsonInfo> sites = new List<JsonInfo>();

    public void SetSites(List<JsonInfo> sites) {
        this.sites = sites;
    }

    public List<JsonInfo> GetSites() {
        return this.sites;
    }
}

public class JsonInfo {
    public string name;
    public string url;

    public string Name {
        get { return this.name; }
        set { this.name = value; }
    }

    public string Url {
        get { return this.url; }
        set { this.url = value; }
    }
}
