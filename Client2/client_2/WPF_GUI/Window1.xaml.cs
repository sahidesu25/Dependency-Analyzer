///////////////////////////////////////////////////////////////////////
// Window1.xaml.cs - WPF User Interface for WCF Communicator         //
//                                                                   //
// ver2.2                                                           //
// Langage:    C#, 2013, .Net Framework 4.5                          //
// Platform:    Lenovo Y40, Win8.1                                   //
// Application: Dependency Analyser Project 4                        //
// Author:      Jim Fawcett, CST 4-187, Syracuse University          //
//              (315) 443-3948, jfawcett@twcny.rr.com                //
//Modified:   Sahithi Desu sldesu@syr.edu                            //
///////////////////////////////////////////////////////////////////////
/*
 * Maintenance History:
 * ====================
 * ver 2.3 : 19 Nov 11
 * - Created functions to perform actions on button click. 
 * ver .2 : 30 Oct 11
 * - added send thread to keep UI from freezing on slow sends
 * - added more comments to clarify what code is doing
 * ver 2.1 : 16 Oct 11
 * - cosmetic changes, posted to the college server but not
 *   distributed in class
 * ver 2.0 : 06 Nov 08
 * - fixed bug that had local and remote ports swapped
 * - made Receive thread background so it would not keep 
 *   application alive after close button is clicked
 * - now closing sender and receiver channels on window
 *   unload
 * ver 1.0 : 17 Jul 07
 * - first release
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;

namespace CodeAnalysis
{
  public partial class Window1 : Window
  {
     
    
    
    public Window1()
    {
      InitializeComponent();
      Get_projectList.IsEnabled = false;
      Generate_XML_button.IsEnabled = false;
    }


    private void OnClick_Connect1(object sender, RoutedEventArgs e)  // establish connection with server on button click
    {
        try
        {
            MessageClient C = new MessageClient();
           
            string proj = "ProjectList";
            C.Connection("7001", proj);
            Get_projectList.IsEnabled = true;
          
             }
        catch (Exception ex)
        {
            Window temp = new Window();
            StringBuilder msg = new StringBuilder(ex.Message);
            msg.Append("\nport = ");
            msg.Append("7001".ToString());
            temp.Content = msg.ToString();
            temp.Height = 100;
            temp.Width = 500;
            temp.Show();
        }
    }

    private void OnClick_Connect2(object sender, RoutedEventArgs e) // establish connection with server on button click
    { 
        try
        {
            MessageClient C = new MessageClient();
         
            string proj="ProjectList";
            C.Connection("7002", proj);
            Get_projectList.IsEnabled = true;
        }
        catch (Exception ex)
        {
            Window temp = new Window();
            StringBuilder msg = new StringBuilder(ex.Message);
            msg.Append("\nport = ");
            msg.Append("7002".ToString());
            temp.Content = msg.ToString();
            temp.Height = 100;
            temp.Width = 500;
            temp.Show();
        }
    }
    private void OnClick_GenerateXML(object sender, RoutedEventArgs e)
    {
        try
        {
            Analysis_ListBox.Items.Clear();

            string Message = ListofProjects.XMLList;
            Analysis_ListBox.Items.Insert(0, Message);

        }
        catch (Exception ex)
        {
            Window temp = new Window();
            temp.Content = ex.Message;
            temp.Height = 100;
            temp.Width = 500;
        }     
    }
    private void OnClick_GetProjectList(object sender, RoutedEventArgs e) //Generate xml on button click
    {
      try
      {
          Project_listbox.Items.Clear();
          List<string> Message=MessageClient.GetProjectList();
          foreach (string m in Message)
          {
              Project_listbox.Items.Insert(0, m);
          }
          Generate_XML_button.IsEnabled = true; 
 }
      catch (Exception ex)
      {
        Window temp = new Window();
        temp.Content = ex.Message;
        temp.Height = 100;
        temp.Width = 500;
      }
    }
    private void Window_Unloaded(object sender, RoutedEventArgs e)
    {
       
    }      
  }
}
