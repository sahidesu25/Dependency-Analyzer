///////////////////////////////////////////////////////////////////////
// Client2.cs   - This package acts as a client and requests the     //
//                 server                                            //
//                                                                   //
// ver 1.0                                                           //
// Language:    C#, 2013, .Net Framework 4.5                         //
// Platform:    Lenovo Y40, Win8.1                                   //
// Application:  MT2Q2-Client.cs - Project #4 Service Client prototype//
// Author:      Jim Fawcett, CST 4-187, Syracuse University          //
//              (315) 443-3948, jfawcett@twcny.rr.com                //
//Modified by: Sahithi Desu    sldesu@syr.edu                        //
///////////////////////////////////////////////////////////////////////
/*
 * Module Operations:
 * ------------------
 * This module defines the following class:
 *   MessageClient  -  This  creates Channels and opens host for communication 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading;
using System.Xml.Linq;

namespace CodeAnalysis
{

    
    public class ListofProjects 
    {
        public string projectName { get; set; }
        public static string XMLList { get; set; }

    }


    public class MessageClient
    {
        static int check_open_host = 0;
        public static bool flag = false;
        static object locker_ = new object();
        
        static int z = 1;
        static List<string> projList = new List<string>();

        public static List<string> GetProjectList()   // get project list
        {
            List<String> pjtlist = new List<String>();
            pjtlist = projList;
            return pjtlist;
        }
        public static string GetProjectXML() //gets projects list in xml
        {
          
            string s = ListofProjects.XMLList;

            return s;
        }
        static public IMessageService CreateClientChannel(string url)
        {
            BasicHttpBinding binding = new BasicHttpBinding();
            EndpointAddress address = new EndpointAddress(url);
            ChannelFactory<IMessageService> factory =
              new ChannelFactory<IMessageService>(binding, address);
            return factory.CreateChannel();
        }
        static ServiceHost CreateServiceChannel(string url)
        {
            BasicHttpBinding binding = new BasicHttpBinding();
            Uri baseAddress = new Uri(url);
            Type service = typeof(MessageService);
            ServiceHost host = new ServiceHost(service, baseAddress);
            host.AddServiceEndpoint(typeof(IMessageService), binding, baseAddress);
            return host;
        }

        public void Connection(string remoteAddr, string commandtype)
        {
            ServiceHost host = CreateServiceChannel("http://localhost:7003/MessageService");
            if (check_open_host == 0)
            {
                host.Open();
                check_open_host = 1;
            }

            IMessageService proxy = CreateClientChannel("http://localhost:" + remoteAddr + "/MessageService");
            ServiceMessage message = new ServiceMessage();
            message.src = new Uri("http://localhost:7003/MessageService");
            message.dst = new Uri("http://localhost:" + remoteAddr + "/MessageService");
            message.body = "Hello";
            if (commandtype == "ProjectList")
            {
                message.cmd = ServiceMessage.Command.ProjectList;

                proxy.PostMessage(message);
            }
           }


        public void GetAnalysis(ServiceMessage message)  //retrieving xml document from messagebody and queries it.
        {
            lock (locker_)
            {
                z = z + 1;

                if (message.cmd.ToString() == ("ProjectList"))
                {
                    ListofProjects p = new ListofProjects();
                    XDocument doc = XDocument.Parse(message.body);
                    ListofProjects.XMLList = message.body;
                    var q3 = from e in
                                 doc.Elements("ProjectListXML").Elements("ProjectName")
                             select e;

                    int numFuncs = q3.Count();
                    for (int i = 0; i < numFuncs; ++i)
                    {
                        projList.Add(q3.ElementAt(i).Value);
                    }
                }

            }
        }
#if(IF_CLIENT) 
        static void Main(string[] args)
        {
            try
            {
                ServiceHost host = CreateServiceChannel("http://localhost:7003/MessageService");
                host.Open();
            }
            catch(Exception e)
            {
                Console.WriteLine("the error message", e);
                Console.WriteLine("Start the server, so that there will be an endpoint to listen ");
            }
            try
            {
                IMessageService proxy = CreateClientChannel("http://localhost:7002/MessageService");
                ServiceMessage msg = new ServiceMessage();
                msg.cmd = ServiceMessage.Command.ProjectList;
                msg.src = new Uri("http://localhost:7003/MessageService");
                msg.dst = new Uri("http://localhost:7002/MessageService");
                msg.body = "Hello";
                proxy.PostMessage(msg);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.Write("\n  press key to terminate service");
            Console.ReadKey();
            Console.Write("\n");
        }

#endif
    }
}
