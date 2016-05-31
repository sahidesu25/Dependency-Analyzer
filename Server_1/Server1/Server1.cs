///////////////////////////////////////////////////////////////////////
// Server1.cs   - This package acts as a server and requests the     //
//                 client                                            //
//                                                                   //
// ver 1.0                                                           //
// Language:    C#, 2013, .Net Framework 4.5                         //
// Platform:    Lenovo Y40, Win8.1                                   //
// Application:  MT2Q2-Client.cs - Project #4 Service Client prototype//
// Author:      Jim Fawcett, CST 4-187, Syracuse University          //
//              (315) 443-3948, jfawcett@twcny.rr.com                //
//Modified by:  Sahithi Desu    sldesu@syr.edu                        //
///////////////////////////////////////////////////////////////////////
/*
 * Module Operations:
 * ------------------
 * This module defines the following class:
 *   Server  -  This  creates Channels and opens host for communication with client
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Xml.Serialization;
using System.IO;
using System.Threading;

namespace CodeAnalysis
{
    class Server
    {
        
        static BlockingQueue<SvcMsg> BlockingQueue = null;
        

        static object locker_ = new object();
       
        static int i = 0;
      

        Server() // creates an instance of blocking queue
        {
            if(BlockingQueue==null)
            {
                
                BlockingQueue = new BlockingQueue<SvcMsg>();
                
            }
        }

        public static void getClientMessage(SvcMsg msg1) // receives message from client 
        {
            IMessageService proxy = CreateClientChannel(msg1.src.ToString());
            List<string> projects = new List<string>();
            SvcMsg msg = new SvcMsg();
            msg.src = new Uri("http://localhost:7001/MessageService");
            msg.dst = msg1.src;
            msg.cmd = SvcMsg.Command.ProjectList;
            msg.body = " ";
            proxy.PostMessage(msg);

            msg1.ShowMessage();
        }
      
       
      
        public static void Message_From_Queue(SvcMsg msg) //enqueues message into server queue and process files for project list
        {

            BlockingQueue.enQ(msg);
            lock(locker_)
            {
                SvcMsg msg1 = BlockingQueue.deQ();

                if (msg1.cmd.ToString() == "ProjectList")
                {
                    
                    Executive exe = new Executive();
                    string list = Executive.processFilesForProjectList();


                    Console.WriteLine("Sending to Client");
                    IMessageService proxy = CreateClientChannel(msg1.src.ToString());
                    SvcMsg msgSend = new SvcMsg();
                    msgSend.src = msg1.dst;
                    msgSend.dst = msg1.src;
                    msgSend.cmd = SvcMsg.Command.ProjectList;
                    msgSend.body = list;
                    proxy.PostMessage(msgSend);
                }

            }
        }

        
        public static IMessageService CreateClientChannel(string url)
        {
            BasicHttpBinding binding = new BasicHttpBinding();
            EndpointAddress address = new EndpointAddress(url);
            ChannelFactory<IMessageService> factory =
              new ChannelFactory<IMessageService>(binding, address);
            return factory.CreateChannel();
        }
        public static ServiceHost CreateServiceChannel(string url)
        {
            BasicHttpBinding binding = new BasicHttpBinding();
            Uri baseAddress = new Uri(url);
            Type service = typeof(MessageService);
            ServiceHost host = new ServiceHost(service, baseAddress);
            host.AddServiceEndpoint(typeof(IMessageService), binding, baseAddress);
            return host;
        }

       
        static void Main(string[] args)
        {

            ServiceHost host = CreateServiceChannel("http://localhost:7001/MessageService");
            host.Open();
            Server s1 = new Server();
            Console.Write("\n  press key to terminate service\n");
            Console.ReadKey();
            Console.Write("\n");
            host.Close();
        }
    }
}
