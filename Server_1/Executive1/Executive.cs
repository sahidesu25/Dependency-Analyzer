///////////////////////////////////////////////////////////////////////
// Executive.cs - Executive is responsible for the entire application//
//                It controls the entire flow                        //
// ver 1.0                                                           //
// Language:    C#, 2013, .Net Framework 4.5                         //
// Platform:    Lenovo Y40, Win8.1                                   //
// Application: Code Analyzer for CSE681, Project #2, Fall 2014      //
// Author:      Dhaval N Dholakiya, Syracuse University              //
//              (315) 447-7644, ddholaki@syr.edu                     //
// Modified by : Sahithi Desu, sldesu@syr.edu                        //
///////////////////////////////////////////////////////////////////////
/*
 * Module Operations:
 * ------------------
 * This module defines the following class:
 *   CommandLineParsing  - It the package provides output to the User
 */
/* Required Files:
 *   IRulesAndActions.cs, RulesAndActions.cs, Parser.cs, Semi.cs, Toker.cs, CommandLineParse.cs, FileMgr.cs, Display.cs, XMLOutput.cs
 *   
 *   
 * Maintenance History:
 * --------------------
 * - ver 1.0: Developed by Dhaval N Dholakiya
 * - ver 1.2: Added processFilesForProjectList and Process_Files
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace CodeAnalysis
{
    
    public class Executive
    {
        public static string processFilesForProjectList()  // takes path and pattern and fetches all the projects into list
        {
            string pathS = "../../../";
            List<string> patternsS = new List<string>();
            patternsS.Add("*.csproj");
            string list=Executive.Process_Files(pathS,patternsS);
            
            return list;
        }
        //-------------------<prcocesses the files>-------------------------
        public static string Process_Files(string path, List<string> patterns)// it retrieves the project list using file manager and creates xdocument 
        {


            FileMgr fileManager = new FileMgr();


            string[] files = fileManager.getFilesForProjectList(path, patterns);
            List<string> f = new List<string>();
            foreach (object file in files)
            {
                string filename = Convert.ToString(file);

                f.Add(filename);
            }
            XDocument xml = new XDocument();
            xml.Declaration = new XDeclaration("1.0", "utf-8", "yes");
            XElement root = new XElement("ProjectListXML");
            xml.Add(root);
           
            foreach (object s in f)
            {
                XElement projList = new XElement("ProjectName", s);
                root.Add(projList);
            }

           return xml.ToString();

       }




#if(IF_EXECUTIVE)  
        public static void Main(string[] args)
        {
            Console.WriteLine("executive");
           
            string path = "../../";
            List<string> patterns = new List<string>();
            List<string> options = new List<string>(); 
            options.Add("/R");
            options.Add("/S");
            patterns.Add("*.cs");
            patterns.Add("*.txt");
           if (!Directory.Exists(path))
            {
                Console.WriteLine("Please Enter Correct Path");
                return;
            }
             string Xml = Executive.Process_Files(path, patterns);
            if (Xml.Length == 0)
            {
                Console.WriteLine("Projects not found");
                return;
            }
            Console.WriteLine(Xml);
        }
            
#endif
    }
}


     