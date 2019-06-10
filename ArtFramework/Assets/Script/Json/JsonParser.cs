using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using LitJson;

public static class JsonParser  {

    
    public static string TestParser(string jsonString) {
        //JsonMapper.ToJson(jsonString);
        StringBuilder builder = new StringBuilder();
        JsonDemo jsonObj = JsonMapper.ToObject<JsonDemo>(jsonString);
        for (int i = 0; i < jsonObj.GetSites().Count; i++) {
            JsonInfo info = jsonObj.GetSites()[i];
            builder.Append("name:" + info.Name + "; url:" + info.Url).Append("\n");
        }

        return builder.ToString();
    }
}
