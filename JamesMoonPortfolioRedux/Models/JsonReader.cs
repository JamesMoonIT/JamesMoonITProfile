using Newtonsoft.Json;

namespace JamesMoonPortfolioRedux.Models
{
    public class JsonReader : IJsonClass
    {
        public JsonClass ReadObject(string filePath)
        {
            string json = File.ReadAllText(filePath);
            JsonClass obj = JsonConvert.DeserializeObject<JsonClass>(json);
            return obj;
        }
    }
}