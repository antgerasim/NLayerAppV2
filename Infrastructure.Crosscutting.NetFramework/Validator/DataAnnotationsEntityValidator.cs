//===================================================================================
// Microsoft Developer & Platform Evangelism
//=================================================================================== 
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES 
// OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
//===================================================================================
// Copyright (c) Microsoft Corporation.  All Rights Reserved.
// This code is released under the terms of the MS-LPL license, 
// http://microsoftnlayerapp.codeplex.com/license
//===================================================================================

using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using Microsoft.Samples.NLayerApp.Infrastructure.Crosscutting.Validator;

namespace Microsoft.Samples.NLayerApp.Infrastructure.Crosscutting.NetFramework.Validator
{

   /// <summary>
   ///    Validator based on Data Annotations.
   ///    This validator use IValidatableObject interface and
   ///    ValidationAttribute ( hierachy of this) for
   ///    perform validation
   /// </summary>
   public class DataAnnotationsEntityValidator : IEntityValidator
   {
      #region Private Methods
      /// <summary>
      ///    Get erros if object implement IValidatableObject
      /// </summary>
      /// <typeparam name="TEntity">The typeof entity</typeparam>
      /// <param name="item">The item to validate</param>
      /// <param name="errors">A collection of current errors</param>
      private void SetValidatableObjectErrors<TEntity>(TEntity item, List<string> errors) where TEntity : class
      {
         if (typeof (IValidatableObject).IsAssignableFrom(typeof (TEntity)))
         {
            var validationContext = new ValidationContext(item, null, null);

            var validationResults = ((IValidatableObject) item).Validate(validationContext);

            errors.AddRange(validationResults.Select(vr => vr.ErrorMessage));
         }
      }

      /// <summary>
      ///    Get errors on ValidationAttribute
      /// </summary>
      /// <typeparam name="TEntity">The type of entity</typeparam>
      /// <param name="item">The entity to validate</param>
      /// <param name="errors">A collection of current errors</param>
      private void SetValidationAttributeErrors<TEntity>(TEntity item, List<string> errors) where TEntity : class
      {
         var result = from property in TypeDescriptor.GetProperties(item).Cast<PropertyDescriptor>()
            from attribute in property.Attributes.OfType<ValidationAttribute>()
            where !attribute.IsValid(property.GetValue(item))
            select attribute.FormatErrorMessage(string.Empty);

         if (result != null && result.Any()) { errors.AddRange(result); }
      }
      #endregion

      #region IEntityValidator Members
      /// <summary>
      ///    <see cref="Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.Validator.IEntityValidator" />
      /// </summary>
      /// <typeparam name="TEntity">
      ///    <see cref="Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.Validator.IEntityValidator" />
      /// </typeparam>
      /// <param name="item">
      ///    <see cref="Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.Validator.IEntityValidator" />
      /// </param>
      /// <returns>
      ///    <see cref="Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.Validator.IEntityValidator" />
      /// </returns>
      public bool IsValid<TEntity>(TEntity item) where TEntity : class
      {

         if (item == null) { return false; }

         var validationErrors = new List<string>();

         SetValidatableObjectErrors(item, validationErrors);
         SetValidationAttributeErrors(item, validationErrors);

         return !validationErrors.Any();
      }

      /// <summary>
      ///    <see cref="Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.Validator.IEntityValidator" />
      /// </summary>
      /// <typeparam name="TEntity">
      ///    <see cref="Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.Validator.IEntityValidator" />
      /// </typeparam>
      /// <param name="item">
      ///    <see cref="Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.Validator.IEntityValidator" />
      /// </param>
      /// <returns>
      ///    <see cref="Microsoft.Samples.NLayerApp.Infrastructure.CrossCutting.Validator.IEntityValidator" />
      /// </returns>
      public IEnumerable<string> GetInvalidMessages<TEntity>(TEntity item) where TEntity : class
      {
         if (item == null) { return null; }

         var validationErrors = new List<string>();

         SetValidatableObjectErrors(item, validationErrors);
         SetValidationAttributeErrors(item, validationErrors);

         return validationErrors;
      }
      #endregion
   }

}