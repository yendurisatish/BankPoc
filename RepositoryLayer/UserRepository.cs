using System;
using System.Web;
using System.Web.Hosting;
using ModelLayer;
using RepositoryLayer.Interface;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MySql.Data.MySqlClient;
namespace RepositoryLayer
{
   public class UserRepository
    {
       public DataSet getUserDetails(Int64 accno)
       {
            MySqlConnection conn = new MySqlConnection("Server=127.0.0.1; Port=3306; Database=mybank; Uid=aeo; Pwd=@300;");
            //SqlConnection con = new SqlConnection();
           try
           {
               //con.ConnectionString = ConfigurationManager.ConnectionStrings["BankManagmentConn"].ConnectionString;
               conn.Open();
               string getUserDetail = "SELECT * FROM Customer where accountno="+accno;
               MySqlCommand cmd = new MySqlCommand(getUserDetail, conn);
               MySqlDataAdapter da = new MySqlDataAdapter(cmd);
               DataSet ds = new DataSet();
               da.Fill(ds);
               return ds;
           }
           //throw new NotImplementedException();
           catch
           {
               return null;
           }
       }
       public void sendMoney(Transaction ts)
       {
            MySqlConnection conn = new MySqlConnection("Server=127.0.0.1; Port=3306; Database=mybank; Uid=aeo; Pwd=@300;");
            //SqlConnection con = new SqlConnection();
           try
           {
              // con.ConnectionString = ConfigurationManager.ConnectionStrings["BankManagmentConn"].ConnectionString;
               conn.Open();
              
              
               MySqlCommand cmd1 = new MySqlCommand("transfer", conn);
               cmd1.CommandType = CommandType.StoredProcedure;
               cmd1.Parameters.AddWithValue("@senderaccount", ts.senderaccount);
               cmd1.Parameters.AddWithValue("@targetaccount",ts.targetaccount );
               cmd1.Parameters.AddWithValue("@bal", ts.balance);
               cmd1.ExecuteNonQuery();
           }
           finally { }
       }
       public DataSet transHistory(Int64 accno)
       {
            MySqlConnection conn = new MySqlConnection("Server=127.0.0.1; Port=3306; Database=mybank; Uid=aeo; Pwd=@300;");
           // SqlConnection con = new SqlConnection();
           try
           {
               //con.ConnectionString = ConfigurationManager.ConnectionStrings["BankManagmentConn"].ConnectionString;
               conn.Open();
               string getUserDetail = "select * from Transaction where trans_account=" + accno + " or oth_account=" + accno ;
               MySqlCommand cmd = new MySqlCommand(getUserDetail, conn);
               MySqlDataAdapter da = new MySqlDataAdapter(cmd);
               DataSet ds = new DataSet();
               da.Fill(ds);
               return ds;
           }
           //throw new NotImplementedException();
           catch
           {
               return null;
           }
       }


