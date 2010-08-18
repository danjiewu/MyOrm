﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.1
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

// 
// 此源代码是由 Microsoft.VSDesigner 4.0.30319.1 版自动生成。
// 
#pragma warning disable 1591

namespace Northwind.Protocal {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.ComponentModel;
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="RemoteServiceSoap", Namespace="http://northwind.sample/")]
    public partial class RemoteService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback TestOperationCompleted;
        
        private System.Threading.SendOrPostCallback InvokeServiceOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public RemoteService() {
            this.Url = global::Northwind.Protocal.Properties.Settings.Default.Northwind_RemoteAPI_localhost_RemoteService;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event TestCompletedEventHandler TestCompleted;
        
        /// <remarks/>
        public event InvokeServiceCompletedEventHandler InvokeServiceCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://northwind.sample/Test", RequestNamespace="http://northwind.sample/", ResponseNamespace="http://northwind.sample/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public object Test() {
            object[] results = this.Invoke("Test", new object[0]);
            return ((object)(results[0]));
        }
        
        /// <remarks/>
        public void TestAsync() {
            this.TestAsync(null);
        }
        
        /// <remarks/>
        public void TestAsync(object userState) {
            if ((this.TestOperationCompleted == null)) {
                this.TestOperationCompleted = new System.Threading.SendOrPostCallback(this.OnTestOperationCompleted);
            }
            this.InvokeAsync("Test", new object[0], this.TestOperationCompleted, userState);
        }
        
        private void OnTestOperationCompleted(object arg) {
            if ((this.TestCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.TestCompleted(this, new TestCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://northwind.sample/InvokeService", RequestNamespace="http://northwind.sample/", ResponseNamespace="http://northwind.sample/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public object InvokeService(string service, string method, object[] args) {
            object[] results = this.Invoke("InvokeService", new object[] {
                        service,
                        method,
                        args});
            return ((object)(results[0]));
        }
        
        /// <remarks/>
        public void InvokeServiceAsync(string service, string method, object[] args) {
            this.InvokeServiceAsync(service, method, args, null);
        }
        
        /// <remarks/>
        public void InvokeServiceAsync(string service, string method, object[] args, object userState) {
            if ((this.InvokeServiceOperationCompleted == null)) {
                this.InvokeServiceOperationCompleted = new System.Threading.SendOrPostCallback(this.OnInvokeServiceOperationCompleted);
            }
            this.InvokeAsync("InvokeService", new object[] {
                        service,
                        method,
                        args}, this.InvokeServiceOperationCompleted, userState);
        }
        
        private void OnInvokeServiceOperationCompleted(object arg) {
            if ((this.InvokeServiceCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.InvokeServiceCompleted(this, new InvokeServiceCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void TestCompletedEventHandler(object sender, TestCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class TestCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal TestCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public object Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((object)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void InvokeServiceCompletedEventHandler(object sender, InvokeServiceCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class InvokeServiceCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal InvokeServiceCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public object Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((object)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591