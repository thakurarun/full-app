using backend.Controllers.Common;
using Newtonsoft.Json;
using NJsonSchema.Annotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace backend.Controllers.Login.Register
{
    [Serializable]
    public class SignUpModel
    {
        [EmailAddress]
        [MaxLength(50)]
        [FieldLabel("Email address")]
        [FieldEditor(Editor.Email)]
        public string Email { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        [FieldLabel("Full name")]
        [FieldEditor(Editor.Textbox)]
        public string FullName { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(20)]
        [FieldLabel("Password")]
        [FieldEditor(Editor.Password)]
        public string Password { get; set; }
        [Compare(nameof(Password))]
        [JsonSchemaExtensionData("compare", "password")] // should match password
        [FieldLabel("Confirm password")]
        [FieldEditor(Editor.Password)]
        public string ConfirmPassword { get; set; }
    }
}
