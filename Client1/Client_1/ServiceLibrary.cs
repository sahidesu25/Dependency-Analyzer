﻿///////////////////////////////////////////////////////////////////////
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
 *    ServiceMessage: Provides Message Contract to the Client
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Runtime.Serialization;
using System.ServiceModel.Web;
using System.Xml.Linq;

namespace CodeAnalysis
{
    [DataContract(Namespace = "CodeAnalysis")]
    public class ServiceMessage//////
    {
        public enum Command {ProjectList };  // Needs bigger set of enums
        [DataMember]
        public Command cmd;
        [DataMember]
        public Uri src;
        [DataMember]
        public Uri dst;
        [DataMember]
        public string body;  // Used to send XML for structured data
        [DataMember]
        public string bodyExt;  // Used to send XML for structured data

        
    }
    [ServiceContract(Namespace = "CodeAnalysis")]
    public interface IMessageService
    {
        [OperationContract(IsOneWay = true)]
        void PostMessage(ServiceMessage msg);

     }

    [ServiceBehavior(Namespace = "CodeAnalysis")]
    public class MessageService : IMessageService
    {
        public void PostMessage(ServiceMessage msg)
        {
            MessageClient c= new MessageClient();
            c.GetAnalysis(msg);
        }
        

    }
}
