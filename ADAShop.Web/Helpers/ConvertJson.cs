using Newtonsoft.Json;
using System.Runtime.Serialization.Json;

namespace ADAShop.Web.Helpers
{
    public static class ConvertJson
    {
        public static T ConvertToObject<T>(object value) => JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(value))!;

        public static string SerializeObjectJson<T>(object value)
        {
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(T));
            MemoryStream msObj = new MemoryStream();
            js.WriteObject(msObj, value);
            msObj.Position = 0;
            StreamReader sr = new StreamReader(msObj);
            string json = sr.ReadToEnd();
            sr.Close();
            msObj.Close();
            return json;
        }
    }
}