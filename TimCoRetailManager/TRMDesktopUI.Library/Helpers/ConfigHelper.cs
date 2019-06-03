﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRMDesktopUI.Library.Helpers
{
    public class ConfigHelper : IConfigHelper
    {
        public decimal GetTaxRate()
        {
            string tax = ConfigurationManager.AppSettings["taxRate"];
            bool IsValidTaxRate = Decimal.TryParse(tax ,out decimal result);
            if (!IsValidTaxRate)
            {
                throw new ConfigurationErrorsException("The tax rate is not set up properly");
            }
            return result;
        }
    }
}
