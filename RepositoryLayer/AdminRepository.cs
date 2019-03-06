using ModelLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using MySql.Data.MySqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace RepositoryLayer
{
    public class AdminRepository : IAdminRepository
    {
        public DataSet GetAccountDetail()
        {
            MySqlConnection conn = new MySqlConnection("Server=127.0.0.1; Port=3306; Database=mybank; Uid=aeo; Pwd=@300;");
            SqlConnection con = new SqlConnection();
            try

            {
                //con.ConnectionString = ConfigurationManager.ConnectionStrings["BankManagmentConn"].ConnectionString;
                conn.Open();
                string getUserDetail = "SELECT * FROM view_users";
                MySqlCommand cmd = new MySqlCommand(getUserDetail, conn);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            //throw new NotImplementedException();
            catch (Exception ex)
            {
                throw ex; ;
            }

        }
        public DataSet GetDeposits()
        {
            MySqlConnection conn = new MySqlConnection("Server=127.0.0.1; Port=3306; Database=mybank; Uid=aeo; Pwd=@300;");
            //SqlConnection con = new SqlConnection();
            try
            {
                //con.ConnectionString = ConfigurationManager.ConnectionStrings["BankManagmentConn"].ConnectionString;
                conn.Open();
                string getUserDetail = "SELECT * FROM deposits";
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
        public DataSet GetUnApprovedDeposits()
        {
            MySqlConnection conn = new MySqlConnection("Server=127.0.0.1; Port=3306; Database=mybank; Uid=aeo; Pwd=@300;");
            //SqlConnection con = new SqlConnection();
            try
            {
                //con.ConnectionString = ConfigurationManager.ConnectionStrings["BankManagmentConn"].ConnectionString;
                conn.Open();
                string getUserDetail = "SELECT * FROM deposits where approved='no'";
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
        public DataSet GetLoans()
        {
            MySqlConnection conn = new MySqlConnection("Server=127.0.0.1; Port=3306; Database=mybank; Uid=aeo; Pwd=@300;");
            //SqlConnection con = new SqlConnection();
            try
            {
                //con.ConnectionString = ConfigurationManager.ConnectionStrings["BankManagmentConn"].ConnectionString;
                conn.Open();
                string getUserDetail = "SELECT * from admin_view_loans";
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
        public DataSet GetUnApprovedLoans()
        {
            MySqlConnection conn = new MySqlConnection("Server=127.0.0.1; Port=3306; Database=mybank; Uid=aeo; Pwd=@300;");
            //SqlConnection con = new SqlConnection();
            try
            {
                //con.ConnectionString = ConfigurationManager.ConnectionStrings["BankManagmentConn"].ConnectionString;
                conn.Open();
                string getUserDetail = "SELECT * from admin_approve_loans";
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
        
        public void CloseAccount(Int64 accno)
        {
            MySqlConnection conn = new MySqlConnection("Server=127.0.0.1; Port=3306; Database=mybank; Uid=aeo; Pwd=@300;");
           // SqlConnection con = new SqlConnection();
            try
            {
               // con.ConnectionString = ConfigurationManager.ConnectionStrings["BankManagmentConn"].ConnectionString;
                conn.Open();
                string getUserDetail = "delete  from Customer where accountno='" + accno + "';";
                MySqlCommand cmd = new MySqlCommand(getUserDetail, conn);
                cmd.ExecuteNonQuery();

            }
            //throw new NotImplementedException();
            catch
            {

            }
        }
        public void ApproveLoans(int id,int acc)
        {
            MySqlConnection conn = new MySqlConnection("Server=127.0.0.1; Port=3306; Database=mybank; Uid=aeo; Pwd=@300;");
            //SqlConnection con = new SqlConnection();
            try
            {
                //con.ConnectionString = ConfigurationManager.ConnectionStrings["BankManagmentConn"].ConnectionString;
                conn.Open();
                string getUserDetail = "SELECT * FROM Loan where Id=" + id;
                MySqlCommand cmd = new MySqlCommand(getUserDetail, conn);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                //DataRow row = ds.Tables[0].Rows[0];
                Int32 bal = Convert.ToInt32(ds.Tables[0].Rows[0]["loan_amount"]);
                Int64 accno = Convert.ToInt32(ds.Tables[0].Rows[0]["account_no"]);
                MySqlCommand cmd1 = new MySqlCommand("approveloans", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@Loan_id", id);
                cmd1.Parameters.AddWithValue("@account_no", accno);
                cmd1.Parameters.AddWithValue("@bal", bal);
                cmd1.Parameters.AddWithValue("@appAccountNo", acc);
                cmd1.ExecuteNonQuery();
            }
            finally { }
        }
        public void ApproveDeposits(int id, int acc)
        {
            MySqlConnection conn = new MySqlConnection("Server=127.0.0.1; Port=3306; Database=mybank; Uid=aeo; Pwd=@300;");
            //SqlConnection con = new SqlConnection();
            try
            {
                //con.ConnectionString = ConfigurationManager.ConnectionStrings["BankManagmentConn"].ConnectionString;
                conn.Open();
                string getUserDetail = "SELECT * FROM deposits where deposit_id=" + id;
                MySqlCommand cmd = new MySqlCommand(getUserDetail, conn);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                //DataRow row = ds.Tables[0].Rows[0];
                Int32 bal = Convert.ToInt32(ds.Tables[0].Rows[0]["deposit_amount"]);
                Int64 accno = Convert.ToInt32(ds.Tables[0].Rows[0][1]);
                MySqlCommand cmd1 = new MySqlCommand("approvedeposits", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@id", id);
                cmd1.Parameters.AddWithValue("@account_no", accno);
                cmd1.Parameters.AddWithValue("@bal", bal);
                cmd1.Parameters.AddWithValue("@appAccountNo", acc);
                cmd1.ExecuteNonQuery();
            }
            finally { }
        }
        public void UpdateAccount(CreateUser cu)
        {
            MySqlConnection conn = new MySqlConnection("Server=127.0.0.1; Port=3306; Database=mybank; Uid=aeo; Pwd=@300;");
            //SqlConnection con = new SqlConnection();
            try
            {
                //con.ConnectionString = ConfigurationManager.ConnectionStrings["BankManagmentConn"].ConnectionString;
                conn.Open();


                int boolInt = cu.IsAdmin ? 1 : 0;
                MySqlCommand cmd = new MySqlCommand("update", conn);
                
                cmd.CommandType = CommandType.StoredProcedure;
               // cmd.Parameters.AddWithValue("@username", cu.UserName);
                cmd.Parameters.AddWithValue("@account_no", cu.AccountNumber);
                cmd.Parameters.AddWithValue("@firstname", cu.FirstName);
                cmd.Parameters.AddWithValue("@lastname", cu.LastName);
              //  cmd.Parameters.AddWithValue("@dob", cu.Dob);
                cmd.Parameters.AddWithValue("@phoneno", cu.PhoneNumber);
                cmd.Parameters.AddWithValue("@email", cu.Email);
               // cmd.Parameters.AddWithValue("@aadhar_no", cu.Aadhar);
             //   cmd.Parameters.AddWithValue("@account_type", cu.AccountType);
                cmd.Parameters.AddWithValue("@balance", cu.Balance);
                cmd.Parameters.AddWithValue("@address", cu.Address);

              //  cmd.Parameters.AddWithValue("@admin", cu.IsAdmin);
                //cmd1.ExecuteNonQuery();
                cmd.ExecuteNonQuery();


            }
            finally
            {
                conn.Close();
            }

        }

        DataSet IAdminRepository.GetAccountDetail()
        {
            throw new NotImplementedException();
        }

        DataSet IAdminRepository.GetDeposits()
        {
            throw new NotImplementedException();
        }

        DataSet IAdminRepository.GetLoans()
        {
            throw new NotImplementedException();
        }

        void IAdminRepository.ApproveLoans(int id)
        {
            throw new NotImplementedException();
        }

        void IAdminRepository.ApproveDeposits(int id)
        {
            throw new NotImplementedException();
        }

        void IAdminRepository.CloseAccount(long acc)
        {
            throw new NotImplementedException();
        }

        public void CreateAccount(CreateUser cu, ref string errorMessage)
        {
            MySqlConnection conn = new MySqlConnection("Server=127.0.0.1; Port=3306; Database=mybank; Uid=aeo; Pwd=@300;");
            //SqlConnection con = new SqlConnection();
            try
            {
                //con.ConnectionString = ConfigurationManager.ConnectionStrings["BankManagmentConn"].ConnectionString;
                conn.Open();

                //DateTime dob = DateTime.ParseExact(Convert.ToString(cu.Dob), "dd/mm/yyyy", null);
                int boolInt = cu.IsAdmin ? 1 : 0;
                MySqlCommand cmd = new MySqlCommand("adddata", conn);
                //  SqlCommand cmd1 = new SqlCommand("insert into Login(username,password,admin) values('" + cu.UserName + "','" + cu.Password + "'," + boolInt + ");", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@username", cu.UserName);
                //  cmd.Parameters.AddWithValue("@accountno", cu.AccountNumber);
                cmd.Parameters.AddWithValue("@firstname", cu.FirstName);
                cmd.Parameters.AddWithValue("@lastname", cu.LastName);
                cmd.Parameters.AddWithValue("@dob", cu.Dob);
                cmd.Parameters.AddWithValue("@phoneno", cu.PhoneNumber);
                cmd.Parameters.AddWithValue("@email", cu.Email);
                cmd.Parameters.AddWithValue("@aadhar_no", cu.Aadhar);
                cmd.Parameters.AddWithValue("@account_type", cu.AccountType);
                cmd.Parameters.AddWithValue("@balance", cu.Balance);
                cmd.Parameters.AddWithValue("@address", cu.Address);
                cmd.Parameters.AddWithValue("@password", cu.Password);
                cmd.Parameters.AddWithValue("@Isadmin", cu.IsAdmin);
                // cmd1.ExecuteNonQuery();
                cmd.ExecuteNonQuery();


            }
            catch (Exception ex)
            {

                errorMessage = ex.Message;

            }
            finally
            {
                conn.Close();
            }


        }
        void IAdminRepository.UpdateAccount(CreateUser cu)
        {
            throw new NotImplementedException();
        }
    }
}
