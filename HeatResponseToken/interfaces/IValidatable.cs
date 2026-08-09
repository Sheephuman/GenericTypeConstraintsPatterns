using HeatResponseToken.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeatResponseToken.interfaces
{
    public interface IValidatable
    {
        ValidationResult Validate();
    }
}
