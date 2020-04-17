using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NJsonSchema;
using NJsonSchema.Generation;

namespace backend.Controllers.Common
{
    public class JsonUtil
    {
        private readonly static JsonSerializerSettings DefaultJsonSerializerSettings =
            new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };


        public static string CreateJsonSchema<T>(string typeName) where T : class
        {
            var type = typeof(T);
            var schema = JsonSchema
                .FromType<T>(new JsonSchemaGeneratorSettings
                {
                    SerializerSettings = DefaultJsonSerializerSettings
                });
            schema.Title = typeName ?? type.Name;
            var schemaData = schema.ToJson();
            return schemaData;
        }

        public static string ModelStateToJson(ModelStateDictionary modelState)
        {
            var serializableModelState = new SerializableError(modelState);
            var modelStateJson = JsonConvert.SerializeObject(serializableModelState, DefaultJsonSerializerSettings);
            return modelStateJson;
        }
    }
}
