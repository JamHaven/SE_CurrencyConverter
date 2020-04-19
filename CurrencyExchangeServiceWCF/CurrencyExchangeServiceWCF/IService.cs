using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace CurrencyExchangeServiceWCF
{
    [ServiceContract]
    public interface IService
    {

        [OperationContract]
        Response<float> ConvertCurrency(ConvertCurrenyRequest req);

        [OperationContract]
        Response<List<string>> GetCurrencyCodes(GetCurrencyCodesRequest req);

    }
 
    [MessageContract]
    public class ConvertCurrenyRequest 
    {
        [MessageBodyMember]
        public AuthHeader autHeader { get; set; }
        [MessageBodyMember]
        public string toCurrency { get; set; }
        [MessageBodyMember]
        public string fromCurrency { get; set; }
        [MessageBodyMember]
        public float amount { get; set; }
    }
    [MessageContract]
    public class GetCurrencyCodesRequest 
    {
        [MessageBodyMember]
        public AuthHeader autHeader { get; set; }
    }
    
    /**
     * The return value can use for service response for any type of var
     */
    [MessageContract]
    public class Response<T>
    {       
        [MessageBodyMember]
        public T ReturnValue;
    }

    /**
     * Date model for the authentication header 
     */
    [DataContract]
    public class AuthHeader
    {
        public string username;
        public string password;

        [DataMember]
        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        [DataMember]
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
    } 
}
    
