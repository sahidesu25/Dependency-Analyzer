///////////////////////////////////////////////////////////////////////
// Analyzer.cs - Manages Code Analysis                               //
// ver 1.0                                                           //
// Language:    C#, 2013, .Net Framework 4.5                         //
// Platform:    Lenovo Y40, Win8.1                                   //
// Application: Code Analyzer for CSE681, Project #2, Fall 2014      //
// Author:      Dhaval N Dholakiya, Syracuse University              //
//              (315) 447-7644, ddholaki@syr.edu                     //
///////////////////////////////////////////////////////////////////////
/*
 * Module Operations:
 * ------------------
 * This module defines the following class:
 *   Analyzer  - It manages code Analysis
 */
/* Required Files:
 *   Parser.cs, IRulesAndActions.cs, RulesAndActions.cs, Parser.cs, Semi.cs, Toker.cs
 *   

 *   
 * Maintenance History:
 * --------------------
 * - first release
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;
namespace CodeAnalysis
{
    /// ///////////////////////////////////
    //Analyses the input files
    public class Analyzer
    {
        //----------------<gets files for test stub>------------------------
        private static string[] getFiles(string path, List<string> patterns)
        {
            List<string> files = new List<string>();
            foreach (string pattern in patterns)
            {
                string[] newFiles = Directory.GetFiles(path, pattern);
                for (int i = 0; i < newFiles.Length; ++i)
                    newFiles[i] = Path.GetFullPath(newFiles[i]);

                files.AddRange(newFiles);
            }
            return files.ToArray();
        }

        //----------<parse1 starts here, it identifies all the types>-----------------
        public string doAnalysis(string[] files, string typeTable)
        {
            OutputRepository.output_.Clear();

            foreach (object file in files)
            {
                string filename = Convert.ToString(file);

                CSsemi.CSemiExp semi = new CSsemi.CSemiExp();
                semi.displayNewLines = false;
                if (!semi.open(file as string))
                {
                    Console.Write("\n  Can't open {0}\n\n", file);
                    return null;
                }


                BuildCodeAnalyzer builder = new BuildCodeAnalyzer(semi);
                Parser parser = builder.build();

                try
                {
                    while (semi.getSemi())
                        parser.parse(semi, filename);
                }
                catch (Exception ex)
                {
                    Console.Write("\n\n  {0}\n", ex.Message);
                }
                Repository rep = Repository.getInstance();
                List<Elem> table = rep.locations;           //storing the repository data into a List
                if (typeTable != null)
                {


                }
                else
                {

                }

                semi.close();
            }
            XDocument doc = XDocument.Parse(typeTable);
            Console.Write("\n\n");
            var entries = from e in
                              doc.Elements("TYPETABLE").Elements("Entries")
                          select e;
            foreach (var entry in entries)
            {
                var q3 = from e in entry.Elements() select e;

                int numFuncs = q3.Count() / 4;
                for (int i = 0; i < numFuncs; ++i)
                {
                    Elem elem = new Elem();
                    elem.fileName = (q3.ElementAt(4 * i).Value);    // name
                    elem.namespaces = (q3.ElementAt(4 * i + 1).Value);
                    elem.type = (q3.ElementAt(4 * i + 2).Value);
                    elem.name = (q3.ElementAt(4 * i + 3).Value);
                    OutputRepository.output_.Add(elem);
                }

            }
            string b = doAnalysisRelationship(files);
            return b;
        }

        public string doAnalysisForRequest(string[] files,string typeTable)
        {

            OutputRepository.output_.Clear();
            foreach (object file in files)
            {
                string filename = Convert.ToString(file);

                CSsemi.CSemiExp semi = new CSsemi.CSemiExp();
                semi.displayNewLines = false;
                if (!semi.open(file as string))
                {
                    Console.Write("\n  Can't open {0}\n\n", file);
                    return null;
                }


                BuildCodeAnalyzer builder = new BuildCodeAnalyzer(semi);
                Parser parser = builder.build();

                try
                {
                    while (semi.getSemi())
                        parser.parse(semi, filename);
                }
                catch (Exception ex)
                {
                    Console.Write("\n\n  {0}\n", ex.Message);
                }
                Repository rep = Repository.getInstance();
                List<Elem> table = rep.locations;           //storing the repository data into a List

                semi.close();
            }
            List<Elem> typetable = OutputRepository.output_;

            XDocument xml = new XDocument();
            xml.Declaration = new XDeclaration("1.0", "utf-8", "yes");
            XElement root = new XElement("TYPETABLE");
            //XElement entries = new XElement("Entries");

            //xml.Add(root);
            // XElement file = null;
            foreach (Elem s in typetable)
            {
                XElement entries = new XElement("Entries");
                entries.Add(new XElement("Filename", s.fileName));
                entries.Add(new XElement("NamespaceName", s.namespaces));
                entries.Add(new XElement("Type", s.type));
                entries.Add(new XElement("TypeName", s.name));
                root.Add(entries);
              
            }
            xml.Add(root);
            return xml.ToString();

        }

        //---------<parse2 starts here, it identifies relationships betn all the types>------------
        public string doAnalysisRelationship(string[] files)
        {
            RelationshipRepository.relationship_.Clear();
            foreach (object file in files)
            {
                string filename = Convert.ToString(file);


                CSsemi.CSemiExp semi = new CSsemi.CSemiExp();
                semi.displayNewLines = false;
                if (!semi.open(file as string))
                {
                    Console.Write("\n  Can't open {0}\n\n", file);
                    return null;
                }

                BuildCodeAnalyzerRelation builderForRelationship = new BuildCodeAnalyzerRelation(semi);
                Parser parser = builderForRelationship.build();

                try
                {
                    while (semi.getSemi())
                        parser.parse(semi, filename);
                }
                catch (Exception ex)
                {
                    Console.Write("\n\n  {0}\n", ex.Message);
                }

                Repository rep = Repository.getInstance();
                List<Elem> table = rep.locations;

                semi.close();
            }
            List<ElemRelation> output = RelationshipRepository.relationship_;

            XDocument xml = new XDocument();
            xml.Declaration = new XDeclaration("1.0", "utf-8", "yes");
            XElement root = new XElement("Relationships");

            foreach (ElemRelation s in output)
            {
                XElement entries = new XElement("TypeDependency");
                entries.Add(new XElement("fromClass", s.fromClass));
                entries.Add(new XElement("fromClassFilename", s.fromClassFilename));
                entries.Add(new XElement("fromClassNamespace", s.fromClassNamespace));
                entries.Add(new XElement("toClass", s.toClass));
                entries.Add(new XElement("toClassFilename", s.toClassFilename));
                entries.Add(new XElement("toClassNamespace", s.toClassNamespace));
                root.Add(entries);
            }
            foreach (ElemRelation s in output)
            {
                if (s.fromClassFilename != s.toClassFilename)
                {
                    XElement entries = new XElement("PackageDependency");

                    entries.Add(new XElement("fromClassFilename", s.fromClassFilename));
                    entries.Add(new XElement("fromClassNamespace", s.fromClassNamespace));
                    entries.Add(new XElement("toClassFilename", s.toClassFilename));
                    entries.Add(new XElement("toClassNamespace", s.toClassNamespace));
                    root.Add(entries);
                }
            }
            xml.Add(root);
            return xml.ToString();

        }

        // ----------------------<Test Stub for Analyzer>---------------------
#if(TEST_ANALYZER)

        static void Main(string[] args)
        {
            string path = "../../";
            string[] arg = { "../../", "*.cs" };
            List<string> patterns = new List<string>();
            patterns.Add("*.cs");
            List<string> options = new List<string>();
            string[] files = getFiles(path, patterns);
            Analyzer analyzer = new Analyzer();
            string typeTable=null;
            analyzer.doAnalysis(files,typeTable);
            analyzer.doAnalysisRelationship(files);
            List<Elem> outputList = OutputRepository.output_;
            foreach (object f in files)
            {
                string filename = Convert.ToString(f);
                Console.Write("      Type                     Name           Begin          End ");
                Console.Write("\n-----------------------------------------------------------------");
                foreach (Elem e in outputList)
                {
                    //Console.Write("\n  {0,10}, {1,25}, {2,5}, {3,5}", e.type, e.name, e.begin, e.end);
                }
                Console.WriteLine();
                Console.Write("\n\n  That's all folks!\n\n");

            }
            List<ElemRelation> table = RelationshipRepository.relationship_;
            foreach (object f in files)
            {
                string filename = Convert.ToString(f);
                Console.Write("         Relation                 From                        To");
                Console.Write("\n-----------------------------------------------------------------");

                foreach (ElemRelation e in table)
                {
                    Console.Write("\n  {0,10}, {1,25}, {2,5}", e.relationType, e.fromClass, e.toClass);
                }
                Console.WriteLine();
                Console.Write("\n\n \n\n");
            }
        }
#endif

    }
}

