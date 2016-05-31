///////////////////////////////////////////////////////////////
// Service.cs - WCF Service in Self Hosted Configuration     //
//                                                           //
// Jim Fawcett, CSE775 - Distributed Objects, Spring 2008    //
///////////////////////////////////////////////////////////////
/*
 * Based on Microsoft WCF Samples
 * http://msdn2.microsoft.com/en-us/library/ms751514.aspx
 */

using System;
using System.Configuration;
using System.ServiceModel;

namespace Calculator
{
  // service contract.

  [ServiceContract(Namespace="http://Calculator")]
  public interface ICalculator
  {
    [OperationContract]
    double Add(double n1, double n2);
    [OperationContract]
    double Subtract(double n1, double n2);
    [OperationContract]
    double Multiply(double n1, double n2);
    [OperationContract]
    double Divide(double n1, double n2);
  }

  // service implementation

  [ServiceBehavior]
  public class CalculatorService : ICalculator
  {
    public double Add(double n1, double n2)
    {
      Console.Write("\n  Add Called");
      return (n1 + n2);
    }

    public double Subtract(double n1, double n2)
    {
      Console.Write("\n  Subtract Called");
      return (n1 - n2);
    }

    public double Multiply(double n1, double n2)
    {
      Console.Write("\n  Multiply Called");
      return (n1 * n2);
    }

    public double Divide(double n1, double n2)
    {
      Console.Write("\n  Divide Called");
      return (n1 / n2);
    }


    // Host the service within this EXE console application.
    public static void Main()
    {
      // Create a ServiceHost for the CalculatorService type.
      using (ServiceHost serviceHost = new ServiceHost(typeof(CalculatorService)))
      {
        // Open the ServiceHost to create listeners and start listening for messages.
              
        serviceHost.Open();

        // The service is now available

        Console.Write("\n  Calculator Service started");
        Console.Write("\n ============================\n");
        Console.Write("\n  Press Enter to terminate service:\n");
        Console.Read();
        Console.Write("\n");
      }
    }
  }
}
