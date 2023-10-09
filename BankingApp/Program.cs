using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp
{
	internal class Program
	{
		static void Main(string[] args)
		{
			// we'll make three accounts, all in a list
			List<BankAccount> accountList = new List<BankAccount>();
			accountList.Add(new BankAccount(100000,5000,"John Doe"));
			accountList.Add(new BankAccount(101000,6000,"Jane Doe"));
			accountList.Add(new BankAccount(110000,4000,"Joel Doe"));

			// do a bunch of transactions
			DepositAccount(accountList[1],3000);
			WithdrawAccount(accountList[0],3000);
			DepositAccount(accountList[0],1500);
			WithdrawAccount(accountList[1],1500);
			DepositAccount(accountList[1],750);
			WithdrawAccount(accountList[0],750);
			DepositAccount(accountList[0],4500);
			WithdrawAccount(accountList[1],4500);
			WithdrawAccount(accountList[2],1500);
			WithdrawAccount(accountList[2],1500);
			WithdrawAccount(accountList[2],1500);
			WithdrawAccount(accountList[2],1500);
			WithdrawAccount(accountList[2],1500);
			WithdrawAccount(accountList[2],1500);
			WithdrawAccount(accountList[2],1500);

			// write em all
			foreach (BankAccount account in accountList){
				WriteAccount(account);
			}

			// wait
			Console.WriteLine();
			Console.WriteLine("Press any key to continue...");
			Console.ReadKey();
		}

		static void WriteAccount(BankAccount account){
			Console.WriteLine(account.AccountNumber.ToString()+" - "+account.AccountName+" - $"+account.AccountBalance.ToString());
			Console.WriteLine("Transaction History:");
			foreach (Transaction trsc in account.TransacHistory){
				var TType = "WTHDRWL";
				if (trsc.transactionType == 1) TType = "DEPOSIT";
				if (trsc.transactionType == 2) TType = "DCLINED";
				Console.WriteLine("$"+trsc.transactionAmount.ToString()+" - "+TType+" - Total: $"+trsc.transactionTotal.ToString());
			}
			// blank line for cleanness
			Console.WriteLine();
		}

		static void DepositAccount(BankAccount account, double amount){
			account.AccountBalance += amount; // add money to the account
			account.TransacHistory.Add(new Transaction(1,amount,account.AccountBalance)); // add to the transaction record
			// no reason to return because this is a void method
		}

		static bool WithdrawAccount(BankAccount account, double amount){
			if (account.AccountBalance < amount){
				account.TransacHistory.Add(new Transaction(2,amount,account.AccountBalance)); // add to the record that a withdrawl was declined
				return false; // we can't pull that much money, so we'll return false
			}else{
				account.AccountBalance -= amount; // deduct money from the account
				account.TransacHistory.Add(new Transaction(0,amount,account.AccountBalance)); // add to the transaction record
				return true; // the transaction is complete. return true
			}
		}

		public class BankAccount{
			public BankAccount(int newID, double newBal, string newName){
				AccountNumber = newID;
				AccountBalance = newBal;
				AccountName = newName;
				TransacHistory = new List<Transaction>();
			}

			public int AccountNumber {get;set;}
			public string AccountName {get;set;}
			public double AccountBalance {get;set;}
			public List<Transaction> TransacHistory {get;set;}
		}

		public struct Transaction{ // for history
			public Transaction(int transacType,double transacAmt,double transacTot){
				transactionType = transacType;
				transactionAmount = transacAmt;
				transactionTotal = transacTot;
			}

			public int transactionType;
			public double transactionAmount;
			public double transactionTotal;
		}
	}
}
