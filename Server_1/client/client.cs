///////////////////////////////////////////////////////////////
// client.cs - WCF Calculator Service client                 //
//                                                           //
// Jim Fawcett, CSE775 - Distributed Objects, Spring 2008    //
///////////////////////////////////////////////////////////////
/*
 * Based on Microsoft WCF Samples
 * http://msdn2.microsoft.com/en-us/library/ms751514.aspx
 * 
 */

using System;
using System.ServiceModel;

namespace Calculator
{
  // The service contract is defined in generatedClient.cs,
  // generated from the service by svcutil tool.

  class Client
  {
    static void Main()
    {
      Console.Write("\n  Calculator Service Client");
      Console.Write("\n ===========================\n");

      CalculatorClient client = null;
      try
      {
        client = new CalculatorClient();

        // Call Add service operation.
        double value1 = 100.00D;
        double value2 = 15.99D;
        double result = client.Add(value1, value2);
        Console.Write("\n  Add({0},{1}) = {2}", value1, value2, result);

        // Call Subtract service operation.
        value1 = 145.00D;
        value2 = 76.54D;
        result = client.Subtract(value1, value2);
        Console.Write("\n  Subtract({0},{1}) = {2}", value1, value2, result);

        // Call Multiply service operation.
        value1 = 9.00D;
        value2 = 81.25D;
        result = client.Multiply(value1, value2);
        Console.Write("\n  Multiply({0},{1}) = {2}", value1, value2, result);

        // Call Divide service operation.
        value1 = 22.00D;
        value2 = 7.00D;
        result = client.Divide(value1, value2);
        Console.Write("\n  Divide({0},{1}) = {2}", value1, value2, result);

        // Call Divide service operation with zero denominator.
        value1 = 22.00D;
        value2 = 0.00D;
        result = client.Divide(value1, value2);
        Console.Write("\n  Divide({0},{1}) = {2}", value1, value2, result);


        Console.WriteLine();
        Console.Write("\n  Client - endpoint:  " + client.Endpoint.Address);
        Console.Write("\n  Client - binding:  " + client.Endpoint.Binding.Name);
        Console.Write("\n  Client - contract: " + client.Endpoint.Contract.Name);

        Console.WriteLine();
        IClientChannel channel = client.InnerChannel;
        Console.Write("\n  Client channel - state: " + channel.State);
        Console.Write("\n  Client channel - session identifier:\n    " + channel.SessionId);

        // Closing client closes connection and cleans up resources

        client.Close();
      }
      catch (Exception ex)
      {
        Console.Write("\n  {0}\n\n", ex.Message);
      }
      Console.Write("\n\n  Press ENTER to terminate client");
      Console.Read();
    }
  }
}
