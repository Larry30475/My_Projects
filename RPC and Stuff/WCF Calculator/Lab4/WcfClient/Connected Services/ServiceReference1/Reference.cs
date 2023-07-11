﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ten kod został wygenerowany przez narzędzie.
//     Wersja wykonawcza:4.0.30319.42000
//
//     Zmiany w tym pliku mogą spowodować nieprawidłowe zachowanie i zostaną utracone, jeśli
//     kod zostanie ponownie wygenerowany.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WcfClient.ServiceReference1 {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="PrimeNumbersResult", Namespace="http://schemas.datacontract.org/2004/07/WcfService")]
    [System.SerializableAttribute()]
    public partial class PrimeNumbersResult : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Numerics.BigInteger CountField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Numerics.BigInteger LargestPrimeField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Numerics.BigInteger Count {
            get {
                return this.CountField;
            }
            set {
                if ((this.CountField.Equals(value) != true)) {
                    this.CountField = value;
                    this.RaisePropertyChanged("Count");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Numerics.BigInteger LargestPrime {
            get {
                return this.LargestPrimeField;
            }
            set {
                if ((this.LargestPrimeField.Equals(value) != true)) {
                    this.LargestPrimeField = value;
                    this.RaisePropertyChanged("LargestPrime");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.ICalculator")]
    public interface ICalculator {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICalculator/iAdd", ReplyAction="http://tempuri.org/ICalculator/iAddResponse")]
        int iAdd(int val1, int val2);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICalculator/iAdd", ReplyAction="http://tempuri.org/ICalculator/iAddResponse")]
        System.Threading.Tasks.Task<int> iAddAsync(int val1, int val2);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICalculator/iSub", ReplyAction="http://tempuri.org/ICalculator/iSubResponse")]
        int iSub(int val1, int val2);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICalculator/iSub", ReplyAction="http://tempuri.org/ICalculator/iSubResponse")]
        System.Threading.Tasks.Task<int> iSubAsync(int val1, int val2);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICalculator/iMul", ReplyAction="http://tempuri.org/ICalculator/iMulResponse")]
        int iMul(int val1, int val2);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICalculator/iMul", ReplyAction="http://tempuri.org/ICalculator/iMulResponse")]
        System.Threading.Tasks.Task<int> iMulAsync(int val1, int val2);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICalculator/iDiv", ReplyAction="http://tempuri.org/ICalculator/iDivResponse")]
        int iDiv(int val1, int val2);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICalculator/iDiv", ReplyAction="http://tempuri.org/ICalculator/iDivResponse")]
        System.Threading.Tasks.Task<int> iDivAsync(int val1, int val2);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICalculator/iMod", ReplyAction="http://tempuri.org/ICalculator/iModResponse")]
        int iMod(int val1, int val2);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICalculator/iMod", ReplyAction="http://tempuri.org/ICalculator/iModResponse")]
        System.Threading.Tasks.Task<int> iModAsync(int val1, int val2);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICalculator/CountAndFindMaxPrimeNumbers", ReplyAction="http://tempuri.org/ICalculator/CountAndFindMaxPrimeNumbersResponse")]
        WcfClient.ServiceReference1.PrimeNumbersResult CountAndFindMaxPrimeNumbers(System.Numerics.BigInteger L1, System.Numerics.BigInteger L2);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICalculator/CountAndFindMaxPrimeNumbers", ReplyAction="http://tempuri.org/ICalculator/CountAndFindMaxPrimeNumbersResponse")]
        System.Threading.Tasks.Task<WcfClient.ServiceReference1.PrimeNumbersResult> CountAndFindMaxPrimeNumbersAsync(System.Numerics.BigInteger L1, System.Numerics.BigInteger L2);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ICalculatorChannel : WcfClient.ServiceReference1.ICalculator, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CalculatorClient : System.ServiceModel.ClientBase<WcfClient.ServiceReference1.ICalculator>, WcfClient.ServiceReference1.ICalculator {
        
        public CalculatorClient() {
        }
        
        public CalculatorClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public CalculatorClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CalculatorClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CalculatorClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public int iAdd(int val1, int val2) {
            return base.Channel.iAdd(val1, val2);
        }
        
        public System.Threading.Tasks.Task<int> iAddAsync(int val1, int val2) {
            return base.Channel.iAddAsync(val1, val2);
        }
        
        public int iSub(int val1, int val2) {
            return base.Channel.iSub(val1, val2);
        }
        
        public System.Threading.Tasks.Task<int> iSubAsync(int val1, int val2) {
            return base.Channel.iSubAsync(val1, val2);
        }
        
        public int iMul(int val1, int val2) {
            return base.Channel.iMul(val1, val2);
        }
        
        public System.Threading.Tasks.Task<int> iMulAsync(int val1, int val2) {
            return base.Channel.iMulAsync(val1, val2);
        }
        
        public int iDiv(int val1, int val2) {
            return base.Channel.iDiv(val1, val2);
        }
        
        public System.Threading.Tasks.Task<int> iDivAsync(int val1, int val2) {
            return base.Channel.iDivAsync(val1, val2);
        }
        
        public int iMod(int val1, int val2) {
            return base.Channel.iMod(val1, val2);
        }
        
        public System.Threading.Tasks.Task<int> iModAsync(int val1, int val2) {
            return base.Channel.iModAsync(val1, val2);
        }
        
        public WcfClient.ServiceReference1.PrimeNumbersResult CountAndFindMaxPrimeNumbers(System.Numerics.BigInteger L1, System.Numerics.BigInteger L2) {
            return base.Channel.CountAndFindMaxPrimeNumbers(L1, L2);
        }
        
        public System.Threading.Tasks.Task<WcfClient.ServiceReference1.PrimeNumbersResult> CountAndFindMaxPrimeNumbersAsync(System.Numerics.BigInteger L1, System.Numerics.BigInteger L2) {
            return base.Channel.CountAndFindMaxPrimeNumbersAsync(L1, L2);
        }
    }
}