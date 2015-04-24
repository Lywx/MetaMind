﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MetaMind.Acutance.PerseveranceServiceReference {
    using MetaMind.Engine.Services;
    using MetaMind.Perseverance.Concepts;
    using MetaMind.Runtime.Concepts.Synchronizations;
    using MetaMind.Runtime.Concepts.Tasks;

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="PerseveranceServiceReference.ISynchronizationService")]
    public interface ISynchronizationService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISynchronizationService/Fetch", ReplyAction="http://tempuri.org/ISynchronizationService/FetchResponse")]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Synchronization))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SynchronizationProcessor))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SynchronizationDescription))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SynchronizationStatistics))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SynchronizationTimer))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(GameEngineAccess))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(Task))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(MetaMind.Engine.Concepts.SynchronizationSpan))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(double[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(int[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(string[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(System.TimeSpan[]))]
        object Fetch();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISynchronizationService/Fetch", ReplyAction="http://tempuri.org/ISynchronizationService/FetchResponse")]
        System.Threading.Tasks.Task<object> FetchAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISynchronizationService/FetchTask", ReplyAction="http://tempuri.org/ISynchronizationService/FetchTaskResponse")]
        Task FetchTask();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISynchronizationService/FetchTask", ReplyAction="http://tempuri.org/ISynchronizationService/FetchTaskResponse")]
        System.Threading.Tasks.Task<Task> FetchTaskAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ISynchronizationServiceChannel : MetaMind.Acutance.PerseveranceServiceReference.ISynchronizationService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SynchronizationServiceClient : System.ServiceModel.ClientBase<MetaMind.Acutance.PerseveranceServiceReference.ISynchronizationService>, MetaMind.Acutance.PerseveranceServiceReference.ISynchronizationService {
        
        public SynchronizationServiceClient() {
        }
        
        public SynchronizationServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public SynchronizationServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SynchronizationServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SynchronizationServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public object Fetch() {
            return base.Channel.Fetch();
        }
        
        public System.Threading.Tasks.Task<object> FetchAsync() {
            return base.Channel.FetchAsync();
        }
        
        public Task FetchTask() {
            return base.Channel.FetchTask();
        }
        
        public System.Threading.Tasks.Task<Task> FetchTaskAsync() {
            return base.Channel.FetchTaskAsync();
        }
    }
}
