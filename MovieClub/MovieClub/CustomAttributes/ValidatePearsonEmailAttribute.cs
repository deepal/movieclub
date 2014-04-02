using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Globalization;

namespace MovieClub.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class ValidatePearsonEmailAttribute:ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            
            return true;
        }
    }
}