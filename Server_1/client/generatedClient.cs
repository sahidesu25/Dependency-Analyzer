﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.42
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace Calculator
{
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://Calculator", ConfigurationName="Calculator.ICalculator")]
    public interface ICalculator
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Calculator/ICalculator/Add", ReplyAction="http://Calculator/ICalculator/AddResponse")]
        double Add(double n1, double n2);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Calculator/ICalculator/Subtract", ReplyAction="http://Calculator/ICalculator/SubtractResponse")]
        double Subtract(double n1, double n2);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Calculator/ICalculator/Multiply", ReplyAction="http://Calculator/ICalculator/MultiplyResponse")]
        double Multiply(double n1, double n2);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Calculator/ICalculator/Divide", ReplyAction="http://Calculator/ICalculator/DivideResponse")]
        double Divide(double n1, double n2);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public interface ICalculatorChannel : Calculator.ICalculator, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public partial class CalculatorClient : System.ServiceModel.ClientBase<Calculator.ICalculator>, Calculator.ICalculator
    {
        
        public CalculatorClient()
        {
        }
        
        public CalculatorClient(string endpointConfigurationName) : 
                base(endpointConfigurationName)
        {
        }
        
        public CalculatorClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress)
        {
        }
        
        public CalculatorClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress)
        {
        }
        
        public CalculatorClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        public double Add(double n1, double n2)
        {
            return base.Channel.Add(n1, n2);
        }
        
        public double Subtract(double n1, double n2)
        {
            return base.Channel.Subtract(n1, n2);
        }
        
        public double Multiply(double n1, double n2)
        {
            return base.Channel.Multiply(n1, n2);
        }
        
        public double Divide(double n1, double n2)
        {
            return base.Channel.Divide(n1, n2);
        }
    }
}
