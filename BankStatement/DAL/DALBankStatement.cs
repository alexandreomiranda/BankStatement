using BankStatement.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace BankStatement.DAL
{
	public class DALBankStatement
	{
		string conexao = WebConfigurationManager.ConnectionStrings["BankConnectionString"].ConnectionString;

		public List<TransactionViewModel> Get(Guid guid, DateTime initialDate)
		{
			string sqlCommand = @"Select t1.ID, t1.DateTransaction, t1.Credit, t1.Debit, t1.Description, t1.AccountID, t2.Number " +
				",ISNULL((SELECT SUM(Credit) FROM [BankStatement].[dbo].[Transaction] " + 
				"WHERE DateTransaction < @initialDate AND AccountID = @guid) - " +
				"(SELECT SUM(Debit) FROM [BankStatement].[dbo].[Transaction] " +
				"WHERE DateTransaction < @initialDate AND AccountID = @guid),0) as StartingDayBalance " +
				"FROM [BankStatement].[dbo].[Transaction] AS t1 " +
                "INNER JOIN [BankStatement].[dbo].[Account] as t2 ON t1.AccountID = t2.ID " +
				"WHERE t1.AccountID = @guid AND t1.DateTransaction >= @initialDate " +
				"ORDER BY t1.DateTransaction";
			
			using (var conn = new SqlConnection(conexao))
			{
				var cmd = new SqlCommand(sqlCommand, conn);
				cmd.Parameters.Add("@guid", SqlDbType.UniqueIdentifier);
				cmd.Parameters["@guid"].Value = new Guid(guid.ToByteArray());
				cmd.Parameters.Add("@initialDate", SqlDbType.DateTime);
				cmd.Parameters["@initialDate"].Value = initialDate;
				List<TransactionViewModel> data = new List<TransactionViewModel>();
				TransactionViewModel tvm = null;
				try
				{
					conn.Open();
					using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
					{
						while (reader.Read())
						{
							tvm = new TransactionViewModel();
                            tvm.ID = (Guid)reader["ID"];
							tvm.StartingDayBalance = (decimal)reader["StartingDayBalance"];
                            tvm.DateTransaction = (DateTime)reader ["DateTransaction"];
                            tvm.Credit = (decimal)reader["Credit"];
                            tvm.Debit = (decimal)reader["Debit"];
                            tvm.Value = tvm.Credit - tvm.Debit;
                            tvm.Description = (string)reader["Description"];
                            tvm.Number = (string)reader["Number"];
							data.Add(tvm);
						}
					}
				}
				finally
				{
					conn.Close();
				}
                if (data != null)
                    return data;
                else
                    return null;
			}
		}

        public List<Account> GetAccounts()
        {
            string sqlCommand = @"Select ID, Number, Name from [BankStatement].[dbo].[Account]";
            using (var conn = new SqlConnection(conexao))
            {
                var cmd = new SqlCommand(sqlCommand, conn);
                List<Account> data = new List<Account>();
                Account a = null;
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            a = new Account();
                            a.ID = (Guid)reader["ID"];
                            a.Name = (string)reader["Name"];
                            a.Number = (string)reader["Number"];
                            data.Add(a);
                        }
                    }
                }
                finally
                {
                    conn.Close();
                }
                return data;
            }
        }
	}
}