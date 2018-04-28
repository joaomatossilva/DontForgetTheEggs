using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;

namespace DontForgetTheEggs.Web.Infrastructure.Validation
{
    public class FluentValidatorFactory : ValidatorFactoryBase
    {
        public override IValidator CreateInstance(Type validatorType)
        {
            var validator = StructuremapMvc.StructureMapDependencyScope.CurrentNestedContainer.TryGetInstance(validatorType) as IValidator;
            return validator;
        }
    }
}