﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebServiceClient.ServiceReference1 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IService1")]
    public interface IService1 {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/ConvertCurrency", ReplyAction="http://tempuri.org/IService1/ConvertCurrencyResponse")]
        string ConvertCurrency(string toCurrency, string value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/ConvertCurrency", ReplyAction="http://tempuri.org/IService1/ConvertCurrencyResponse")]
        System.Threading.Tasks.Task<string> ConvertCurrencyAsync(string toCurrency, string value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetCurrencyCodes", ReplyAction="http://tempuri.org/IService1/GetCurrencyCodesResponse")]
        string[] GetCurrencyCodes();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetCurrencyCodes", ReplyAction="http://tempuri.org/IService1/GetCurrencyCodesResponse")]
        System.Threading.Tasks.Task<string[]> GetCurrencyCodesAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IService1Channel : WebServiceClient.ServiceReference1.IService1, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class Service1Client : System.ServiceModel.ClientBase<WebServiceClient.ServiceReference1.IService1>, WebServiceClient.ServiceReference1.IService1 {
        
        public Service1Client() {
        }
        
        public Service1Client(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public Service1Client(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string ConvertCurrency(string toCurrency, string value) {
            return base.Channel.ConvertCurrency(toCurrency, value);
        }
        
        public System.Threading.Tasks.Task<string> ConvertCurrencyAsync(string toCurrency, string value) {
            return base.Channel.ConvertCurrencyAsync(toCurrency, value);
        }
        
        public string[] GetCurrencyCodes() {
            return base.Channel.GetCurrencyCodes();
        }
        
        public System.Threading.Tasks.Task<string[]> GetCurrencyCodesAsync() {
            return base.Channel.GetCurrencyCodesAsync();
        }
    }
}