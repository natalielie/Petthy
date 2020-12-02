using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Petthy.Models
{
    public class Client
    {
        public int ClientId { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }

        public Client() { }
        public Client(int clientId, string userId, string firstName, string lastName, 

            string phoneNumber, DateTime dateOfBirth, string address)
        {
            this.ClientId = clientId;
            this.UserId = userId;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.PhoneNumber = phoneNumber;
            this.DateOfBirth = dateOfBirth;
            this.Address = address;
        }
    }
}
