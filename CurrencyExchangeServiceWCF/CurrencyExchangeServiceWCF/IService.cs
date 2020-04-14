using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace CurrencyExchangeServiceWCF
{
    [ServiceContract]
    public interface IService
    {

     
        [OperationContract]
        
        string ConvertCurrency(String toCurrency, string value);


        [OperationContract]
        
        List<string> GetCurrencyCodes();
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        [DataMember]
        public bool BoolValue { get; set; } = true;

        [DataMember]
        public double DoubleValue { get; set; } = 0.0;

        [DataMember]
        public string StringValue { get; set; } = "Hello ";
    }
}
