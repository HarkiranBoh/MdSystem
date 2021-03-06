﻿using MedicalSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalSystem.Models
{
    public class ProfileRepository
    {
        private readonly AppDbContext _appDbContext;


        public ProfileRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        //updates the user details  with the usernsame and profoleviewmodel being passed in.
        public void UpdateUser(string userName, ProfileViewModel profileViewModel)
        {
            var rec = _appDbContext.Users.Where(usr => usr.UserName == userName).FirstOrDefault();
           if(rec != null && profileViewModel != null)
            {
                rec.FirstName = profileViewModel.FirstName;
                rec.LastName = profileViewModel.LastName;
                rec.HospitalName = profileViewModel.HospitalName;
                rec.AddressLine1 = profileViewModel.AddressLine1;
                rec.AddressLine2 = profileViewModel.AddressLine2;
                rec.postcode = profileViewModel.PostCode;

                _appDbContext.Update(rec);
                _appDbContext.SaveChanges();
            }
        }

        //get logged on user details for updating
        public User GetLoggedInUserDetails(string userName)
        {
            var rec = _appDbContext.Users.Where(usr => usr.UserName == userName).FirstOrDefault();
            User user = null;
            if (rec != null)
            {
                user = new User();
                user.HospitalName = rec.HospitalName;
                user.UserName = rec.UserName;
                user.Email = rec.Email;
                user.PostCode = rec.postcode;
                user.AddressLine1 = rec.AddressLine1;
                user.AddressLine2 = rec.AddressLine2;
                user.FirstName = rec.FirstName;
                user.LastName = rec.LastName;
                
            }
           
            return user;
        }

    }
}
    