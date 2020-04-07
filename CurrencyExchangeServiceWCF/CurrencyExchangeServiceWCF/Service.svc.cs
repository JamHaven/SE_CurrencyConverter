using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml;

namespace CurrencyExchangeServiceWCF
{

    public class Service : IService
    {
        public string ConvertCurrency(string toCurrency, string value)
        {
            string currencyInputPattern = @"^[a-zA-Z]{3}$";
            string valueInputPattern = @"^[,.0-9]*$";

           

            if (String.IsNullOrEmpty(value) || String.IsNullOrEmpty(toCurrency))
            {
                //throw new SoapException("Wert konnte nicht umgerechnet werden.", new System.Xml.XmlQualifiedName("InvalidParameter", this.ToString()));
                return "Input cannot be NULL!";
            }
            toCurrency = toCurrency.ToUpper();


            bool isCurrencyValid = Regex.IsMatch(toCurrency, currencyInputPattern);
            bool isValueValid = Regex.IsMatch(value, valueInputPattern);

            if (!isCurrencyValid || !isValueValid)
            {
                return "Invalid input!";
            }

            // necessary because the European Central Bank only shows the exchange rate list based in EUR, that is why we have to workaround and have our base in USD
            if (toCurrency.Equals("USD"))
            {
                return value;
            }
            if (toCurrency.Equals("EUR"))
                toCurrency = "USD";

            if (!GetCurrencyCodes().Contains(toCurrency))
            {
                return "Currency not available in the CurrencyConverter currencies list.";

            }

            double conversionRateDouble;
            string conversionRateString;
            double valueDouble;

            try
            {
                conversionRateString = GetActualConversionRate(toCurrency);
                conversionRateDouble = Convert.ToDouble(conversionRateString);
                valueDouble = Convert.ToDouble(value);
            }
            catch (Exception)
            {
                return "Input value has invalid number format.";
                //SoapException soapException = new SoapException("Wert konnte nicht umgerechnet werden.", SoapException.ClientFaultCode, "");
                //throw soapException;
            }
            if (toCurrency.Equals("USD"))
            {
                value = (valueDouble / conversionRateDouble).ToString();
            }
            else
            {
                value = (valueDouble * conversionRateDouble).ToString();
            }

            return value;

        }

        private string GetActualConversionRate(string currency)
        {

            string url = "http://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml";
            XmlTextReader reader = new XmlTextReader(url);
            string rate = "";
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element: // The node is an element.

                        while (reader.MoveToNextAttribute())
                        {// Read the attributes.
                            if (reader.Value.Equals(currency))
                            {
                                reader.MoveToAttribute("rate");
                                rate = reader.Value;
                            }
                        }
                        break;
                    case XmlNodeType.Text: //Display the text in each element.
                        break;
                    case XmlNodeType.EndElement: //Display the end of the element.
                        break;

                }
            }
            rate = rate.Replace(".", ",");
            return rate;
        }

        public List<string> GetCurrencyCodes()
        {
            List<string> currencyList = new List<string>();

            string Url = "http://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml";
            XmlTextReader reader = new XmlTextReader(Url);
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element: // The node is an element.
                        while (reader.MoveToNextAttribute())
                        {// Read the attributes.
                            if (reader.Name.Equals("currency"))
                            {
                                currencyList.Add(reader.Value);
                            }
                        }
                        break;
                    case XmlNodeType.Text: //Display the text in each element.
                        break;
                    case XmlNodeType.EndElement: //Display the end of the element.
                        break;
                }
            }
            return currencyList;
        }
    }
}
