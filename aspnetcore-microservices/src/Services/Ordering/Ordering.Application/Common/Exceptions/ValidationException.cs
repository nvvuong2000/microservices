﻿using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Common.Exceptions
{
    public class ValidationException : ApplicationException
    {
        public ValidationException(): base("One or more validation failures have occured")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(IEnumerable<ValidationFailure> failures) : this()
        {
            Errors = failures
                .GroupBy(e=>e.PropertyName, e=>e.ErrorMessage)
                .ToDictionary(e => e.Key, e => e.ToArray());

        }
        public Dictionary<string, string[]> Errors { get; }
    }
}
