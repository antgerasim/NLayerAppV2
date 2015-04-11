using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Crosscutting.Tests.Classes
{

   internal class EntityWithValidationAttribute
   {

      [Required(ErrorMessage = "This is a required property")]
      public string RequiredProperty { get; set; }

   }

}