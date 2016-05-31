///////////////////////////////////////////////////////////////////////
// ServiceLibrary.cs - This package acts as a client and requests the//
//                 server                                            //
//                                                                   //
// ver 1.0                                                           //
// Language:    C#, 2013, .Net Framework 4.5                         //
// Platform:    Lenovo Y40, Win8.1                                   //
// Application: MT2Q2-ServiceLibrary.cs - Project #4 Service prototype//
// Author:      Jim Fawcett, CST 4-187, Syracuse University          //
//              (315) 443-3948, jfawcett@twcny.rr.com  
// Modified by : Sahithi Desu sldesu@syr.edu
///////////////////////////////////////////////////////////////////////
/*
 * Module Operations:
 * ------------------
 * This module defines the following class:
 *    ServiceMessage: Provides Message Contract to the Server
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Runtime.Serialization;
using System.ServiceModel.Web;

namespace CodeAnalysis
{
    [DataContract(Namespace = "CodeAnalysis")]
    public class SvcMsg
    {
        public enum Command { ProjectList}; 
        [DataMember]
        public Command cmd;
        [DataMember]
        public Uri src;
        [DataMember]
        public Uri dst;
        [DataMember]
        public string body;  
        [DataMember]
        public string bodyExt; 

        public void ShowMessage()
        {
            Console.Write("\n  Received Message:");
            Console.Write("\n    src = {0}\n    dst = {1}", src.ToString(), dst.ToString());
            Console.Write("\n    cmd = {0}", cmd.ToString());
            Console.Write("\n    body:\n{0}", body);
        }
    }
    public class SvcMsgServer
    {
        
        public enum Command { ProjectList };
        [DataMember]
        public Command cmd;
        [DataMember]
        public Uri src;
        [DataMember]
        public Uri dst;
        [DataMember]
        public string body;  
        [DataMember]
        public string bodyExt;  

        public void ShowMessage()
        {
            Console.Write("\n  Received Message:");
            Console.Write("\n    src = {0}\n    dst = {1}", src.ToString(), dst.ToString());
            Console.Write("\n    cmd = {0}", cmd.ToString());
            Console.Write("\n    body:\n{0}", body);
        }
    }
    [ServiceContract(Namespace = "CodeAnalysis")]
    public interface IMessageService
    {
        [OperationContract(IsOneWay = true)]
        void PostMessage(SvcMsg msg);
        

    }

    [ServiceBehavior(Namespace = "CodeAnalysis")]
    public class MessageService : IMessageService
    {
        public void PostMessage(SvcMsg msg)
        {
            
            Server.Message_From_Queue(msg);
            msg.ShowMessage();
        }
        
    }
}
