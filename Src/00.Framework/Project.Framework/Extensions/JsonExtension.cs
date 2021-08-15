using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Framework.Extensions
{
    // https://www.newtonsoft.com/json/help/html/ModifyJson.htm modify json
    public static class JsonExtension
    {
        public static string RemoveItem(this string response, string keyItem)
        {
            JObject jo = JObject.Parse(response);
            JToken jsonToken = jo[keyItem];
            if (jsonToken != null)
            {
                jo.Property(keyItem).Remove();
                return jo.ToString();
            }
            return response;
        }
        public static string GetItem(this string response, string keyItem)
        {
            string propertyValue = string.Empty;

            if (!response.IsValidJson())
            {
                return string.Empty;
            }

            JObject jo = JObject.Parse(response);
            JToken jsonToken = jo[keyItem];
            if (jsonToken != null)
            {
                JToken jt = jo.Property(keyItem).Value;
                return jt.Value<string>();
            }
            return string.Empty;
        }
        public static string ChangeItem(this string response, string keyItem , string newItem)
        {
            string propertyValue = string.Empty;

            if (!response.IsValidJson())
            {
                return response;
            }

            JObject jo = JObject.Parse(response);
            JToken jsonToken = jo[keyItem];
            if (jsonToken != null)
            {
                jo[keyItem] = newItem;
                return Newtonsoft.Json.JsonConvert.SerializeObject(jo, Newtonsoft.Json.Formatting.Indented);
            }
            return response;
        }
        public static bool IsValidJson(this string strInput)
        {
            if (string.IsNullOrWhiteSpace(strInput)) { return false; }
            strInput = strInput.Trim();
            if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || //For object
                (strInput.StartsWith("[") && strInput.EndsWith("]"))) //For array
            {
                try
                {
                    var obj = JToken.Parse(strInput);
                    return true;
                }
                catch (JsonReaderException jex)
                {
                    return false;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        //public static void Sample(this string response)
        //{
        //    KeyValuePair<string, JToken> os;
        //    JObject o = (JObject)JsonConvert.DeserializeObject(response);
        //    JToken j;
        //    o.TryGetValue("err", out j);
        //    //o.Property("err").Remove();
        //    foreach (var item in o)
        //    {
        //        if (item.Key == "err")
        //        {
        //            os = item;
        //        }
        //    }

        //    var aJson = JsonConvert.DeserializeObject<JObject>(response);
        //    JToken doc = null;
        //    doc = aJson["Error"];
        //    if (doc == null)
        //    {
        //        doc = aJson["err"];
        //    }

        //    if (doc.Type == JTokenType.Object)
        //    {
        //        //var output = Newtonsoft.Json.JsonConvert.SerializeObject(os.Value);

        //    }
        //    else if (doc.Type == JTokenType.Array)
        //    {
        //        // os map to list
        //    }
        //}
        //public static List<ErrorHotel> GetErrorFromJson(this string response)
        //{
        //    List<ErrorHotel> errors = new List<ErrorHotel>();

        //    JObject jsonObject = (JObject)JsonConvert.DeserializeObject(response);
        //    JToken jsonToken;
        //    jsonObject.TryGetValue("err", out jsonToken);
        //    if (jsonToken == null)
        //    {
        //        jsonObject.TryGetValue("Error", out jsonToken);
        //    }

        //    if (jsonToken != null && jsonToken.Parent.First().HasValues && jsonToken.Type == JTokenType.Object)
        //    {
        //        ErrorHotel err = JsonConvert.DeserializeObject<ErrorHotel>(jsonToken.ToString());
        //        errors.Add(new ErrorHotel
        //        {
        //            code = err.code,
        //            msg = err.msg,
        //            Text = err.Text,
        //            ECcode = err.ECcode
        //        });

        //    }
        //    else if (jsonToken != null && jsonToken.Parent.First().HasValues && jsonToken.Type == JTokenType.Array)
        //    {
        //        List<ErrorHotel> err = JsonConvert.DeserializeObject<List<ErrorHotel>>(jsonToken.ToString());
        //        errors = err;
        //    }

        //    return errors;
        //}
        //public static string IsValidJson(this string strInput)
        //{
        //    if (string.IsNullOrWhiteSpace(strInput)) { return "IsNull"; }
        //    strInput = strInput.Trim();
        //    if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || //For object
        //        (strInput.StartsWith("[") && strInput.EndsWith("]"))) //For array
        //    {
        //        try
        //        {
        //            var obj = JToken.Parse(strInput);
        //            return "IsJson";
        //        }
        //        catch (JsonReaderException jex)
        //        {
        //            return "IsNull";
        //        }
        //        catch (Exception ex)
        //        {
        //            return $"NotMap-{ex.Message}";
        //        }
        //    }
        //    else
        //    {
        //        return "IsString";
        //    }
        //}
    }
}
