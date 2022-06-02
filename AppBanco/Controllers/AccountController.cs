using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppBanco.Models;
using AppBanco.Data;
using AppBanco.Models.DTOs;

namespace AppBanco.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : Controller
    {
        // POST: Account/Create
        [HttpPost("cadastro")]
        [ValidateAntiForgeryToken]
        public ActionResult Register([FromBody] NewDTOAccount NewAccount)
        {
            try
            {
                if (!NewAccount.Email.Equals(null) && !NewAccount.Password.Equals(null))
                {
                    Random r = new Random();
                    int Acc = r.Next(10000000, 99999999);
                    AccountContext accountContext = new AccountContext();
                    User user = new User();
                    
                    user.Name = NewAccount.Name;
                    user.Email = NewAccount.Email;
                    user.Password = NewAccount.Password;

                    Account account = new Account();
                    account.NumAccount = Acc;
                    account.Balance = 0f;
                    account.AccTran = new List<AccTran>();

                    user.NumAccount = account;

                    accountContext.Users.Add(user);
                    accountContext.Accounts.Add(account);
                    accountContext.SaveChanges();

                    return Ok($"Usuario cadastrado com a conta: {Acc}");
                }else return Ok("Preencha todos os campos");
            }
            catch
            {
                return NoContent();
            }
        }

        // GET: Account/Delete/5
        [HttpGet("login")]
        public ActionResult Login([FromBody] NewDTOAccount LoginAccount)
        {
            try
            {
                AccountContext account = new AccountContext();
                var usuarios = (from User in account.Users select User.Email).ToList();
                for (int i = 0; i < usuarios.Count; i++)
                {
                    if (LoginAccount.Email == usuarios[i])
                    {
                        string senha = (from User in account.Users where User.Email.Equals(usuarios[i]) select User.Password).ToString();
                        if (senha.Equals(LoginAccount.Password))
                        {
                            return Ok("login ok");
                        }
                        else return Ok("Senha incorreta");
                    }
                    else return Ok("Email não encontrado");
                }
                return Ok(); ;
            }
            catch
            {
                return NoContent();
            }
        }
        [HttpGet("saldo")]
        public ActionResult Saldo([FromBody] Account Conta)
        {
            try
            {
                AccountContext account = new AccountContext();
                float Saldo = (from Account in account.Accounts where Account.NumAccount.Equals(Conta.NumAccount) select Account.Balance).FirstOrDefault();
                return Ok($"Seu saldo é de: {Saldo}");
            }
            catch
            {
                return NoContent();
            } 
        }
        [HttpGet("extrato")]
        public ActionResult Extrato([FromBody] Account Conta)
        {
            try
            {
                AccountContext account = new AccountContext();
                var extrato = (from Transaction in account.Transactions where Transaction.IDSourceAccount.Equals(Conta.NumAccount) || Transaction.IDTargetAccount.Equals(Conta.NumAccount) select Transaction).ToList();
                return Ok(extrato);
            }
            catch
            {
                return NoContent();
            }
        }
        [HttpGet("transacao")]
        public ActionResult Transacao([FromBody] Account ContaOrigem, Account ContaDestino, float Valor)
        {
            try
            {
                AccountContext account = new AccountContext();
                Transaction transacao = new Transaction();
                float SaldoOrigem = (from Account in account.Accounts where Account.NumAccount.Equals(ContaOrigem.NumAccount) select Account.Balance).FirstOrDefault();
                float SaldoDestino = (from Account in account.Accounts where Account.NumAccount.Equals(ContaDestino.NumAccount) select Account.Balance).FirstOrDefault();

                if (Valor <= ContaOrigem.Balance)
                {
                    SaldoOrigem -= Valor;
                    SaldoDestino += Valor;

                    transacao.TransactionDate = DateTime.Now;
                    ContaDestino.Balance = SaldoDestino;
                    ContaOrigem.Balance = SaldoOrigem;
                    account.SaveChanges();
                    return Ok("Transacao Realizada");
                }
                else return Ok("Saldo insuficiente");
            }
            catch
            {
                return NoContent();
            }
        }
    }
}
