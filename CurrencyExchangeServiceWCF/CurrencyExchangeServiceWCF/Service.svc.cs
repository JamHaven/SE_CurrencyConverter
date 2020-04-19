using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Authentication;
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
        public Response<float> ConvertCurrency(ConvertCurrenyRequest req)
        {
            Response<float> serviceResponse = new Response<float>();
            try
            {
                
                if (!req.autHeader.password.Equals("pa$$w0rd") || !req.autHeader.username.Equals("Admin")){
                    throw new AuthenticationException("authentication not valid"); 
                }
                String toCurrency = req.toCurrency;
                String fromCurrency = req.fromCurrency;
                float value = req.amount;
                toCurrency = toCurrency.ToUpper();

                string currencyInputPattern = @"^[a-zA-Z]{3}$";

                if (String.IsNullOrEmpty(toCurrency))
                {
                    throw new SoapException();
                }
                toCurrency = toCurrency.ToUpper();


                bool isCurrencyValid = Regex.IsMatch(toCurrency, currencyInputPattern);

                if (!isCurrencyValid)
                {
                    throw new SoapException();
                }
                
                serviceResponse.ReturnValue = GetExchangeRate(fromCurrency, toCurrency, value);
                return serviceResponse;
            }
            catch (Exception)
            {
               /* soapException = new SoapException("Wert konnte nicht umgerechnet werden.", SoapException.ClientFaultCode, "");
                throw soapException;*/
               serviceResponse.ReturnValue = 0;
               return serviceResponse;
            }
        }

        private float GetActualConversionRate(string currency)
        {
            if (currency.ToLower() == "")
                throw new ArgumentException("Invalid Argument! currency parameter cannot be empty!");
            if (currency.ToLower() == "eur")
                return 0;
            
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
            CultureInfo ci = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            ci.NumberFormat.CurrencyDecimalSeparator = ".";
            float exchangeRate = float.Parse(
                rate,
                NumberStyles.Any, 
                ci);
            return exchangeRate;
        }

        
        
        public  float GetExchangeRate(string from, string to, float amount = 1)
        {
            // If currency's are empty abort
            if (from == null || to == null)
                return 0;

            // Convert Euro to Euro
            if (from.ToLower() == "eur" && to.ToLower() == "eur")
                return amount;

            try
            {
                // First Get the exchange rate of both currencies in euro
                float toRate = GetActualConversionRate(to);
                float fromRate = GetActualConversionRate(from); 
                
                // Convert Between Euro to Other Currency
                if (from.ToLower() == "eur")
                {
                    return (amount * toRate);
                }
                else if (to.ToLower() == "eur")
                {
                    return (amount / fromRate);
                }
                else
                {
                    // Calculate non EURO exchange rates From A to B
                    return (amount * toRate) / fromRate;
                }
            }
            catch { return 0; }
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
