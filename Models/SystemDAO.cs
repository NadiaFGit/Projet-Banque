using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace Examen.Models
{
    public class SystemDAO
    {
        private DbContext dbContext;
        public SystemDAO(IConfiguration _configuration)
        {
            dbContext = new DbContext(_configuration);
        }

        //retourne la liste des banques.
        public List<Bank> GetBanks()
        {
            List<Bank> banks = new List<Bank>();
            MySqlConnection conn = new MySqlConnection(dbContext.connectionString);
            try
            {
                string sql = "SELECT * FROM Bank";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Bank b = new Bank();
                    b.ID = rdr.GetInt16(0);
                    b.Name = rdr.GetString(1);
                    b.City = rdr.GetString(2);
                    banks.Add(b);
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                conn.Close();
            }
            return banks;
        }

        public List<Account> GetAccountsForCustomer(int id)
        {
            List<Account> accounts = new List<Account>();
            MySqlConnection conn = new MySqlConnection(dbContext.connectionString);
            try
            {
                string sql = "SELECT * FROM Account WHERE CustomerID = " + id;
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Account c = new Account();
                    c.ID = rdr.GetInt16(0);
                    c.Type = rdr.GetString(1);
                    c.Balance = rdr.GetFloat(2);
                    c.CustomerID = rdr.GetInt16(3);
                    accounts.Add(c);
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                conn.Close();
            }
            return accounts;
        }

        public void AddCustomer(Customer client)
        {
            MySqlConnection conn = new MySqlConnection(dbContext.connectionString);
            try
            {
                string sql = "INSERT INTO Customer (LastName, FirstName, Phone, BankID) VALUES (@nom, @prenom, @telephone, @banqueid)";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.Add(new MySqlParameter("@nom", client.LastName));
                cmd.Parameters.Add(new MySqlParameter("@prenom", client.FirstName));
                cmd.Parameters.Add(new MySqlParameter("@telephone", client.Phone));
                cmd.Parameters.Add(new MySqlParameter("@banqueid", client.BankID));

                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                conn.Close();
            }
        }

        public void UpdateAccount(Account compte)
        {
            MySqlConnection conn = new MySqlConnection(dbContext.connectionString);
            try
            {
                string sql = "UPDATE Account SET Balance = @solde WHERE ID = @compteid";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.Add(new MySqlParameter("@solde", compte.Balance));
                cmd.Parameters.Add(new MySqlParameter("@compteid", compte.ID));

                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                conn.Close();
            }
        }

        public List<Customer> GetCustomers()
        {
            List<Customer> customers = new List<Customer>();
            MySqlConnection conn = new MySqlConnection(dbContext.connectionString);
            try
            {
                string sql = "SELECT * FROM Customer";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Customer c = new Customer();
                    c.ID = rdr.GetInt16(0);
                    c.LastName = rdr.GetString(1);
                    c.FirstName = rdr.GetString(2);
                    c.Phone = rdr.GetString(3);
                    c.BankID = rdr.GetInt16(4);
                    customers.Add(c);
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                conn.Close();
            }
            return customers;
        }

        public List<Customer> GetCustomersForBank(int id)
        {
            List<Customer> customers = new List<Customer>();
            MySqlConnection conn = new MySqlConnection(dbContext.connectionString);
            try
            {
                string sql = "SELECT * FROM Customer WHERE BankID = " + id;
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Customer c = new Customer();
                    c.ID = rdr.GetInt16(0);
                    c.LastName = rdr.GetString(1);
                    c.FirstName = rdr.GetString(2);
                    c.Phone = rdr.GetString(3);
                    c.BankID = rdr.GetInt16(4);
                    customers.Add(c);
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                conn.Close();
            }
            return customers;
        }
    }
}