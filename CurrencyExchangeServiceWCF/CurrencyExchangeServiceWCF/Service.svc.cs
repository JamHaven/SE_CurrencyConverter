using System;
using System.Collections.Generic;
using System.Web.Services.Protocols;
using System.Text.RegularExpressions;
using System.Xml;

namespace CurrencyExchangeServiceWCF
{
    //souce:        https://www.c-sharpcorner.com/blogs/message-contract-and-its-implementation
    //              https://www.c-sharpcorner.com/UploadFile/788083/how-to-implement-message-contract-in-wcf/
    //encription :  https://docs.microsoft.com/en-us/dotnet/framework/wcf/feature-details/using-message-contracts
    public class Service1 : IService1
    {
        SoapException soapException ;
        public Response<string> ConvertCurrency(ConvertCurrenyRequest req)
        {
            try
            {
                Response<string> r = new Response<string>();
                if (!req.autHeader.password.Equals("pa$$w0rd") || !req.autHeader.username.Equals("Admin")){
                    r.ReturnValue = "authentication not valid"; 
                    return r;
                }
                String toCurrency = req.toCurrency;
                String value = req.ccValue;
                toCurrency = toCurrency.ToUpper();

                string currencyInputPattern = @"^[a-zA-Z]{3}$";
                string valueInputPattern = @"^[,.0-9]*$";



                if (String.IsNullOrEmpty(value) || String.IsNullOrEmpty(toCurrency))
                {
                    r.ReturnValue = "Input cannot be NULL!";
                    return r;
                }
                toCurrency = toCurrency.ToUpper();


                bool isCurrencyValid = Regex.IsMatch(toCurrency, currencyInputPattern);
                bool isValueValid = Regex.IsMatch(value, valueInputPattern);

                if (!isCurrencyValid || !isValueValid)
                {
                    r.ReturnValue = "Input cannot be NULL!";
                    return r;
                }

                double ConversionRateDouble;
                string ConversionRateString;
            
                if (toCurrency.Equals("USD"))
                {
                    r.ReturnValue = value;
                    return r;
                }
                if (toCurrency.Equals("EUR"))
                    toCurrency = "USD";

                if (!GetCurrencyCodes().Contains(toCurrency))
                {
                    soapException = new SoapException("Currency not available in the CurrencyConverter currencies list.", SoapException.ClientFaultCode, "");
                    throw soapException;

                }
                if (String.IsNullOrEmpty(value))
                {
                    soapException = new SoapException("Wert darf nicht 0 sein.", SoapException.ClientFaultCode, "");
                    throw soapException;
                }
           
                ConversionRateString = GetActualConversionRate(toCurrency);
                ConversionRateDouble = Convert.ToDouble(ConversionRateString);
           
                if (toCurrency.Equals("USD")) 
                {
                    value = (Convert.ToDouble(value) / ConversionRateDouble).ToString();
                }
                else
                {
                    value = (Convert.ToDouble(value) * ConversionRateDouble).ToString();
                }           
                r.ReturnValue = value;         
                return r;
            }
            catch (Exception)
            {
                soapException = new SoapException("Wert konnte nicht umgerechnet werden.", SoapException.ClientFaultCode, "");
                throw soapException;
            }
        }

        private string GetActualConversionRate(string Currency)
        {
            string Url = "http://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml";
            XmlTextReader reader = new XmlTextReader(Url);
            string rate = "";
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element: // The node is an element.

                        while (reader.MoveToNextAttribute())
                        {// Read the attributes.
                            if (reader.Value.Equals(Currency))
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

        public Response<List<string>> GetCurrencyCodes(GetCurrencyCodesRequest req)
        {
            Response<List<string>> r = new Response<List<string>>();
            if (!req.autHeader.password.Equals("pa$$w0rd") || !req.autHeader.username.Equals("Admin"))
            {
                r.ReturnValue = new List<string> {"authentication not valid"};
                return r;
            }
            r.ReturnValue = GetCurrencyCodes(); //delegate
            return r;
        }

        private List<string> GetCurrencyCodes()
        {
            try
            {
                List<string> CurrencyList = new List<string>();
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
                                    CurrencyList.Add(reader.Value);
                                }
                            }
                            break;
                        case XmlNodeType.Text: //Display the text in each element.
                            break;
                        case XmlNodeType.EndElement: //Display the end of the element.
                            break;
                    }
                }
                return CurrencyList;
            }
            catch (Exception)
            {
                soapException = new SoapException("Wert konnte nicht umgerechnet werden.", SoapException.ClientFaultCode, "");
                throw soapException; 
            }
        }
       
    }
}
