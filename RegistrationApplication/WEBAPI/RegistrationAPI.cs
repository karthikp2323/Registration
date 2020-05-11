using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using DAL.Repositories.IRepository;
using DAL.Repositories.Repository;
using MailKit.Net.Smtp;
using MimeKit;
using Registration.ViewModel;
using Microsoft.AspNetCore.Razor.TagHelpers;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Registration.WEBAPI
{
    [Route("api/Registration")]
    public class RegistrationAPI : Controller
    {
        IUserRepository _userRepository;
        IAddressRepository _addressRepository;
        public RegistrationAPI(IUserRepository userRepository, IAddressRepository addressRepository) {
            _userRepository = userRepository;
            _addressRepository = addressRepository;
        }

        // POST api/Registration/Login
        [HttpPost("Login")]
        public User Login([FromBody]User user)
        {
            string password = EnryptString(user.Password);
            var getUser = _userRepository.GetAll().FirstOrDefault(x => x.EmailId == user.EmailId && x.Password == password);
            return getUser;
        }

        // POST api/Registration/SignUp
        [HttpPost("SignUp")]
        public async Task<User> SignUp([FromBody]User user)
        {
            user.Password = EnryptString(user.Password);
            await _userRepository.AddUser(user);
            //commented sendmail for now, enable it with appropriate sender credentials.
            //SendMail();
            return user;
        }

        // POST api/Registration/CompleteProfile
        [HttpPost("CompleteProfile")]
        public async Task<User> CompleteProfile([FromBody]UserAddress userAddress)
        {
            var user = _userRepository.Get(userAddress.User.Id).Result;

            user.FirstName = userAddress.User.FirstName;
            user.LastName = userAddress.User.LastName;
            user.Contact = userAddress.User.Contact;
            user.AllowEmailNotifications = userAddress.User.AllowEmailNotifications;
            

            Address address = new Address()
            {
                Street1 = userAddress.Address.Street1,
                Street2 = userAddress.Address.Street2,
                ZipCode = userAddress.Address.ZipCode,
                State = userAddress.Address.State,
                Country = userAddress.Address.Country,
                UserId = userAddress.User.Id
            };

            await _userRepository.UpdateUser(user);
            await _addressRepository.AddAddress(address);
            return user;
        }

        protected string EnryptString(string strEncrypted)
        {
            byte[] b = System.Text.ASCIIEncoding.ASCII.GetBytes(strEncrypted);
            string encrypted = Convert.ToBase64String(b);
            return encrypted;
        }

        protected void SendMail() {
            MimeMessage message = new MimeMessage();

            MailboxAddress from = new MailboxAddress("Admin",
            "your@gmail.com");
            message.From.Add(from);

            MailboxAddress to = new MailboxAddress("User",
            "sender@gmail.com");
            message.To.Add(to);

            message.Subject = "Thank you for registering!";

            BodyBuilder bodyBuilder = new BodyBuilder();
            //bodyBuilder.HtmlBody = "<h1>Welcome</h1>";
            bodyBuilder.TextBody = "Welcome!";

            SmtpClient client = new SmtpClient();
            //provide address and port number here
            client.Connect("smtp_address_here", 0000, true);
            client.Authenticate("user_name_here", "pwd_here");

            client.Send(message);
            client.Disconnect(true);
            client.Dispose();
        }

    }
}
