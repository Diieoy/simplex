﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfApp.WcfSeatServiceReference {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="SeatDTO", Namespace="http://schemas.datacontract.org/2004/07/WcfServiceLibrary.DTO")]
    [System.SerializableAttribute()]
    public partial class SeatDTO : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int AreaIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int NumberField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int RowField;
        
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
        public int AreaId {
            get {
                return this.AreaIdField;
            }
            set {
                if ((this.AreaIdField.Equals(value) != true)) {
                    this.AreaIdField = value;
                    this.RaisePropertyChanged("AreaId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Number {
            get {
                return this.NumberField;
            }
            set {
                if ((this.NumberField.Equals(value) != true)) {
                    this.NumberField = value;
                    this.RaisePropertyChanged("Number");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Row {
            get {
                return this.RowField;
            }
            set {
                if ((this.RowField.Equals(value) != true)) {
                    this.RowField = value;
                    this.RaisePropertyChanged("Row");
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="InvalidEventException", Namespace="http://schemas.datacontract.org/2004/07/WcfServiceLibrary.CustomExceptions")]
    [System.SerializableAttribute()]
    public partial class InvalidEventException : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CustomErrorField;
        
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
        public string CustomError {
            get {
                return this.CustomErrorField;
            }
            set {
                if ((object.ReferenceEquals(this.CustomErrorField, value) != true)) {
                    this.CustomErrorField = value;
                    this.RaisePropertyChanged("CustomError");
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CanNotCreateEventSeatException", Namespace="http://schemas.datacontract.org/2004/07/WcfServiceLibrary.CustomExceptions")]
    [System.SerializableAttribute()]
    public partial class CanNotCreateEventSeatException : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CustomErrorField;
        
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
        public string CustomError {
            get {
                return this.CustomErrorField;
            }
            set {
                if ((object.ReferenceEquals(this.CustomErrorField, value) != true)) {
                    this.CustomErrorField = value;
                    this.RaisePropertyChanged("CustomError");
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CanNotDeleteEventException", Namespace="http://schemas.datacontract.org/2004/07/WcfServiceLibrary.CustomExceptions")]
    [System.SerializableAttribute()]
    public partial class CanNotDeleteEventException : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CustomErrorField;
        
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
        public string CustomError {
            get {
                return this.CustomErrorField;
            }
            set {
                if ((object.ReferenceEquals(this.CustomErrorField, value) != true)) {
                    this.CustomErrorField = value;
                    this.RaisePropertyChanged("CustomError");
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="WcfSeatServiceReference.ISeatServicece")]
    public interface ISeatServicece {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOf_SeatDTO/Create", ReplyAction="http://tempuri.org/IServiceOf_SeatDTO/CreateResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(WpfApp.WcfSeatServiceReference.InvalidEventException), Action="http://tempuri.org/IServiceOf_SeatDTO/CreateInvalidEventExceptionFault", Name="InvalidEventException", Namespace="http://schemas.datacontract.org/2004/07/WcfServiceLibrary.CustomExceptions")]
        void Create(WpfApp.WcfSeatServiceReference.SeatDTO obj);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOf_SeatDTO/Create", ReplyAction="http://tempuri.org/IServiceOf_SeatDTO/CreateResponse")]
        System.Threading.Tasks.Task CreateAsync(WpfApp.WcfSeatServiceReference.SeatDTO obj);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOf_SeatDTO/Delete", ReplyAction="http://tempuri.org/IServiceOf_SeatDTO/DeleteResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(WpfApp.WcfSeatServiceReference.CanNotCreateEventSeatException), Action="http://tempuri.org/IServiceOf_SeatDTO/DeleteCanNotCreateEventSeatExceptionFault", Name="CanNotCreateEventSeatException", Namespace="http://schemas.datacontract.org/2004/07/WcfServiceLibrary.CustomExceptions")]
        [System.ServiceModel.FaultContractAttribute(typeof(WpfApp.WcfSeatServiceReference.CanNotDeleteEventException), Action="http://tempuri.org/IServiceOf_SeatDTO/DeleteCanNotDeleteEventExceptionFault", Name="CanNotDeleteEventException", Namespace="http://schemas.datacontract.org/2004/07/WcfServiceLibrary.CustomExceptions")]
        void Delete(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOf_SeatDTO/Delete", ReplyAction="http://tempuri.org/IServiceOf_SeatDTO/DeleteResponse")]
        System.Threading.Tasks.Task DeleteAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOf_SeatDTO/Update", ReplyAction="http://tempuri.org/IServiceOf_SeatDTO/UpdateResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(WpfApp.WcfSeatServiceReference.InvalidEventException), Action="http://tempuri.org/IServiceOf_SeatDTO/UpdateInvalidEventExceptionFault", Name="InvalidEventException", Namespace="http://schemas.datacontract.org/2004/07/WcfServiceLibrary.CustomExceptions")]
        void Update(WpfApp.WcfSeatServiceReference.SeatDTO obj);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOf_SeatDTO/Update", ReplyAction="http://tempuri.org/IServiceOf_SeatDTO/UpdateResponse")]
        System.Threading.Tasks.Task UpdateAsync(WpfApp.WcfSeatServiceReference.SeatDTO obj);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOf_SeatDTO/GetById", ReplyAction="http://tempuri.org/IServiceOf_SeatDTO/GetByIdResponse")]
        WpfApp.WcfSeatServiceReference.SeatDTO GetById(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOf_SeatDTO/GetById", ReplyAction="http://tempuri.org/IServiceOf_SeatDTO/GetByIdResponse")]
        System.Threading.Tasks.Task<WpfApp.WcfSeatServiceReference.SeatDTO> GetByIdAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOf_SeatDTO/GetAll", ReplyAction="http://tempuri.org/IServiceOf_SeatDTO/GetAllResponse")]
        WpfApp.WcfSeatServiceReference.SeatDTO[] GetAll();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOf_SeatDTO/GetAll", ReplyAction="http://tempuri.org/IServiceOf_SeatDTO/GetAllResponse")]
        System.Threading.Tasks.Task<WpfApp.WcfSeatServiceReference.SeatDTO[]> GetAllAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISeatServicece/CreateSeats", ReplyAction="http://tempuri.org/ISeatServicece/CreateSeatsResponse")]
        void CreateSeats(WpfApp.WcfSeatServiceReference.SeatDTO[] seatDTOs);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISeatServicece/CreateSeats", ReplyAction="http://tempuri.org/ISeatServicece/CreateSeatsResponse")]
        System.Threading.Tasks.Task CreateSeatsAsync(WpfApp.WcfSeatServiceReference.SeatDTO[] seatDTOs);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ISeatServiceceChannel : WpfApp.WcfSeatServiceReference.ISeatServicece, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SeatServiceceClient : System.ServiceModel.ClientBase<WpfApp.WcfSeatServiceReference.ISeatServicece>, WpfApp.WcfSeatServiceReference.ISeatServicece {
        
        public SeatServiceceClient() {
        }
        
        public SeatServiceceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public SeatServiceceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SeatServiceceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SeatServiceceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void Create(WpfApp.WcfSeatServiceReference.SeatDTO obj) {
            base.Channel.Create(obj);
        }
        
        public System.Threading.Tasks.Task CreateAsync(WpfApp.WcfSeatServiceReference.SeatDTO obj) {
            return base.Channel.CreateAsync(obj);
        }
        
        public void Delete(int id) {
            base.Channel.Delete(id);
        }
        
        public System.Threading.Tasks.Task DeleteAsync(int id) {
            return base.Channel.DeleteAsync(id);
        }
        
        public void Update(WpfApp.WcfSeatServiceReference.SeatDTO obj) {
            base.Channel.Update(obj);
        }
        
        public System.Threading.Tasks.Task UpdateAsync(WpfApp.WcfSeatServiceReference.SeatDTO obj) {
            return base.Channel.UpdateAsync(obj);
        }
        
        public WpfApp.WcfSeatServiceReference.SeatDTO GetById(int id) {
            return base.Channel.GetById(id);
        }
        
        public System.Threading.Tasks.Task<WpfApp.WcfSeatServiceReference.SeatDTO> GetByIdAsync(int id) {
            return base.Channel.GetByIdAsync(id);
        }
        
        public WpfApp.WcfSeatServiceReference.SeatDTO[] GetAll() {
            return base.Channel.GetAll();
        }
        
        public System.Threading.Tasks.Task<WpfApp.WcfSeatServiceReference.SeatDTO[]> GetAllAsync() {
            return base.Channel.GetAllAsync();
        }
        
        public void CreateSeats(WpfApp.WcfSeatServiceReference.SeatDTO[] seatDTOs) {
            base.Channel.CreateSeats(seatDTOs);
        }
        
        public System.Threading.Tasks.Task CreateSeatsAsync(WpfApp.WcfSeatServiceReference.SeatDTO[] seatDTOs) {
            return base.Channel.CreateSeatsAsync(seatDTOs);
        }
    }
}