       public DataSet loanDetails(Int64 accno)
       {
            MySqlConnection conn = new MySqlConnection("Server=127.0.0.1; Port=3306; Database=mybank; Uid=aeo; Pwd=@300;");
            //SqlConnection con = new SqlConnection();
           try
           {
               //con.ConnectionString = ConfigurationManager.ConnectionStrings["BankManagmentConn"].ConnectionString;
               conn.Open();
               string loanDetail = "select * from user_view_loans where account_no=" + accno;
               MySqlCommand cmd = new MySqlCommand(loanDetail, conn);
               MySqlDataAdapter da = new MySqlDataAdapter(cmd);
               DataSet ds = new DataSet();
               da.Fill(ds);
               return ds;
           }
           //throw new NotImplementedException();
           catch
           {
               return null;
           }
       }
       public DataSet depositDetails(Int64 accno)
       {
            MySqlConnection conn = new MySqlConnection("Server=127.0.0.1; Port=3306; Database=mybank; Uid=aeo; Pwd=@300;");
            //SqlConnection con = new SqlConnection();
           try
           {
               //con.ConnectionString = ConfigurationManager.ConnectionStrings["BankManagmentConn"].ConnectionString;
               conn.Open();
               string depositDetail = "select * from deposits where accountno=" + accno;
               MySqlCommand cmd = new MySqlCommand(depositDetail, conn);
              MySqlDataAdapter da = new MySqlDataAdapter(cmd);
               DataSet ds = new DataSet();
               da.Fill(ds);
               return ds;
           }
           //throw new NotImplementedException();
           catch
           {
               return null;
           }
       }
       string SaveFile(string sourcepath)
       {
           // Specify the path to save the uploaded file to.
           string savePath = "~\\Files\\";
           //savePath = System.Web.Hosting.HostingEnvironment.MapPath("~\\Files\\");
           //if (!System.IO.Directory.Exists(savePath))
           //{
           //    System.IO.Directory.CreateDirectory(savePath);
           //}
           // Get the name of the file to upload.
           string fileName = Path.GetFileName(sourcepath); 

           // Create the path and file name to check for duplicates.
           string pathToCheck = savePath + fileName;
           var x = System.Web.Hosting.HostingEnvironment.MapPath(pathToCheck);
           // Create a temporary file name to use for checking duplicates.
           string tempfileName = "";

           // Check to see if a file already exists with the
           // same name as the file to upload.        
           if (System.IO.File.Exists(x))
           {
               int counter = 2;
               while (System.IO.File.Exists(x))
               {
                   // if a file with this name already exists,
                   // prefix the filename with a number.
                   tempfileName = counter.ToString() + fileName;
                   x = savePath + tempfileName;
                   counter++;
               }

               fileName = tempfileName;

               // Notify the user that the file name was changed.
               //UploadStatusLabel.Text = "A file with the same name already exists." +
               //   "<br />Your file was saved as " + fileName;
           }
           else
           {
               // Notify the user that the file was saved successfully.
               // UploadStatusLabel.Text = "Your file was uploaded successfully.";
           }

           // Append the name of the file to upload to the path.
           savePath += fileName;

           // Call the SaveAs method to save the uploaded
           // file to the specified directory.
           System.IO.File.Copy(sourcepath, System.Web.Hosting.HostingEnvironment.MapPath(savePath), true);
          
           return savePath;

       }
      public void applyLoan(ApplyLoan al)
       {
            MySqlConnection conn = new MySqlConnection("Server=127.0.0.1; Port=3306; Database=mybank; Uid=aeo; Pwd=@300;");
         //   SqlConnection con = new SqlConnection();
          // con.ConnectionString = ConfigurationManager.ConnectionStrings["BankManagmentConn"].ConnectionString;
           conn.Open();
          // string source = @"C:\Users\saivenkatas\Desktop\doc\hackathon_participation.jpg";
           //al.Payslip = SaveFile(source);
           //al.Photo = SaveFile(source);
           //al.Signature = SaveFile(source);
           try
           {
               MySqlCommand cmd = new MySqlCommand("applyLoan", conn);
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.Parameters.AddWithValue("@accountNumber", al.AccountNumber);
               cmd.Parameters.AddWithValue("@loanType", al.LoanType);
               cmd.Parameters.AddWithValue("@income", al.Income);
               //cmd.Parameters.AddWithValue("@payslip", al.Payslip);
               //cmd.Parameters.AddWithValue("@photo", al.Photo);
               //cmd.Parameters.AddWithValue("@signature", al.Signature);
               cmd.Parameters.AddWithValue("@loanAmount", al.LoanAmount);
               cmd.Parameters.AddWithValue("@empType", al.EmpType);
               cmd.Parameters.AddWithValue("@city", al.City);
               cmd.ExecuteNonQuery();
           }
           finally
           {

               conn.Close();
           }
       }
      public void applyDeposit(Deposits ds)
      {
            MySqlConnection conn = new MySqlConnection("Server=127.0.0.1; Port=3306; Database=mybank; Uid=aeo; Pwd=@300;");
            //SqlConnection con = new SqlConnection();
          try
          {
              //con.ConnectionString = ConfigurationManager.ConnectionStrings["BankManagmentConn"].ConnectionString;
              conn.Open();
             MySqlCommand cmd1 = new MySqlCommand("insert into deposits(accountno,deposit_amount,duration,approved) values('" + ds.AccountNumber + "','" + ds.DepositAmount + "','" + ds.Duration + "','no')", conn);
              cmd1.ExecuteNonQuery();
          }
          finally
          {
              conn.Close();
          }
      }


      public DataSet Login(string username,string password)
      {
            MySqlConnection conn = new MySqlConnection("Server=127.0.0.1; Port=3306; Database=mybank; Uid=aeo; Pwd=@300;");
           // SqlConnection con = new SqlConnection();
          try
          {
            //  conn.ConnectionString = ConfigurationManager.ConnectionStrings["BankManagmentConn"].ConnectionString;
              conn.Open();
              MySqlCommand cmd = new MySqlCommand("select * from Customer where username='" + username + "' and password='" + password + "'", conn);
              MySqlDataAdapter da = new MySqlDataAdapter(cmd);
              DataSet ds = new DataSet();
              da.Fill(ds);
              return ds;
          }
          //throw new NotImplementedException();
          catch
          {
              return null;
          }
      }
    }
}
