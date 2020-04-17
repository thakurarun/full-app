using NJsonSchema.Annotations;

namespace backend.Controllers.Common
{
    public class FieldLabel : JsonSchemaExtensionDataAttribute
    {
        public FieldLabel(string value) : base("label", value)
        {
        }
    }

    public class FieldEditor : JsonSchemaExtensionDataAttribute
    {
        public FieldEditor(string value) : base("editor", value)
        {
        }
    }

    public class FieldRequiredErrorMessage : JsonSchemaExtensionDataAttribute
    {
        public FieldRequiredErrorMessage(string value) : base("required_error", value)
        {
        }
    }

    public class FieldPatternErrorMessage : JsonSchemaExtensionDataAttribute
    {
        public FieldPatternErrorMessage(string value) : base("pattern", value)
        {
        }
    }
}
///^(([^<>()\[\]\.,;:\s@\"]+(\.[^<>()\[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/i