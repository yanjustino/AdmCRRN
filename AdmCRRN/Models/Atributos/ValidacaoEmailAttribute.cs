using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace AdmCRRN.Models.Atributos
{    
    public class ValidacaoEmailAttribute : RegularExpressionAttribute, IClientValidatable
    {
        public ValidacaoEmailAttribute()
            : base("^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,6}$")
        {
            this.ErrorMessage = (this.ErrorMessage == null || this.ErrorMessage.Length == 0) ? "Email inválido" : this.ErrorMessage;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRegexRule(this.ErrorMessageString, base.Pattern);
            return new[] { rule };
        }
    }
}
