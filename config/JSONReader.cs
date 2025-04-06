using Newtonsoft.Json;

namespace TUDU_BOT.config
{
    internal class JSONReader
    {
        public String token {  get; set; }
        public String prefix { get; set; }

        public async Task ReadJSON()
        {
            using (StreamReader sr = new StreamReader("config.json"))
            {
                String json = await sr.ReadToEndAsync();

                JsonStructure ourData = JsonConvert.DeserializeObject<JsonStructure>(json);

                this.token = ourData.token;
                this.prefix = ourData.prefix;
            }
        }
    }

    internal sealed class JsonStructure 
    {
        public String token { get; set; }
        public String prefix { get; set; }
    }
}
