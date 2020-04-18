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
    public class Service : IService
    {
        SoapException soapException ;
        public Response<string> ConvertCurrency(ConvertCurrenyRequest req)
        {
            try
            {
                Response<string> serviceResponse = new Response<string>();
                if (!req.autHeader.password.Equals("pa$$w0rd") || !req.autHeader.username.Equals("Admin")){
                    serviceResponse.ReturnValue = "authentication not valid"; 
                    return serviceResponse;
                }
                String toCurrency = req.toCurrency;
                String value = req.ccValue;
                toCurrency = toCurrency.ToUpper();

                string currencyInputPattern = @"^[a-zA-Z]{3}$";
                string valueInputPattern = @"^[,.0-9]*$";



                if (String.IsNullOrEmpty(value) || String.IsNullOrEmpty(toCurrency))
                {
                    serviceResponse.ReturnValue = "Input cannot be NULL!";
                    return serviceResponse;
                }
                toCurrency = toCurrency.ToUpper();


                bool isCurrencyValid = Regex.IsMatch(toCurrency, currencyInputPattern);
                bool isValueValid = Regex.IsMatch(value, valueInputPattern);

                if (!isCurrencyValid || !isValueValid)
                {
                    serviceResponse.ReturnValue = "Input cannot not valid!";
                    return serviceResponse;
                }
            
                if (toCurrency.Equals("USD"))
                {
                    serviceResponse.ReturnValue = value;
                    return serviceResponse;
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

                string conversionRateString = GetActualConversionRate(toCurrency);
                double conversionRateDouble = Convert.ToDouble(conversionRateString);
           
                if (toCurrency.Equals("USD")) 
                {
                    value = (Convert.ToDouble(value) / conversionRateDouble).ToString();
                }
                else
                {
                    value = (Convert.ToDouble(value) * conversionRateDouble).ToString();
                }
                serviceResponse.ReturnValue = value;         
                return serviceResponse;
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
            Response<List<string>> serviceResponse = new Response<List<string>>();
            if (!req.autHeader.password.Equals("pa$$w0rd") || !req.autHeader.username.Equals("Admin"))
            {
                serviceResponse.ReturnValue = new List<string> {"authentication not valid"};
                return serviceResponse;
            }
            serviceResponse.ReturnValue = GetCurrencyCodes(); //delegate
            return serviceResponse;
        }

        private List<string> GetCurrencyCodes()
        {
            try
            {
                List<string> currencyList = new List<string>();
                string url = "http://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml";
                XmlTextReader reader = new XmlTextReader(url);
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
            catch (Exception)
            {
                soapException = new SoapException("Wert konnte nicht umgerechnet werden.", SoapException.ClientFaultCode, "");
                throw soapException; 
            }
        }
       
    }
}
