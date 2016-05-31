///////////////////////////////////////////////////////////////////////
// MT1Q2.cs - Type-safe TypeTable                                    //
//                                                                   //
// Jim Fawcett, CSE681 - Software Modeling and Analysis, Fall 2014   //
///////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CodeAnalysis
{
    public class TypeElem
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string Servername { get; set; }
        public string Namespace { get; set; }
        public string Filename { get; set; }        // passing the file name to print file by file data

    }
    public class TypeTable
    {
        object locker_ = new object();
        public Dictionary<string, TypeElem> types { get; set; }

        public TypeTable()
        {
            types = new Dictionary<string, TypeElem>();
        }
        //----< f will use lambda capture to acquire its arguments >-------

        T lockit<T>(Func<T> f)
        {
            lock (locker_) { return f.Invoke(); }
        }
        public bool add(string Type, string Name, string Namespace, string Filename, string ServerName)
        {
            TypeElem elem = new TypeElem();
            elem.Type = Type;
            elem.Name = Name;
            elem.Filename = Filename;
            elem.Servername = ServerName;
            elem.Namespace = Namespace;
            return lockit<bool>(() =>
            {
                if (types.Keys.Contains(Type) && types[Type].Namespace == Namespace)
                    return false;
                types[Type] = elem;
                return true;
            });
        }
        public bool remove(string Type)
        {
            return lockit<bool>(() => { return types.Remove(Type); });
        }
        string namespce(string Type)
        {
            return lockit<string>(() =>
            {
                return (types.Keys.Contains(Type)) ? types[Type].Namespace : "";
            });
        }
        public string filename(string Type)
        {
            return lockit<string>(() =>
            {
                return (types.Keys.Contains(Type)) ? types[Type].Filename : "";
            });
        }
        public bool contains(string Type)
        {
            return lockit<bool>(() => types.Keys.Contains(Type));
        }
    }
    class Q2
    {
        //----< partial test of type-safe TypeTable >----------------------

#if(TEST_TYPE)
      static void Main(string[] args)
    {
      TypeTable tt = new TypeTable();
      tt.add("Type1", "myNamespace", " someFile.cs");
      tt.add("Type2", "myNamespace", "someOtherFile.cs");
      foreach(string key in tt.types.Keys)
      {
        Console.Write("\n  {0, -15} {1, -20} {2, 20}", key, tt.types[key].Namespace, tt.types[key].Filename);
      }
      Console.Write("\n\n");
    }
#endif
    }
}

