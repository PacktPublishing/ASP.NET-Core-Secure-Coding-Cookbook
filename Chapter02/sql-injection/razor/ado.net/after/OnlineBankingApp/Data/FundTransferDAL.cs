using System;  
using System.Collections.Generic;  
using System.Data;  
using System.Data.SqlClient;  
using Microsoft.Extensions.Configuration;
using OnlineBankingApp.Models;
using Microsoft.Data.Sqlite;

namespace OnlineBankingApp.Data  
{
    public class FundTransferDAL  
    {
        private readonly IConfiguration Configuration;
        private readonly string connectionString;

        public FundTransferDAL(IConfiguration configuration)
        {
            Configuration = configuration;
            connectionString = Configuration["ConnectionStrings:OnlineBankDB"];
        }

        public IEnumerable<FundTransfer> GetFundTransfers()  
        {  
            List<FundTransfer> fundTransfers = new List<FundTransfer>();  
  
            using (SqliteConnection con = new SqliteConnection(connectionString))  
            {  
                SqliteCommand cmd = new SqliteCommand("Select * from FundTransfer", con);  
                cmd.CommandType = CommandType.Text;  
  
                con.Open();  
                SqliteDataReader rdr = cmd.ExecuteReader();  
  
                while (rdr.Read())  
                {  
                    FundTransfer fundtransfer = new FundTransfer();  
  
                    fundtransfer.ID = Convert.ToInt32(rdr["ID"]);  
                    fundtransfer.AccountFrom = Convert.ToInt32(rdr["AccountFrom"]);  
                    fundtransfer.AccountTo = Convert.ToInt32(rdr["AccountTo"]);
                    fundtransfer.TransactionDate = Convert.ToDateTime(rdr["TransactionDate"]);
                    fundtransfer.Amount = decimal.Parse(rdr["Amount"].ToString());
                    fundtransfer.Note = rdr["Note"].ToString();
                    fundTransfers.Add(fundtransfer);  
                }  
                con.Close();  
            }  
            return fundTransfers;  
        }  
    
  
        public IEnumerable<FundTransfer> GetFundTransfers(string search)  
        {  
            List<FundTransfer> fundTransfers = new List<FundTransfer>();  
  
            using (SqliteConnection con = new SqliteConnection(connectionString))  
            {  
                SqliteCommand cmd = new SqliteCommand("Select * from FundTransfer where Note like '%" + @search + "%'", con);  
                cmd.CommandType = CommandType.Text;  
  
                cmd.Parameters.AddWithValue("@search", search);  

                con.Open();  
                SqliteDataReader rdr = cmd.ExecuteReader();  
  
                while (rdr.Read())  
                {  
                    FundTransfer fundtransfer = new FundTransfer();  
                    fundtransfer.ID = Convert.ToInt32(rdr["ID"]);  
                    fundtransfer.AccountFrom = Convert.ToInt32(rdr["AccountFrom"]);  
                    fundtransfer.AccountTo = Convert.ToInt32(rdr["AccountTo"]);
                    fundtransfer.TransactionDate = Convert.ToDateTime(rdr["TransactionDate"]);
                    fundtransfer.Amount = decimal.Parse(rdr["Amount"].ToString());
                    fundtransfer.Note = rdr["Note"].ToString();
                    fundTransfers.Add(fundtransfer);  
                }  
                con.Close();  
            }  
            return fundTransfers;  
        } 

    }
}