using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception
    {
        private Type _validatorType;
        public ValidationAspect(Type validatorType)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new System.Exception("Bu bir doğrulama sınıfı değil");
            }

            _validatorType = validatorType;
        }
        protected override void OnBefore(IInvocation invocation)
        {   //hangi IValidator olcaksa onun istance sini oluştur
            var validator = (IValidator)Activator.CreateInstance(_validatorType);
            //basetype nı bul ve ordaki ilk generic argumanı bul ve onu entityType da tut
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];
            //invocation(method) argumanlarından entitytype a denk geleni entities e at
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);
            foreach (var entity in entities)//her birini tek tek gez
            {
                ValidationTool.Validate(validator, entity); //validationtool u kullanarak validate et
            }
        }
    }
}
