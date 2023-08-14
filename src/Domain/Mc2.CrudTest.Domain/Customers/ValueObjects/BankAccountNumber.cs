using EmailCore.Validation;
using IbanNet;
using Mc2.CrudTest.Domain.Customers.Exceptions;
using Mc2.CrudTest.Presentation.Shared.Exceptions;
using Mc2.CrudTest.Presentation.Shared.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Domain.Customers.ValueObjects
{
    public class BankAccountNumber : ValueObject
    {
        public static implicit operator BankAccountNumber(string? value)
        {
            if (string.IsNullOrEmpty(value))
                return null;

            return new BankAccountNumber(value: value);
        }

        private BankAccountNumber() : base()
        {
        }
        private BankAccountNumber(string value) : this()
        {
            Value = value;
            Validate();
        }

        public string Value { get; init; }

        private void Validate()
        {
            var validator = new IbanValidator();
            ValidationResult validationResult = validator.Validate("Value");
            if (!validationResult.IsValid)
                throw new InvalidBankAccountNumberException(ExceptionsEnum.InvalidBankAccountNumberException, Value);
        }
    }
}
