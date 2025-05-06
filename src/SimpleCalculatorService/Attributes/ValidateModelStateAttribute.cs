using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SimpleCalculatorService.Attributes
{
    /// <summary>  
    /// Model state validation attribute  
    /// </summary>  
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        /// <summary>  
        /// Called before the action method is invoked  
        /// </summary>  
        /// <param name="context"></param>  
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Per https://blog.markvincze.com/how-to-validate-action-parameters-with-dataannotation-attributes/  
            var descriptor = context.ActionDescriptor as ControllerActionDescriptor;
            if (descriptor is not null)
            {
                foreach (var parameter in descriptor.MethodInfo.GetParameters())
                {
                    if (parameter.Name != null && context.ActionArguments.TryGetValue(parameter.Name, out var args))
                    {
                        ValidateAttributes(parameter, args, context.ModelState);
                    }
                }
            }

            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }

        private void ValidateAttributes(ParameterInfo parameter, object? args, ModelStateDictionary modelState)
        {
            foreach (var attributeData in parameter.CustomAttributes)
            {
                var attributeInstance = parameter.GetCustomAttribute(attributeData.AttributeType);

                var validationAttribute = attributeInstance as ValidationAttribute;
                if (validationAttribute is not null)
                {
                    var isValid = validationAttribute.IsValid(args);
                    if (!isValid)
                    {
                        var parameterName = parameter.Name ?? "UnknownParameter"; // Ensure parameter.Name is not null  
                        modelState.AddModelError(parameterName, validationAttribute.FormatErrorMessage(parameterName));
                    }
                }
            }
        }
    }
}
